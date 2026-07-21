using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.AdvVg.Common;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;
using System.Net;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.AdvVg;
internal sealed class GetGridRequest : IRequest, ICookieContainerRequest
{   
    private const string RequestArguments = @"/$/boards:partner/grid-export?ex%5Bx5%5D=0&ex%5Buseorder%5D=1&ex%5Bflds%5D%5B%5D=oid&ex%5Bflds%5D%5B%5D=cyt&ex%5Bflds%5D%5B%5D=dis&ex%5Bflds%5D%5B%5D=adr&ex%5Bflds%5D%5B%5D=zon&ex%5Bflds%5D%5B%5D=inf&ex%5Bflds%5D%5B%5D=c&ex%5Bflds%5D%5B%5D=f&ex%5Bflds%5D%5B%5D=s&ex%5Bflds%5D%5B%5D=l&ex%5Bflds%5D%5B%5D=rid_doors&ex%5Bflds%5D%5B%5D=ots&ex%5Bflds%5D%5B%5D=grp&ex%5Bflds%5D%5B%5D=prc&ex%5Bflds%5D%5B%5D=occ&ex%5Bflds%5D%5B%5D=purl&ex%5Bflds%5D%5B%5D=surl&ex%5Bflds%5D%5B%5D=mapurl";
    public string BaseUri { get; }
    public Supplier Supplier { get; }
    public List<Cookie> Cookies { get; } = [];

    public GetGridRequest(Supplier supplier)
    {
        this.Supplier = supplier;
        this.BaseUri = supplier switch
        {
            Supplier.Perekhid => @"https://base.perekhid-outdoor.com.ua",
            Supplier.SVOutdoor => @"https://i.sv-outdoor.com",
            _ => throw new NotImplementedException($"Unknown baseUri for provider: {supplier}")
        };
    }

    public Uri GetUri()
    { 
        return new Uri(BaseUri + RequestArguments);
    }
}
