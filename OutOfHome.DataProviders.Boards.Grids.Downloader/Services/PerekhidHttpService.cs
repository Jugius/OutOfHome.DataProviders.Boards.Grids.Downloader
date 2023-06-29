using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Perekhid;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Perekhid.Common;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Services;
public class PerekhidHttpService
{
    private readonly HttpClient _httpClient;
    private readonly HttpEngine<GetGridRequest, ResponseContent, ContentParser> _getGridEngine;
    public PerekhidHttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _getGridEngine = new HttpEngine<GetGridRequest, ResponseContent, ContentParser>(_httpClient);
    }
    public PerekhidHttpService() : this(HttpClientFactory.CreateDefaultHttpClient())
    {
    }
    public async Task<ResponseContent> GetGrid(GetGridRequest request) => await _getGridEngine.QueryAsync(request);
}
