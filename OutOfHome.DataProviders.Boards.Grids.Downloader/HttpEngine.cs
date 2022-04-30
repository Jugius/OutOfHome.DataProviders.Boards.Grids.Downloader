using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Common.Enums;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;
using System;
using System.Net;
using System.Net.Http;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader;

public abstract class HttpEngine : IDisposable
{
    private static HttpClient httpClient;
    private static readonly TimeSpan httpTimeout = new TimeSpan(0, 0, 30);
    protected internal static HttpClient HttpClient
    {
        get
        {
            if (HttpEngine.httpClient == null)
            {
                var httpClientHandler = new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                    CookieContainer = new CookieContainer()
                };

                HttpEngine.httpClient = new HttpClient(httpClientHandler)
                {
                    Timeout = HttpEngine.httpTimeout
                };

            }

            return HttpEngine.httpClient;
        }
        set => HttpEngine.httpClient = value;
    }
    public virtual void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Disposes of the <see cref="HttpClient"/>, if <paramref name="disposing"/> is true.
    /// </summary>
    /// <param name="disposing">Whether to dispose resources or not.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposing)
            return;

        HttpEngine.HttpClient?.Dispose();
        HttpEngine.httpClient = null;
    }

}
public sealed class HttpEngine<TRequest, TResponse, TParser> : HttpEngine
        where TRequest : IRequest, new()
        where TResponse : IResponse, new()
        where TParser : IContentDeserializer<TResponse>, new()
{
    internal static readonly HttpEngine<TRequest, TResponse, TParser> instance = new HttpEngine<TRequest, TResponse, TParser>();

    public async Task<TResponse> QueryAsync(TRequest request, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage httpMessage = await ProcessRequestAsync(request, cancellationToken).ConfigureAwait(false);

        var response = await ProcessResponseAsync(httpMessage).ConfigureAwait(false);

        return response;

    }
    internal async Task<HttpResponseMessage> ProcessRequestAsync(TRequest request, CancellationToken cancellationToken = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var uri = request.GetUri();

        if (request is IRequestPost p)
        {
            using (var content = p.GetContent())
            {
                return await HttpEngine.HttpClient.PostAsync(uri, content, cancellationToken).ConfigureAwait(false);
            }                
        }

        return await HttpEngine.HttpClient.GetAsync(uri, cancellationToken).ConfigureAwait(false);
    }
    internal async Task<TResponse> ProcessResponseAsync(HttpResponseMessage httpResponse)
    {
        if (httpResponse == null)
            throw new ArgumentNullException(nameof(httpResponse));

        using (httpResponse)
        {
            httpResponse.EnsureSuccessStatusCode();

            var response = new TResponse();

            if (response is IResponseContent)
            {
                TParser parser = new TParser();
                response = await parser.DeserializeAsync(httpResponse).ConfigureAwait(false);
            }

            response.RawQueryString = httpResponse.RequestMessage.RequestUri.PathAndQuery;
            response.Status = httpResponse.IsSuccessStatusCode
                ? response.Status ?? Status.Ok
                : Status.HttpError;

            return response;
        }
    }


}
