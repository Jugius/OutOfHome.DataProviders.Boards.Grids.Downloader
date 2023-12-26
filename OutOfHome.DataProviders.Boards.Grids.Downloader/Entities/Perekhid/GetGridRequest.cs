using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Perekhid;
internal class GetGridRequest : IRequest
{
    public Uri GetUri() => new Uri(@"https://base.perekhid-outdoor.com.ua/upload/partners/24/export/perekhid-outdoor.xlsx");
}
