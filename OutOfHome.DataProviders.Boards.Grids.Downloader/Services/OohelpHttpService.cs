using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp.Common;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Services;
public class OohelpHttpService
{
    private readonly HttpClient _httpClient;
    private readonly HttpEngine<GetGridExtendedRequest, ResponseContent, ZipContentParser> _getGridExtendedEngine;
    private readonly HttpEngine<GetGridRequest, ResponseContent, ZipContentParser> _getGridEngine;

    public OohelpHttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _getGridJsonEngine = new HttpEngine<GetGridJsonRequest, ResponseContent, ContentParser>(_httpClient);
        _getGridZipEngine = new HttpEngine<GetGridZipRequest, ResponseContent, ZipContentParser>(_httpClient);
    }
    public OohelpHttpService() : this(HttpClientFactory.CreateDefaultHttpClient())
    {
    }

    public async Task<ResponseContent> GetGrid(GetGridZipRequest request) => await _getGridZipEngine.QueryAsync(request);


}
