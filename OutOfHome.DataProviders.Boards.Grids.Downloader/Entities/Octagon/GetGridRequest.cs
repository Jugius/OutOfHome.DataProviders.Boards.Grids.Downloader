namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon;
public class GetGridRequest : Interfaces.IRequestPost
{
    private const string baseUri = "https://sales.octagon.com.ua/handler/hnd_addressprogramxml";
    public string CityId { get; set; }
    public DateOnly PeriodFrom { get; set; }
    public DateOnly PeriodTo { get; set; }


    public Uri GetUri() => new Uri(baseUri);
    public HttpContent GetContent()
    {
        var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("CityId", CityId),

                new KeyValuePair<string, string>("MediaTypeId", null),
                new KeyValuePair<string, string>("PriceFrom", null),
                new KeyValuePair<string, string>("PriceTo", null),
                new KeyValuePair<string, string>("GrpFrom", null),
                new KeyValuePair<string, string>("GrpTo", null),
                new KeyValuePair<string, string>("OtsFrom", null),
                new KeyValuePair<string, string>("OtsTo", null),
                new KeyValuePair<string, string>("Faces", null),

                new KeyValuePair<string, string>("PeriodFrom", PeriodFrom.ToString("yyyy-MM-dd")),
                new KeyValuePair<string, string>("PeriodTo", PeriodTo.ToString("yyyy-MM-dd")),

                new KeyValuePair<string, string>("ShapeType", null),
                new KeyValuePair<string, string>("PolylineWKT", null),
                new KeyValuePair<string, string>("PolygonWKT", null),
                new KeyValuePair<string, string>("CircleLat", null),
                new KeyValuePair<string, string>("CircleLng", null),
                new KeyValuePair<string, string>("CircleRadius", null),

                new KeyValuePair<string, string>("Status", "a")
            });

        return content;
    }   
    
}
