using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp;
public class GetActualGridsRequest : IRequest
{
    private const string baseUri = "https://boards.oohelp.net/api/grids";
    public DateOnly? GridsDownoadedDate { get; set; }
    public virtual Uri GetUri()
    {
        if (this.GridsDownoadedDate.HasValue)
        { 
            return new Uri($"{baseUri}?date={GridsDownoadedDate:yyyy.MM.dd}");
        }

        return new Uri(baseUri);
    }
}