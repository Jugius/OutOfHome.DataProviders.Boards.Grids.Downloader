
namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp;
internal class AuthenticateResponseContentConverter : Interfaces.IResponseConverter<string>
{
    public async Task<string> Convert(HttpResponseMessage message)
    {
        return await message.Content.ReadAsStringAsync();
    }
}
