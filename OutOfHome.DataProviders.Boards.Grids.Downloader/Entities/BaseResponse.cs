using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Common.Enums;
using System.Text.Json.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities
{
    public class BaseResponse : Interfaces.IResponse
    {
        [JsonIgnore]
        public string RawQueryString { get; set; }

        [JsonIgnore]
        public Status? Status { get; set; }

        [JsonIgnore]
        public string ErrorMessage { get; set; }
    }
}
