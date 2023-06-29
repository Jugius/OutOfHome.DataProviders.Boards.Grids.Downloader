using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Perekhid;
public class GetGridRequest : IRequest
{
    private const string baseUri = "https://base.perekhid-outdoor.com.ua/upload/partners/24/export/perekhid-outdoor.xlsx";

    public Uri GetUri() => new Uri(baseUri);
}
