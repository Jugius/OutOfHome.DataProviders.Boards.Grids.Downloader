using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Common.Enums;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader;

public sealed class HttpEngine<TBody, TRequest, TResponse, TParser>
        where TBody : new()
        where TRequest : IRequest, new()
        where TResponse : IContentResponse<TBody>, new()
        where TParser : IContentParser<TBody>, new()
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

    public async Task<TResponse> QueryAsync(TRequest request, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage httpMessage;

        try
        {
            httpMessage = await ProcessRequestAsync(request, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return new TResponse { Status = Status.RequestError, ErrorMessage = ex.GetBaseException().Message };
        }

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
                return await this.HttpClient.PostAsync(uri, content, cancellationToken).ConfigureAwait(false);
            }                
        }

        return await this.HttpClient.GetAsync(uri, cancellationToken).ConfigureAwait(false);
    }
    internal async Task<TResponse> ProcessResponseAsync(HttpResponseMessage httpResponse)
    {
        var response = new TResponse();
        response.RawQueryString = httpResponse.RequestMessage.RequestUri.PathAndQuery;

        using (httpResponse)
        {
            if (httpResponse.IsSuccessStatusCode)
            {
                TParser parser = new TParser();
                try
                {
                    response.Result = await parser.ParseContent(httpResponse);
                    response.Status = Status.Ok;
                }
                catch (Exception ex)
                {
                    response.Status = Status.ContentParsingError;
                    response.ErrorMessage = ex.Message;
                }                
            }
            else
            {
                response.Status = Status.HttpError;
                response.ErrorMessage = httpResponse.ReasonPhrase;                
            }
        }
        return response;
    }


}
public sealed class HttpEngine<TRequest, TResponse>
        where TRequest : IRequest, new()
        where TResponse : IResponse, new()
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

    public async Task<TResponse> QueryAsync(TRequest request, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage httpMessage;

        try
        {
            httpMessage = await ProcessRequestAsync(request, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return new TResponse { Status = Status.RequestError, ErrorMessage = ex.GetBaseException().Message };
        }

        return ProcessResponse(httpMessage);
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
                return await this.HttpClient.PostAsync(uri, content, cancellationToken).ConfigureAwait(false);
            }
        }

        return await this.HttpClient.GetAsync(uri, cancellationToken).ConfigureAwait(false);
    }
    internal TResponse ProcessResponse(HttpResponseMessage httpResponse)
    {
        using (httpResponse)
        {
            var response = new TResponse();
            response.RawQueryString = httpResponse.RequestMessage.RequestUri.PathAndQuery;

            if (httpResponse.IsSuccessStatusCode)
            { 
                response.Status = Status.Ok;
            }
            else
            {
                response.Status = Status.HttpError;
                response.ErrorMessage = httpResponse.ReasonPhrase;
            }
            
            return response;            
        }
    }
}
