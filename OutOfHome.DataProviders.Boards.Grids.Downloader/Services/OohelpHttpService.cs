using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp.Common;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Services;
public class OohelpHttpService
{
    private readonly HttpClient _httpClient;
    private readonly HttpEngine<GetGridJsonRequest, ResponseContent, ContentParser> _getGridJsonEngine;
    private readonly HttpEngine<GetGridZipRequest, ResponseContent, ZipContentParser> _getGridZipEngine;

    public OohelpHttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _getGridJsonEngine = new HttpEngine<GetGridJsonRequest, ResponseContent, ContentParser>(_httpClient);
        _getGridZipEngine = new HttpEngine<GetGridZipRequest, ResponseContent, ZipContentParser>(_httpClient);
    }
    public OohelpHttpService() : this(HttpClientFactory.CreateDefaultHttpClient())
    {
    }

    public async Task<ResponseContent> GetGrid(GetGridJsonRequest request) => await _getGridJsonEngine.QueryAsync(request);
    public async Task<ResponseContent> GetGrid(GetGridZipRequest request) => await _getGridZipEngine.QueryAsync(request);


}
