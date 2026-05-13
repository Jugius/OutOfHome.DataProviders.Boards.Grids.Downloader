using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Exceptions;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Services;

internal sealed class HttpEngine<TRequest, TResult, TParser>
    where TRequest : IRequest
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
        using HttpResponseMessage httpMessage = await ProcessRequestAsync(request, cancellationToken).ConfigureAwait(false);
        await ThrowIfNotSuccessStatusCode(httpMessage);
        return await ProcessResponseAsync(httpMessage).ConfigureAwait(false);
    }
    public async Task<string> GetResponseStringAsync(TRequest request, CancellationToken cancellationToken = default)
    {
        using HttpResponseMessage httpMessage = await ProcessRequestAsync(request, cancellationToken).ConfigureAwait(false);
        await ThrowIfNotSuccessStatusCode(httpMessage);
        return await httpMessage.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
    }
    public async Task<byte[]> GetResponseBytesAsync(TRequest request, CancellationToken cancellationToken = default)
    {
        using HttpResponseMessage httpMessage = await ProcessRequestAsync(request, cancellationToken).ConfigureAwait(false);
        await ThrowIfNotSuccessStatusCode(httpMessage);
        return await httpMessage.Content.ReadAsByteArrayAsync(cancellationToken).ConfigureAwait(false);
    }
    private async Task<HttpResponseMessage> ProcessRequestAsync(TRequest request, CancellationToken cancellationToken = default)
    {
        using var message = BuildRequestMessage(request);
        try
        {
            return await httpClient.SendAsync(message, cancellationToken).ConfigureAwait(false);
        }
        catch (HttpRequestException httpUnavailable)
        {
            var m = $"Connection error to '{message.RequestUri}': {httpUnavailable.Message}";
            throw new DownloaderException(ErrorCode.HttpError, m);
        }        
    }
    private static async Task<TResult> ProcessResponseAsync(HttpResponseMessage httpResponse)
    {
        TParser parser = new TParser();
        return await parser.Convert(httpResponse);
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
    private static async Task ThrowIfNotSuccessStatusCode(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var message = $"Response error from '{response.RequestMessage.RequestUri}': StatusCode: {response.StatusCode}";            

            var content = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(content))
                message += $", Content: {content}";

            throw new DownloaderException(ErrorCode.ServerError, message);
        }
    }

}