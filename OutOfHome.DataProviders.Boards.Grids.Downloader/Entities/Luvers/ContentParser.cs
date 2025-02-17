
namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Luvers;
internal class ContentParser : Interfaces.IResponseConverter<string[]>
{
    public async Task<string[]> Convert(HttpResponseMessage message)
    {
        var response = await message.Content.ReadAsStringAsync();
        return response.Split(Environment.NewLine);
    }
}
