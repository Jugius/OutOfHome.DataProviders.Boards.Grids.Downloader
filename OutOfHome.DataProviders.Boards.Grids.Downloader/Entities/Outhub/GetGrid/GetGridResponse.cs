using System.Text.Json.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Outhub.GetGrid
{
    public class GetGridResponse : BaseGridResponse
    {
        [JsonPropertyName("items")]
        public string[] Boards { get; set; }

        [JsonPropertyName("status")]
        public int ServerStatus { get; set; }

        [JsonPropertyName("dateFromSec")]
        public int DateFromSec { get; set; }

        [JsonPropertyName("dateToSec")]
        public int DateToSec { get; set; }
    }
}
