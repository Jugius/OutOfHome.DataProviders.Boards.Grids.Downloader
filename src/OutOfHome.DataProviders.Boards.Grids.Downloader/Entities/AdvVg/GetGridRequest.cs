using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.AdvVg;
public abstract class GetGridRequest : IRequest
{
    //private const string encodedArguments = @"/$/boards:partner/grid-export?ex[x5]=0&ex[useorder]=1&ex[flds][]=oid&ex[flds][]=cyt&ex[flds][]=dis&ex[flds][]=adr&ex[flds][]=zon&ex[flds][]=inf&ex[flds][]=c&ex[flds][]=f&ex[flds][]=s&ex[flds][]=l&ex[flds][]=rid_doors&ex[flds][]=ots&ex[flds][]=grp&ex[flds][]=prc&ex[flds][]=occ&ex[flds][]=purl&ex[flds][]=surl&ex[flds][]=mapurl";

    protected const string RequestArguments = @"/$/boards:partner/grid-export?ex%5Bx5%5D=0&ex%5Buseorder%5D=1&ex%5Bflds%5D%5B%5D=oid&ex%5Bflds%5D%5B%5D=cyt&ex%5Bflds%5D%5B%5D=dis&ex%5Bflds%5D%5B%5D=adr&ex%5Bflds%5D%5B%5D=zon&ex%5Bflds%5D%5B%5D=inf&ex%5Bflds%5D%5B%5D=c&ex%5Bflds%5D%5B%5D=f&ex%5Bflds%5D%5B%5D=s&ex%5Bflds%5D%5B%5D=l&ex%5Bflds%5D%5B%5D=rid_doors&ex%5Bflds%5D%5B%5D=ots&ex%5Bflds%5D%5B%5D=grp&ex%5Bflds%5D%5B%5D=prc&ex%5Bflds%5D%5B%5D=occ&ex%5Bflds%5D%5B%5D=purl&ex%5Bflds%5D%5B%5D=surl&ex%5Bflds%5D%5B%5D=mapurl";

    public abstract string BaseUri { get; }
    public virtual Uri GetUri()
    { 
        return new Uri(BaseUri + RequestArguments);
    }
}
