using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.SpyOutdoor;
public class GetGridRequest : IRequest
{
    private const string uriBase = "https://igrok.spyoutdoor.com";
    public Uri GetUri() => new Uri(uriBase);
}
