namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;
public interface IAdvVgHttpService
{
    Task<AdvVg.Common.ResponseContent> GetGrid();
}
