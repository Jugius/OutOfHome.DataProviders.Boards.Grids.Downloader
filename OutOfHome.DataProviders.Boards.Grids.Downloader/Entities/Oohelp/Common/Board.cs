﻿using System.Text.Json.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp.Common;

public class Board
{
    [JsonPropertyName("prov")]
    public string Provider { get; set; }

    [JsonPropertyName("provCode")]
    public string ProviderCode { get; set; }

    [JsonPropertyName("sp")]
    public int SupplierId { get; set; }


    [JsonPropertyName("code")]
    public string SupplierCode { get; set; }


    [JsonPropertyName("city")]
    public int CityId { get; set; }


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
    public int TypeId { get; set; }


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
    public int Price { get; set; }


    [JsonPropertyName("prs")]
    public PricePeriod[] PricePeriods { get; set; }
    
}
