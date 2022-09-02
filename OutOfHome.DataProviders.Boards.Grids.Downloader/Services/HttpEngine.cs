using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Exceptions;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Services;

internal sealed class HttpEngine<TRequest, TResult, TParser>
    where TRequest : IRequest, new()
    where TResult : new()
    where TParser : IContentParser<TResult>, new()
{
    private HttpClient httpClient;
    public HttpClient HttpClient
    {
        get => httpClient ??= HttpClientFactory.CreateDefaultHttpClient();
        set => httpClient = value;
    }
    public HttpEngine(HttpClient httpClient)
    {
        HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }
    public HttpEngine() : this(HttpClientFactory.CreateDefaultHttpClient())
    { }

    public async Task<TResult> QueryAsync(TRequest request, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage httpMessage;

        try
        {
            httpMessage = await ProcessRequestAsync(request, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            throw new DownloaderException(ErrorCode.RequestError, ex.GetBaseException().Message);
        }

        var response = await ProcessResponseAsync(httpMessage).ConfigureAwait(false);

        return response;

    }
    private async Task<HttpResponseMessage> ProcessRequestAsync(TRequest request, CancellationToken cancellationToken = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var uri = request.GetUri();

        if (request is IRequestPost p)
        {
            using (var content = p.GetContent())
            {
                return await HttpClient.PostAsync(uri, content, cancellationToken).ConfigureAwait(false);
            }
        }

        return await HttpClient.GetAsync(uri, cancellationToken).ConfigureAwait(false);
    }
    private async Task<TResult> ProcessResponseAsync(HttpResponseMessage httpResponse)
    {
        using (httpResponse)
        {
            if (httpResponse.IsSuccessStatusCode)
            {
                TParser parser = new TParser();
                return await parser.ParseContent(httpResponse);
            }
            else
            {
                throw new DownloaderException(ErrorCode.HttpError, httpResponse.ReasonPhrase);
            }
        }
    }
}
internal sealed class HttpEngine<TRequest>
    where TRequest : IRequest, new()
{
    private HttpClient httpClient;
    public HttpClient HttpClient
    {
        get => httpClient ??= HttpClientFactory.CreateDefaultHttpClient();
        set => httpClient = value;
    }
    public HttpEngine(HttpClient httpClient)
    {
        HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }
    public HttpEngine() : this(HttpClientFactory.CreateDefaultHttpClient())
    { }

    public async Task<bool> QueryAsync(TRequest request, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage httpMessage;

        try
        {
            httpMessage = await ProcessRequestAsync(request, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            throw new DownloaderException(ErrorCode.RequestError, ex.GetBaseException().Message);
        }

        return ProcessResponse(httpMessage);
    }
    private async Task<HttpResponseMessage> ProcessRequestAsync(TRequest request, CancellationToken cancellationToken = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var uri = request.GetUri();

        if (request is IRequestPost p)
        {
            using (var content = p.GetContent())
            {
                return await HttpClient.PostAsync(uri, content, cancellationToken).ConfigureAwait(false);
            }
        }

        return await HttpClient.GetAsync(uri, cancellationToken).ConfigureAwait(false);
    }
    private bool ProcessResponse(HttpResponseMessage httpResponse)
    {
        using (httpResponse)
        {
            if (httpResponse.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new DownloaderException(ErrorCode.HttpError, httpResponse.ReasonPhrase);
            }
        }
    }
}
