
namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Bigmedia;
internal class AuthenticateResponseContentConverter : Interfaces.IResponseConverter<bool>
{
    public Task<bool> Convert(HttpResponseMessage httpResponse) => Task.FromResult(true);
}
