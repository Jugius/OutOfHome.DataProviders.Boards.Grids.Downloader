using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.SVOutdoor;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Services;
public class SVOutdoorHttpService : AdvVgHttpService<GetAdvVgGridRequest>
{    
    private readonly HttpEngine<GetGridRequest, Entities.AdvVg.Common.ResponseContent, Entities.AdvVg.ContentParser> _getGridEngine;

    public SVOutdoorHttpService(HttpClient httpClient)
    {
        _getGridEngine = new HttpEngine<GetGridRequest, Entities.AdvVg.Common.ResponseContent, Entities.AdvVg.ContentParser>(httpClient);
    }
    public SVOutdoorHttpService() : this(HttpClientFactory.CreateDefaultHttpClient())
    {
    }

    public async Task<Entities.AdvVg.Common.ResponseContent> GetGrid() => await _getGridEngine.QueryAsync(new GetGridRequest());

}
