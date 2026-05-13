using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Common;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Services;

public class DownloadFileHttpService
{
    private readonly HttpEngine<DownloadFileRequest, FileResponse, FileContentParser> _httpEngine;
    public DownloadFileHttpService(HttpClient httpClient)
    {
        _httpEngine = new HttpEngine<DownloadFileRequest, FileResponse, FileContentParser>(httpClient);
    }
    public DownloadFileHttpService() : this(HttpClientFactory.CreateDefaultHttpClient()) { }
    public async Task<FileResponse> DownloadFile(DownloadFileRequest request, CancellationToken cancellationToken = default) =>
        await _httpEngine.QueryAsync(request, cancellationToken);
}
