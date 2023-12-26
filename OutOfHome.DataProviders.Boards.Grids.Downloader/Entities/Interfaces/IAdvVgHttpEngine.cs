
namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Services;
public interface IAdvVgHttpEngine
{
    Task<Entities.AdvVg.Common.ResponseContent> GetAdvVgGrid();
}
