using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Outhub;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Outhub.Common;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Services;

public class OuthubHttpService
{
    private readonly HttpClient _httpClient;
    private readonly HttpEngine<GetGridRequest, ResponseContent, ContentParser> _getGridEngine;

    public OuthubHttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _getGridEngine = new HttpEngine<GetGridRequest, ResponseContent, ContentParser>(_httpClient);
    }
    public OuthubHttpService() : this(HttpClientFactory.CreateDefaultHttpClient())
    {
    }
    public async Task<ResponseContent> GetGrid(GetGridRequest request) => await _getGridEngine.QueryAsync(request);
}
