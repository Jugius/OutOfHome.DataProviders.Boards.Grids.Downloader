using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.SpyOutdoor;
public class GetBoardsRequest : IRequest
{
    private const int MaxCount = 400;

    private const string uriBase_IGROK = "https://igrok.spyoutdoor.com";
    private const string uriBase_BROK = "https://brok.spyoutdoor.com";

    private readonly Supplier supplier;
    private readonly int townId;
    private readonly int start;
    private readonly int count;

    public GetBoardsRequest(Supplier supplier, int townId, int start, int count)
    {
        this.supplier = supplier;
        this.townId = townId;
        this.start = start;
        this.count = count;
    }

    public Uri GetUri()
    { 
        string baseUri = this.supplier switch
        {
            Supplier.Igrok => uriBase_IGROK,
            Supplier.Brok => uriBase_BROK,
            _ => throw new NotImplementedException($"Base Uri not registered for supplier: {this.supplier}"),
        };

        baseUri += $"/api/query?restUrl=board&method=GET&params%5BtownId%5D={this.townId}&params%5Bstart%5D={this.start}&params%5Bcount%5D={this.count}";
        return new Uri(baseUri);
    }
}


