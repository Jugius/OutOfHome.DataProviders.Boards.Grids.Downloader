using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Common.Enums;
using System.Text.Json.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities
{
    public class BaseGridResponse : BaseResponse, Interfaces.IResponseContent
    {
        [JsonIgnore]
        public string RawResponseString { get; set; }
        public int Downloaded { get; set; }
        public Language Language { get; set; }
    }
}
