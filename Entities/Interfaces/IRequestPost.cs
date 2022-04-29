using System.Net.Http;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;

public interface IRequestPost : IRequest
{
    HttpContent GetContent();
}
