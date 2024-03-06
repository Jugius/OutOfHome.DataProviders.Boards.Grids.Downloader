
namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Luvers;
internal class ContentParser : Interfaces.IContentParser<string[]>
{
    public async Task<string[]> ParseContent(HttpResponseMessage message)
    {
        var response = await message.Content.ReadAsStringAsync();
        return response.Split(Environment.NewLine);
    }
}
