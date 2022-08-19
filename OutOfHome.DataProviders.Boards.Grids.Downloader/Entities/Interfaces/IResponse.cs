using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Common.Enums;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;

public interface IResponse
{
    string RawQueryString { get; set; }
    Status Status { get; set; }
    string ErrorMessage { get; set; }
}
