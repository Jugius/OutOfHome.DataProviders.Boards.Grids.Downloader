using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Exceptions;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Services;

internal sealed class HttpEngine<TRequest, TResult, TParser>
    where TRequest : IRequest, new()
    where TResult : notnull
    where TParser : IResponseConverter<TResult>, new()
{
    private readonly HttpClient httpClient;
    
    public HttpEngine(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }
    public HttpEngine() : this(HttpClientFactory.CreateDefaultHttpClient()) { }

    public async Task<TResult> QueryAsync(TRequest request, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage httpMessage;

        try
        {
            httpMessage = await ProcessRequestAsync(request, cancellationToken).ConfigureAwait(false);
        }
        catch (DownloaderException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new DownloaderException(ErrorCode.HttpError, ex.GetBaseException().Message);
        }

        var response = await ProcessResponseAsync(httpMessage).ConfigureAwait(false);

        return response;
    }
    public async Task<string> GetResponseStringAsync(TRequest request, CancellationToken cancellationToken = default)
    {
        using (HttpResponseMessage httpMessage = await ProcessRequestAsync(request, cancellationToken).ConfigureAwait(false))
        {
            if (httpMessage.IsSuccessStatusCode)
            {
                return await httpMessage.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            }
            else
            {
                throw new DownloaderException(ErrorCode.HttpError, httpMessage.ReasonPhrase);
            }
        }
    }
    public async Task<byte[]> GetResponseBytesAsync(TRequest request, CancellationToken cancellationToken = default)
    {
        using (HttpResponseMessage httpMessage = await ProcessRequestAsync(request, cancellationToken).ConfigureAwait(false))
        {
            if (httpMessage.IsSuccessStatusCode)
            {
                return await httpMessage.Content.ReadAsByteArrayAsync(cancellationToken).ConfigureAwait(false);
            }
            else
            {
                throw new DownloaderException(ErrorCode.HttpError, httpMessage.ReasonPhrase);
            }
        }
    }
    private async Task<HttpResponseMessage> ProcessRequestAsync(TRequest request, CancellationToken cancellationToken = default)
    {
        using var message = BuildRequestMessage(request);
        try
        {
            using (var message = BuildRequestMessage(request))
            {
                return await httpClient.SendAsync(message, cancellationToken).ConfigureAwait(false);
            }
        catch (HttpRequestException httpUnavailable)
        catch (Exception ex)
        {
            var m = $"Connection error to '{message.RequestUri}': {httpUnavailable.Message}";
            throw new DownloaderException(ErrorCode.HttpError, m);
        }
        
    }
    private async Task<TResult> ProcessResponseAsync(HttpResponseMessage httpResponse)
    {
        using (httpResponse)
        {
            if (httpResponse.IsSuccessStatusCode)
            {
                TParser parser = new TParser();
                return await parser.Convert(httpResponse);
            }
            else
            {
                throw new DownloaderException(ErrorCode.HttpError, httpResponse.ReasonPhrase);
            }
        }
    }
    private static HttpRequestMessage BuildRequestMessage(TRequest request)
    {
        var uri = request.GetUri();
        if (request is IRequestPost post)
        {
            return new HttpRequestMessage(HttpMethod.Post, uri) { Content = post.GetContent() };
        }
        return new HttpRequestMessage(HttpMethod.Get, uri);
    }

}