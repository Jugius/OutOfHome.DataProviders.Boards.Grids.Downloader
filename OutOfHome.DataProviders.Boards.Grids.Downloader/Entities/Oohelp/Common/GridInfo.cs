using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Common;
using System.Text.Json.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp.Common;
public class GridInfo
{
    public Guid Id { get; set; }
    public ProviderInfo Provider { get; set; }
    public DateTime Downloaded { get; set; }
    public int BoardsCount { get; set; }
}
