using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Outhub;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Outhub.Common;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Outhub.GetGrid;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader;

public class OuthubHttpEngine
{
    private readonly HttpClient _httpClient;
    public HttpEngine<ResponseContent, GetGridRequest, GetGridResponse, ContentParser> GetGrid { get; }

    public OuthubHttpEngine(HttpClient httpClient)
    {        
        this._httpClient = httpClient;
        this.GetGrid = new HttpEngine<ResponseContent, GetGridRequest, GetGridResponse, ContentParser>(_httpClient);
    }
    public OuthubHttpEngine() : this(HttpClientFactory.CreateDefaultHttpClient())
    {
    }    
}
