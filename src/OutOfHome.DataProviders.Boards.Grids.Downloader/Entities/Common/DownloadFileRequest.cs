using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Common;

public sealed class DownloadFileRequest : IRequest
{
    public const string KentUri = @"https://docs.google.com/spreadsheets/d/1iNU3TjYw8YFSP9D9fNfy8jtoCOusAdBYc9cIL6A8nkY/export?format=xlsx";
    public const string MegaboardUri = @"http://ftp.megaboard.ua/downloads/SETKA_MEGABOARD.xlsx";
    public const string OllanUri = @"https://docs.google.com/spreadsheets/d/1eLJy9XGLtjYX7ipBVQHLjcvY2ymU64zVI38Z8bLeTHI/export?format=xlsx";
    public const string PerekhidUri = @"https://base.perekhid-outdoor.com.ua/upload/partners/24/export/perekhid-outdoor.xlsx";
    public const string PozitivBCUri = @"https://docs.google.com/spreadsheets/d/1oGHFxvdCzxq98eIh6uyLrHqJd0igpP5T/export?format=xlsx";
    public const string RoyalTrendUri = @"https://docs.google.com/spreadsheets/d/1OTjr1HNcBNsc3RXCzzWksiTgETLo9tHm7AUW3T33Z8s/export?format=xlsx";
    public const string SVOutdoorUri = @"https://i.sv-outdoor.com/upload/partners/86/export/sv-outdoor.xlsx";


    private readonly string _uri;
    private DownloadFileRequest(string uri) => _uri = uri;    
    public Uri GetUri() => new Uri(_uri);
    public static DownloadFileRequest Kent => new DownloadFileRequest(KentUri);
    public static DownloadFileRequest Megaboard => new DownloadFileRequest(MegaboardUri);
    public static DownloadFileRequest Ollan => new DownloadFileRequest(OllanUri);
    public static DownloadFileRequest Perekhid => new DownloadFileRequest(PerekhidUri);
    public static DownloadFileRequest PozitivBC => new DownloadFileRequest(PozitivBCUri);
    public static DownloadFileRequest RoyalTrend => new DownloadFileRequest(RoyalTrendUri);
    public static DownloadFileRequest SVOutdoor => new DownloadFileRequest(SVOutdoorUri);
    
}
