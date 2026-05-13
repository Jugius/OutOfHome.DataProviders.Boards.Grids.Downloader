using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.SpyOutdoor; 
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.SpyOutdoor.Common;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Services;
public class SpyOutdoorHttpService
{
    private readonly HttpClient _httpClient;
    private readonly HttpEngine<GetGridRequest, ResponseContent, BaseResponseContentParser> _getGridEngine;
    private readonly HttpEngine<GetBoardsRequest, BoardsTable, GeetBoardsResponseParser> _getBoardsEngine;

    public SpyOutdoorHttpService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _getGridEngine = new HttpEngine<GetGridRequest, ResponseContent, BaseResponseContentParser>(_httpClient);
        _getBoardsEngine = new HttpEngine<GetBoardsRequest, BoardsTable, GeetBoardsResponseParser>(_httpClient);
    }
    public SpyOutdoorHttpService() : this(HttpClientFactory.CreateDefaultHttpClient()) { }
    public async Task<ResponseContent> GetGrid(GetGridRequest request) => await this._getGridEngine.QueryAsync(request);
    public async Task<BoardsTable> GetBoards(GetBoardsRequest request) => await _getBoardsEngine.QueryAsync(request);
    public async Task<string> ReadResponseAsString(GetGridRequest request) => await _getGridEngine.GetResponseStringAsync(request);
    public async Task<string> ReadResponseAsString(GetBoardsRequest request) => await _getBoardsEngine.GetResponseStringAsync(request);
}
