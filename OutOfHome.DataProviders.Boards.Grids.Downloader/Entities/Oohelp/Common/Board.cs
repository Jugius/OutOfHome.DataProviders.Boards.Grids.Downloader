using System.Text.Json.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp.Common
{
    public class Board
    {
        //[JsonProperty("prov")]
        //public GridProvider Provider { get; set; }
        //[JsonPropertyName("provId")]
        //public string ProviderID { get; set; }


        [JsonPropertyName("sp")]
        public string Supplier { get; set; }


        [JsonPropertyName("code")]
        public string SupplierCode { get; set; }


        [JsonPropertyName("city")]
        public Guid CityID { get; set; }


        [JsonPropertyName("dist")]
        public string District { get; set; }


        [JsonPropertyName("str")]
        public string Street { get; set; }


        [JsonPropertyName("strN")]
        public string StreetNumber { get; set; }


        [JsonPropertyName("strD")]
        public string StreetDescription { get; set; }


        [JsonPropertyName("side")]
        public string Side { get; set; }


        [JsonPropertyName("size")]
        public string Size { get; set; }


        [JsonPropertyName("type")]
        public string Type { get; set; }


        [JsonPropertyName("lig")]
        public byte? Lighting { get; set; }


        [JsonPropertyName("lat")]
        public double Latitude { get; set; }


        [JsonPropertyName("lon")]
        public double Longitude { get; set; }


        [JsonPropertyName("ang")]
        public int? Angle { get; set; }


        [JsonPropertyName("dix")]
        public int? DoorsDix { get; set; }


        [JsonPropertyName("ots")]
        public int? Ots { get; set; }


        [JsonPropertyName("grp")]
        public float? Grp { get; set; }


        [JsonPropertyName("photo")]
        public string Photo { get; set; }
        
        
        [JsonPropertyName("map")]
        public string Map { get; set; }


        [JsonPropertyName("st")]
        public DateOnly VisibleStart { get; set; }


        [JsonPropertyName("end")]
        public DateOnly VisibleEnd { get; set; }


        [JsonPropertyName("occ")]
        public OccupationPeriod[] OccupationPeriods { get; set; }


        [JsonPropertyName("pr_const")]
        public byte? PriceIsConstant { get; set; }


        [JsonPropertyName("pr")]
        public float Price { get; set; }


        [JsonPropertyName("prs")]
        public PricePeriod[] PricePeriods { get; set; }
        
    }
}
