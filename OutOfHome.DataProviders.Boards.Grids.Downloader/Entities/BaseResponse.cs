using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Common.Enums;
using System.Text.Json.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities;

public class BaseResponse : Interfaces.IResponse
{
    public string RawQueryString { get; set; }

    public Status Status { get; set; }

    public string ErrorMessage { get; set; }
}
