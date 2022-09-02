using System.Net.Http;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;

internal interface IRequestPost : IRequest
{
    HttpContent GetContent();
}
