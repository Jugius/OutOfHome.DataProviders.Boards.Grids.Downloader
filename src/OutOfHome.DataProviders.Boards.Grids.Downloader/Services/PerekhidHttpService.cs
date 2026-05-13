using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Perekhid;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Services;
public class PerekhidHttpService : AdvVgHttpService<GetAdvVgGridRequest>
{    
    private readonly HttpEngine<GetGridRequest, Entities.AdvVg.Common.ResponseContent, Entities.AdvVg.ContentParser> _getGridEngine;
    public PerekhidHttpService(HttpClient httpClient)
    {
        _getGridEngine = new HttpEngine<GetGridRequest, Entities.AdvVg.Common.ResponseContent, Entities.AdvVg.ContentParser>(httpClient);
    }
    public PerekhidHttpService() : this(HttpClientFactory.CreateDefaultHttpClient())
    {
    }    
    public async Task<Entities.AdvVg.Common.ResponseContent> GetGrid() => await _getGridEngine.QueryAsync(new GetGridRequest());
}
