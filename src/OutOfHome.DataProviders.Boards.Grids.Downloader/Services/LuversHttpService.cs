using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Luvers;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Services;
public class LuversHttpService
{
    private readonly HttpClient _httpClient;
    private readonly HttpEngine<GetSidesRequest, string[], ContentParser> _getSidesEngine;
    private readonly HttpEngine<GetSpecRequest, string[], ContentParser> _getSpecEngine;

    public LuversHttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _getSidesEngine = new HttpEngine<GetSidesRequest, string[], ContentParser>(_httpClient);
        _getSpecEngine = new HttpEngine<GetSpecRequest, string[], ContentParser>(_httpClient);
    }
    public LuversHttpService() : this(HttpClientFactory.CreateDefaultHttpClient())
    {
    }

    public async Task<string[]> GetSides() => await this._getSidesEngine.QueryAsync(new GetSidesRequest());
    public async Task<string[]> GetSpec() => await this._getSpecEngine.QueryAsync(new GetSpecRequest());
}
