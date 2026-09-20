using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon.Common;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Services;
public class OctagonHttpService
{
    private readonly HttpEngine<GetGridRequest, GridResponse, GridResponseContentParser> _getGridEngine;
    private readonly HttpEngine<GetFacetsRequest, FacetsResponse, FacetsResponseContentParser> _getFacetsEngine;
    public OctagonHttpService(HttpClient httpClient)
    {
        _getGridEngine = new HttpEngine<GetGridRequest, GridResponse, GridResponseContentParser>(httpClient);
        _getFacetsEngine = new HttpEngine<GetFacetsRequest, FacetsResponse, FacetsResponseContentParser>(httpClient);
    }
    public async Task<GridResponse> GetGrid() => await _getGridEngine.QueryAsync(new GetGridRequest());
    public async Task<FacetsResponse> GetFacets() => await _getFacetsEngine.QueryAsync(new GetFacetsRequest());
    public async Task<string> GetResponseStringAsync() => await _getGridEngine.GetResponseStringAsync(new GetGridRequest());
}
