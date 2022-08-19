using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Outhub.Common;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Outhub.GetGrid;

public class GetGridResponse : BaseResponse, Interfaces.IContentResponse<ResponseContent>
{
    public ResponseContent Result { get; set; }
    public string RawResponseString { get; set; }
}
