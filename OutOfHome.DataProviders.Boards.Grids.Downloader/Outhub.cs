using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Outhub.GetGrid;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader
{
    public class Outhub
    {
        public static HttpEngine<GetGridRequest, GetGridResponse, GetGrigContentParser> GetGrid => HttpEngine<GetGridRequest, GetGridResponse, GetGrigContentParser>.instance;
    }
}
