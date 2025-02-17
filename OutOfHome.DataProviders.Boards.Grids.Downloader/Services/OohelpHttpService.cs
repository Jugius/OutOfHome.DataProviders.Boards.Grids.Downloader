using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp.Common;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Services;
public class OohelpHttpService
{
    private readonly HttpClient _httpClient;
    private readonly HttpEngine<GetGridRequest, ResponseContent, GetGridResponseContentConverter> _getGridEngine;
    private readonly HttpEngine<GetActualGridsRequest, List<GridInfo>, GetActualGridsResponseContentConverter> _getActualGridsEngine;
    private readonly HttpEngine<AuthenticateRequest, string, AuthenticateResponseContentConverter> _authenticateEngine;

    public OohelpHttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _getGridEngine = new HttpEngine<GetGridRequest, ResponseContent, GetGridResponseContentConverter>(_httpClient);
        _getActualGridsEngine = new HttpEngine<GetActualGridsRequest, List<GridInfo>, GetActualGridsResponseContentConverter>(_httpClient);
        _authenticateEngine = new HttpEngine<AuthenticateRequest, string, AuthenticateResponseContentConverter>(_httpClient);
    }
    public OohelpHttpService() : this(HttpClientFactory.CreateDefaultHttpClient())
    {
    }

    public async Task<ResponseContent> GetGrid(GetGridRequest request) => await _getGridEngine.QueryAsync(request);
    public async Task<List<GridInfo>> GetActualGrids(GetActualGridsRequest request) => await _getActualGridsEngine.QueryAsync(request);
    public async Task<string> Authenticate(AuthenticateRequest request)
    { 
        var token = await _authenticateEngine.QueryAsync(request);
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        return token;
    }

}
