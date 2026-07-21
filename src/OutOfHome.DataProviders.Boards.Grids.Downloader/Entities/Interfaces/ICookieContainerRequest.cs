using System.Net;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;

internal interface ICookieContainerRequest
{
    List<Cookie> Cookies { get; }
}
