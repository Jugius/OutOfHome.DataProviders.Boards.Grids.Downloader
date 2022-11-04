using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Bigmedia.Common;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Bigmedia;
internal class ContentParser : Interfaces.IContentParser<List<Board>>
{
    public async Task<List<Board>> ParseContent(HttpResponseMessage message)
    {
        var jsonOptions = new JsonSerializerOptions()
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };
        //var boards = await JsonSerializer.DeserializeAsync(await message.Content.ReadAsStreamAsync(), BigmediaJsonContext.Default.ListBmaBoard);
        var boards = await JsonSerializer.DeserializeAsync<Board[]>(await message.Content.ReadAsStreamAsync(), jsonOptions);

        if (boards == null || boards.Length == 0)
            throw new Exceptions.DownloaderException(Exceptions.ErrorCode.ServerError, $"Cервер вернул 0 записей.");

        foreach (var brd in boards)
        {
            brd.PhotoUrl = NormalizeUri(brd.PhotoUrl);
            brd.SchemaUrl = NormalizeUri(brd.SchemaUrl);
        }
        return boards.ToList();
    }
    private static string NormalizeUri(string uri)
    {
        if (string.IsNullOrEmpty(uri))
            return null;

        //string newlink = uri.Replace("\"", "");

        return Uri.TryCreate(uri, UriKind.Absolute, out Uri u) ? u.ToString() : null;
    }
}
