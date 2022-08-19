using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon.Common;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon.GetGrid;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader;
public class OctagonHttpEngine
{
    private readonly HttpClient _httpClient;
    public HttpEngine<ResponseContent, GetGridRequest, GetGridResponse, ContentParser> GetGrid { get; }
    public OctagonHttpEngine(HttpClient httpClient)
    {
        this._httpClient = httpClient;
        this.GetGrid = new HttpEngine<ResponseContent, GetGridRequest, GetGridResponse, ContentParser>(_httpClient);
    }
    public OctagonHttpEngine() : this(HttpClientFactory.CreateDefaultHttpClient())
    {
    }
}
