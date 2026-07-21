using PuppeteerSharp;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Services;

public interface IChromiumInitializer : IAsyncDisposable
{
    Task<string> EnsureDownloadedAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает контекст с новой страницей. 
    /// Освобождение контекста автоматически учитывает активность и запускает таймер закрытия браузера.
    /// </summary>
    Task<IPageContext> CreatePageAsync(CancellationToken cancellationToken = default);
}

public interface IPageContext : IAsyncDisposable
{
    IPage Page { get; }
}

public class ChromiumInitializer : IChromiumInitializer
{
    private readonly string? _chromePath;
    private readonly TimeSpan _idleTimeout;
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    private bool _isDownloaded = false;
    private string _executablePath = string.Empty;
    private IBrowser? _browser;

    private int _activePagesCount = 0;
    private CancellationTokenSource? _ctsIdleTimer;

    public ChromiumInitializer(string? chromePath = null, TimeSpan? idleTimeout = null)
    {
        _chromePath = chromePath;
        // По умолчанию закрываем браузер через 5 минут простоя
        _idleTimeout = idleTimeout ?? TimeSpan.FromMinutes(5);
    }
    public async Task<IPageContext> CreatePageAsync(CancellationToken cancellationToken = default)
    {
        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            // 1. Отменяем таймер авто-закрытия, так как пришел новый запрос
            CancelIdleTimer();

            // 2. Гарантируем запуск браузера
            var browser = await GetOrLaunchBrowserInternalAsync(cancellationToken);

            // 3. Создаем новую вкладку
            var page = await browser.NewPageAsync();

            // 4. Увеличиваем счетчик активных страниц
            Interlocked.Increment(ref _activePagesCount);

            return new PageContext(page, OnPageDisposedAsync);
        }
        finally
        {
            _semaphore.Release();
        }
    }
    private async Task<IBrowser> GetOrLaunchBrowserInternalAsync(CancellationToken cancellationToken)
    {
        if (_browser is { IsConnected: true, IsClosed: false })
            return _browser;

        if (_browser is not null)
        {
            await _browser.DisposeAsync();
            _browser = null;
        }

        var executablePath = await EnsureDownloadedInternalAsync(cancellationToken);

        var options = new LaunchOptions
        {
            Headless = true,
            ExecutablePath = executablePath,
            Args = new[]
            {
                "--no-sandbox",
                "--disable-setuid-sandbox",
                "--disable-dev-shm-usage"
            }
        };

        _browser = await Puppeteer.LaunchAsync(options);
        _browser.Disconnected += OnBrowserDisconnected;

        return _browser;
    }

    private async Task OnPageDisposedAsync(IPage page)
    {
        // Закрываем саму страницу
        try
        {
            if (!page.IsClosed) await page.CloseAsync();
        }
        catch { /* Игнорируем ошибки при закрытии уже упавшей страницы */ }

        await _semaphore.WaitAsync();
        try
        {
            // Уменьшаем счетчик активных вкладок
            var currentCount = Interlocked.Decrement(ref _activePagesCount);

            // Если активных страниц больше нет — запускаем таймер обратного отсчета
            if (currentCount <= 0)
            {
                StartIdleTimer();
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }
    private void StartIdleTimer()
    {
        CancelIdleTimer();
        _ctsIdleTimer = new CancellationTokenSource();
        var token = _ctsIdleTimer.Token;

        _ = Task.Run(async () =>
        {
            try
            {
                await Task.Delay(_idleTimeout, token);

                // Если таймаут истек и отмена не вызвана — закрываем браузер
                await CloseBrowserAsync();
            }
            catch (OperationCanceledException)
            {
                // Таймер был сброшен новым запросом — ничего не делаем
            }
        }, token);
    }

    private void CancelIdleTimer()
    {
        _ctsIdleTimer?.Cancel();
        _ctsIdleTimer?.Dispose();
        _ctsIdleTimer = null;
    }

    public async Task CloseBrowserAsync()
    {
        await _semaphore.WaitAsync();
        try
        {
            CancelIdleTimer();

            if (_browser is not null)
            {
                _browser.Disconnected -= OnBrowserDisconnected;
                await _browser.CloseAsync();
                await _browser.DisposeAsync();
                _browser = null;
            }
        }
        catch { /* Игнорируем ошибки закрытия */ }
        finally
        {
            _semaphore.Release();
        }
    }
    public async Task<string> EnsureDownloadedAsync(CancellationToken cancellationToken = default)
    {
        if (_isDownloaded) return _executablePath;

        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            return await EnsureDownloadedInternalAsync(cancellationToken);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    private async Task<string> EnsureDownloadedInternalAsync(CancellationToken cancellationToken)
    {
        if (_isDownloaded) return _executablePath;

        var options = _chromePath == null
            ? new BrowserFetcherOptions()
            : new BrowserFetcherOptions { Path = _chromePath };

        var browserFetcher = new BrowserFetcher(options);
        var installedInfo = await browserFetcher.DownloadAsync();

        _executablePath = installedInfo.GetExecutablePath();
        _isDownloaded = true;

        return _executablePath;
    }

    private void OnBrowserDisconnected(object? sender, EventArgs e)
    {
        if (sender is IBrowser b) b.Disconnected -= OnBrowserDisconnected;
        _browser = null;
    }

    public async ValueTask DisposeAsync()
    {
        await CloseBrowserAsync();
        _semaphore.Dispose();
    }

    private sealed class PageContext : IPageContext
    {
        private readonly Func<IPage, Task> _onDispose;
        private int _isDisposed;

        public IPage Page { get; }

        public PageContext(IPage page, Func<IPage, Task> onDispose)
        {
            Page = page;
            _onDispose = onDispose;
        }

        public async ValueTask DisposeAsync()
        {
            if (Interlocked.Exchange(ref _isDisposed, 1) == 0)
            {
                await _onDispose(Page);
            }
        }
    }
}
