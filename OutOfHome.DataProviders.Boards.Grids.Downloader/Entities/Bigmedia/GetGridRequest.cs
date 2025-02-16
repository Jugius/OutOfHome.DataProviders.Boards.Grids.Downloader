using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Common;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Bigmedia;

public class GetGridRequest : IRequest
{
    private const string baseUri = "https://www.bma.ua/api/v1/data?faces=1";
    public Language Language { get; set; } = Language.Ukrainian;
    public Uri GetUri() => new Uri($"{baseUri}&locale={this.Language.ToValue()}");
}
