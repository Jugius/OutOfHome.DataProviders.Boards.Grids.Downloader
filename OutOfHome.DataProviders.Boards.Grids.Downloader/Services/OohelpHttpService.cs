using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp.Common;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Services;
public class OohelpHttpService
{
    private readonly HttpClient _httpClient;
    private readonly HttpEngine<GetGridRequest, ResponseContent, ZipContentParser> _getGridEngine;
    private readonly HttpEngine<GetActualGridsRequest, List<GridInfo>, GetActualGridsContentParser> _getActualGridsEngine;

    public OohelpHttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _getGridEngine = new HttpEngine<GetGridRequest, ResponseContent, ZipContentParser>(_httpClient);
        _getActualGridsEngine = new HttpEngine<GetActualGridsRequest, List<GridInfo>, GetActualGridsContentParser>(_httpClient);
    }
    public OohelpHttpService() : this(HttpClientFactory.CreateDefaultHttpClient())
    {
    }

    public async Task<ResponseContent> GetGrid(GetGridRequest request) => await _getGridEngine.QueryAsync(request);
    public async Task<List<GridInfo>> GetActualGrids(GetActualGridsRequest request) => await _getActualGridsEngine.QueryAsync(request);

}
