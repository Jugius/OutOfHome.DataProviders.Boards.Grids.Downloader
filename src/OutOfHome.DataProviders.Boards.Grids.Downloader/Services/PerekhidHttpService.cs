using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Perekhid;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Services;

public class PerekhidHttpService : IAdvVgHttpService
{
    private readonly AdvVgHttpService<GetGridRequest> servise = new AdvVgHttpService<GetGridRequest>();
    public Task<Entities.AdvVg.Common.ResponseContent> GetGrid() => servise.GetGrid();
}