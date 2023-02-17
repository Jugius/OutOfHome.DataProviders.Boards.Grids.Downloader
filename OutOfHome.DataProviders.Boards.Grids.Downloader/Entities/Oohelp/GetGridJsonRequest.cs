using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp;

public class GetGridJsonRequest : IRequest
{
    private const string baseUri = "https://boards.oohelp.net/api/grids/download/json";
    public Uri GetUri() => new Uri(baseUri);
}
