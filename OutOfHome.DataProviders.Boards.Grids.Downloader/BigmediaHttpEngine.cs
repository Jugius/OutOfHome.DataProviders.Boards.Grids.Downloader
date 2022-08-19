using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Bigmedia;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Bigmedia.Common;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Bigmedia.GetGrid;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader;
public class BigmediaHttpEngine
{
    private readonly HttpClient _httpClient;
    public HttpEngine<List<BmaBoard>, GetGridRequest, GetGridResponse, ContentParser> GetGrid { get; }

    public BigmediaHttpEngine(HttpClient httpClient)
    {
        this._httpClient = httpClient;
        this.GetGrid = new HttpEngine<List<BmaBoard>, GetGridRequest, GetGridResponse, ContentParser>(_httpClient);
    }
    public BigmediaHttpEngine() : this(HttpClientFactory.CreateDefaultHttpClient())
    {
    }
}
