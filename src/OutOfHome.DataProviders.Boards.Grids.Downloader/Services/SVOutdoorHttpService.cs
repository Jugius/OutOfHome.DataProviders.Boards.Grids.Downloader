using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.AdvVg.Common;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.SVOutdoor;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Services;

public class SVOutdoorHttpService : IAdvVgHttpService
{
    private readonly AdvVgHttpService<GetGridRequest> service = new();

    public Task<ResponseContent> GetGrid() => service.GetGrid();
}