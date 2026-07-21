using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;
using System.Net;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Outhub;

internal class GetGridRequest : IRequestPost, ICookieContainerRequest
{
    private const string baseUri = "https://booking.outhub.online/api/booking/GetBoardsData";

    public List<Cookie> Cookies { get; } = [];
    public Uri GetUri() => new Uri(baseUri);
    public HttpContent GetContent() => null;
}
