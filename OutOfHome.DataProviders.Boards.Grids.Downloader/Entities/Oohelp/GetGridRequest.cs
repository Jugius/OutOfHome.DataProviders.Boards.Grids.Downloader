using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp;
public class GetGridRequest : IRequest
{
    private const string baseUri = "https://boards.oohelp.net/api/grids/download/zip";
    public Uri GetUri() => new Uri(baseUri);
}
