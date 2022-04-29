using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Common.Enums;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities
{
    public class BaseGridResponse : BaseResponse, Interfaces.IResponseContent
    {
        public string RawResponseString { get; set; }
        public int Downloaded { get; set; }
        public Language Language { get; set; }
    }
}
