using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Bigmedia;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Bigmedia.Common;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Services;
public class BigmediaHttpService
{
    private readonly HttpClient _httpClient;
    private readonly HttpEngine<GetGridRequest, List<Board>, ContentParser> _getGridEngine;
    private readonly HttpEngine<AuthenticateRequest> _authenticateEngine;

    public BigmediaHttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _getGridEngine = new HttpEngine<GetGridRequest, List<Board>, ContentParser>(_httpClient);
        _authenticateEngine = new HttpEngine<AuthenticateRequest>(_httpClient);
    }
    public BigmediaHttpService() : this(HttpClientFactory.CreateDefaultHttpClient())
    {
    }
    public async Task<List<Board>> GetGrid(GetGridRequest request) => await _getGridEngine.QueryAsync(request);
    public async Task<bool> Authenticate(AuthenticateRequest request) => await _authenticateEngine.QueryAsync(request);
}
