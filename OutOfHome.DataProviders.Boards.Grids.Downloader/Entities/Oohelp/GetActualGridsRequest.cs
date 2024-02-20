using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp;
public class GetActualGridsRequest : IRequest
{
    public DateOnly? GridsDownoadedDate { get; set; }
    public string Key { get; set; }
    public Uri GetUri()
    {
        List<string> parameters = new List<string>();
        
        if (this.GridsDownoadedDate.HasValue)
            parameters.Add($"date={GridsDownoadedDate:yyyy.MM.dd}");

        if(!string.IsNullOrEmpty(this.Key))
            parameters.Add($"key={this.Key}");

        if(parameters.Count == 0)
            return new Uri(OohelpRequestBase.URL);

        return new Uri($"{OohelpRequestBase.URL}?{string.Join('&', parameters)}");        
    }
}
