using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon.Common;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Services;
public class OctagonHttpService
{
    private readonly HttpEngine<GetGridRequest, GridResponse, GridResponseContentParser> _getGridEngine;
    private readonly HttpEngine<GetGridRequest, OctagonBoard[], ContentParser> _getGridEngine;
    public OctagonHttpService(HttpClient httpClient)
    {
        _getGridEngine = new HttpEngine<GetGridRequest, GridResponse, GridResponseContentParser>(httpClient);
        _getGridEngine = new HttpEngine<GetGridRequest, OctagonBoard[], ContentParser>(_httpClient);
    }
    public async Task<GridResponse> GetGrid() => await _getGridEngine.QueryAsync(new GetGridRequest());
    public async Task<string> GetResponseStringAsync() => await _getGridEngine.GetResponseStringAsync(new GetGridRequest());
}
