namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon.GetGrid;
public class GetGridRequest : BaseRequest, Interfaces.IRequestPost
{
    public string CityId { get; set; }
    public DateOnly PeriodFrom { get; set; }
    public DateOnly PeriodTo { get; set; }
    protected internal override string BaseUrl => "sales.octagon.com.ua/handler/hnd_addressprogramxml";

    public HttpContent GetContent()
    {
        var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("CityId", this.CityId),

                new KeyValuePair<string, string>("MediaTypeId", null),
                new KeyValuePair<string, string>("PriceFrom", null),
                new KeyValuePair<string, string>("PriceTo", null),
                new KeyValuePair<string, string>("GrpFrom", null),
                new KeyValuePair<string, string>("GrpTo", null),
                new KeyValuePair<string, string>("OtsFrom", null),
                new KeyValuePair<string, string>("OtsTo", null),
                new KeyValuePair<string, string>("Faces", null),

                new KeyValuePair<string, string>("PeriodFrom", this.PeriodFrom.ToString("yyyy-MM-dd")),
                new KeyValuePair<string, string>("PeriodTo", this.PeriodTo.ToString("yyyy-MM-dd")),

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
