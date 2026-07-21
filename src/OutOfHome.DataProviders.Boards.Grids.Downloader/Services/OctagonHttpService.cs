using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon.Common;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Services;
public class OctagonHttpService
{
    private readonly HttpClient _httpClient;
    private readonly HttpEngine<GetGridRequest, OctagonBoard[], ContentParser> _getGridEngine;
    public OctagonHttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _getGridEngine = new HttpEngine<GetGridRequest, OctagonBoard[], ContentParser>(_httpClient);
    }
    public async Task<OctagonBoard[]> GetGrid() => await _getGridEngine.QueryAsync(new GetGridRequest());
    public async Task<string> GetResponseStringAsync() => await _getGridEngine.GetResponseStringAsync(new GetGridRequest());
}
