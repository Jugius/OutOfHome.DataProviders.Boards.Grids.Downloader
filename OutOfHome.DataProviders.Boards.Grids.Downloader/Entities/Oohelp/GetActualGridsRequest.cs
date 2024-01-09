using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp;
internal class GetActualGridsRequest : IRequest
{
    public DateOnly? GridsDownoadedDate { get; set; }
    public Uri GetUri()
    {
        if (this.GridsDownoadedDate.HasValue)
        {
            return new Uri($"{OohelpRequestBase.URL}?date={GridsDownoadedDate:yyyy.MM.dd}");
        }
        return new Uri(OohelpRequestBase.URL);
    }
}
