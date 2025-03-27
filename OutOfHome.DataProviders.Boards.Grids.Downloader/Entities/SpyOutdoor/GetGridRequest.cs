using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.SpyOutdoor;
public class GetGridRequest : IRequest
{
    private const string uriBase_IGROK = "https://igrok.spyoutdoor.com";
    
    public Supplier Supplier { get; set; }
    public Uri GetUri() => this.Supplier switch
    {
        Supplier.Igrok => new Uri(uriBase_IGROK),
        _ => throw new NotImplementedException($"Base Uri not registered for supplier: {this.Supplier}"),
    };
}
