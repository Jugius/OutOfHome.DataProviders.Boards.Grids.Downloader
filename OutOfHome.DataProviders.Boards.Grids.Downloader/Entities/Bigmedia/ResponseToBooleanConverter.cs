
namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Bigmedia;
internal class ResponseToBooleanConverter : Interfaces.IContentParser<bool>
{
    public Task<bool> ParseContent(HttpResponseMessage httpResponse) => Task.FromResult(true);
}
