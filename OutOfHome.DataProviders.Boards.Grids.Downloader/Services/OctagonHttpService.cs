using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon.Common;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Services;
public class OctagonHttpService
{
    private readonly HttpClient _httpClient;
    private readonly HttpEngine<GetGridRequest, ResponseContent, ContentParser> _getGridEngine;
    public OctagonHttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _getGridEngine = new HttpEngine<GetGridRequest, ResponseContent, ContentParser>(_httpClient);
    }
    public OctagonHttpService() : this(HttpClientFactory.CreateDefaultHttpClient())
    {
    }
    public async Task<ResponseContent> GetGrid(GetGridRequest request) => await _getGridEngine.QueryAsync(request);
    public async Task<string> GetResponseStringAsync(GetGridRequest request) => await _getGridEngine.GetResponseStringAsync(request);
}
