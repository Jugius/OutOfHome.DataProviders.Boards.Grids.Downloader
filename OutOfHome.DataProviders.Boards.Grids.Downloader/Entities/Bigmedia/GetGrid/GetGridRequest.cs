using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Bigmedia.Common;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Bigmedia.GetGrid;

public class GetGridRequest : BaseBigmediaRequest
{
    public Language Language { get; set; } = Language.Ukrainian;   
    public override Uri GetUri()
    {
        string uri = base.GetUri().ToString();
        uri += "?faces=1";
        uri += "&locale=" + this.Language.ToValue();
        return new Uri(uri);
    }
    protected internal override string BaseUrl => base.BaseUrl + "data";
}
