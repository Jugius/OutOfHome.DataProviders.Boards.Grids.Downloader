
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Bigmedia.Common;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Bigmedia.GetGrid;

public class GetGridResponse : BaseResponse, Interfaces.IContentResponse<List<BmaBoard>>
{
    public List<BmaBoard> Result { get; set; }
    public string RawResponseString { get; set; }
}
