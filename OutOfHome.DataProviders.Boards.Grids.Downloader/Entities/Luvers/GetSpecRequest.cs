using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Luvers;
internal class GetSpecRequest : IRequest
{
    private const string uri = @"https://db.luvers.com.ua/spec.php";
    public Uri GetUri() => new Uri(uri);
}