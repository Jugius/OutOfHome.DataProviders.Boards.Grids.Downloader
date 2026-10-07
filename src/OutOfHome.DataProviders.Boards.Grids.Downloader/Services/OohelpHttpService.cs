using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp.Common;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Services;
public class OohelpHttpService(HttpClient httpClient)
{
    private readonly HttpEngine<GetGridRequest, ResponseContent, GetGridResponseContentConverter> _getGridEngine = new(httpClient);
    private readonly HttpEngine<GetActualGridsRequest, List<GridInfo>, GetActualGridsResponseContentConverter> _getActualGridsEngine = new(httpClient);


    public async Task<ResponseContent> GetGrid(GetGridRequest request) => await _getGridEngine.QueryAsync(request);
    public async Task<List<GridInfo>> GetActualGrids(GetActualGridsRequest request) => await _getActualGridsEngine.QueryAsync(request);
}
