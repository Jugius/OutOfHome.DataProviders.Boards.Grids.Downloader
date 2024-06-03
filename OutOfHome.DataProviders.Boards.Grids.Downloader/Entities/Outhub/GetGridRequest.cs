using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Outhub;

public class GetGridRequest : IRequestPost
{
    private const string baseUri = "https://booking.outhub.online/api/booking/GetBoardsData";


    public Uri GetUri() => new Uri(baseUri);
    public HttpContent GetContent() => null;
}
