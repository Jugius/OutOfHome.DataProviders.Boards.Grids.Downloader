using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon.Common;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon.GetGrid;
public class GetGridResponse : BaseResponse, Interfaces.IContentResponse<ResponseContent>
{
    public ResponseContent Result { get; set; }
    public string RawResponseString { get; set; }
}
