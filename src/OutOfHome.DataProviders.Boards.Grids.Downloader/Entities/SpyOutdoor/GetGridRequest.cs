using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.SpyOutdoor;
public class GetGridRequest : IRequest
{
    private const string uriBase_IGROK = "https://igrok.spyoutdoor.com";
    private const string uriBase_BROK = "https://brok.spyoutdoor.com";
    private const string uriBase_UNICOM = "https://unicom.spyoutdoor.com";


    public Supplier Supplier { get; set; }
    public Uri GetUri() => this.Supplier switch
    {
        Supplier.Igrok => new Uri(uriBase_IGROK),
        Supplier.Brok => new Uri(uriBase_BROK),
        Supplier.Unicom => new Uri(uriBase_UNICOM),
        _ => throw new NotImplementedException($"Base Uri not registered for supplier: {this.Supplier}"),
    };
}
