
namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;

internal interface IRequestPost : IRequest
{
    HttpContent GetContent();
}
