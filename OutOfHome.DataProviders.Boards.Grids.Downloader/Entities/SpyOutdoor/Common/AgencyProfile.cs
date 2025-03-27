using System.Text.Json.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.SpyOutdoor.Common;

public class AgencyProfile
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("regionId")]
    public int RegionId { get; set; }

    [JsonPropertyName("townId")]
    public int TownId { get; set; }

    [JsonPropertyName("operatorId")]
    public object OperatorId { get; set; }

    [JsonPropertyName("fields")]
    public List<string> Fields { get; set; }

    [JsonPropertyName("yandexKey")]
    public object YandexKey { get; set; }

    [JsonPropertyName("googleKey")]
    public string GoogleKey { get; set; }

    [JsonPropertyName("fullExcelExport")]
    public int FullExcelExport { get; set; }

    [JsonPropertyName("scoring")]
    public int Scoring { get; set; }

    [JsonPropertyName("language")]
    public string Language { get; set; }

    [JsonPropertyName("slogan")]
    public string Slogan { get; set; }

    [JsonPropertyName("logo")]
    public string Logo { get; set; }

    [JsonPropertyName("address")]
    public string Address { get; set; }

    [JsonPropertyName("phone")]
    public string Phone { get; set; }

    [JsonPropertyName("phone2")]
    public string Phone2 { get; set; }

    [JsonPropertyName("phone3")]
    public string Phone3 { get; set; }

    [JsonPropertyName("site")]
    public string Site { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("logoFile")]
    public string LogoFile { get; set; }

    [JsonPropertyName("showHeader")]
    public int ShowHeader { get; set; }

    [JsonPropertyName("showFooter")]
    public int ShowFooter { get; set; }

    [JsonPropertyName("showHelp")]
    public int ShowHelp { get; set; }

    [JsonPropertyName("showBusyBoards")]
    public int ShowBusyBoards { get; set; }

    [JsonPropertyName("counterCode")]
    public object CounterCode { get; set; }

    [JsonPropertyName("agency")]
    public Agency Agency { get; set; }
}
