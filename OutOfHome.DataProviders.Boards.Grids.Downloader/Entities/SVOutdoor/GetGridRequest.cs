using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.SVOutdoor;
internal class GetGridRequest : IRequest
{
    public Uri GetUri() => new Uri(@"https://i.sv-outdoor.com/upload/partners/86/export/sv-outdoor.xlsx");
}
