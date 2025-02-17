using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp;
public class GetActualGridsRequest : IRequest
{
    private const string baseUri = "https://boards.oohelp.net/api/grids";
    public DateOnly? GridsDownoadedDate { get; set; }
    public string Key { get; set; }
    public virtual Uri GetUri()
    {
        List<string> parameters = new List<string>();
        
        if (this.GridsDownoadedDate.HasValue)
            parameters.Add($"date={GridsDownoadedDate:yyyy.MM.dd}");

        if(!string.IsNullOrEmpty(this.Key))
            parameters.Add($"key={this.Key}");

        if(parameters.Count == 0)
            return new Uri(baseUri);

        return new Uri($"{baseUri}?{string.Join('&', parameters)}");        
    }
}
