using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Bigmedia.Common;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Bigmedia.Extentions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Bigmedia;
internal class GetGridResponseContentConverter : Interfaces.IResponseConverter<List<Board>>
{
    public async Task<List<Board>> Convert(HttpResponseMessage message)
    {
        List<Board> boards = new List<Board>();

        using (var readingStream = await message.Content.ReadAsStreamAsync())
        {
            boards = await ConvertToResponse(readingStream);
        }

        if (boards.Count == 0)
            throw new Exceptions.DownloaderException(Exceptions.ErrorCode.ServerError, $"Cервер вернул 0 записей.");

        foreach (var brd in boards)
        {
            brd.PhotoUrl = NormalizeUri(brd.PhotoUrl);
            brd.SchemaUrl = NormalizeUri(brd.SchemaUrl);
        }
        return boards;
    }
    private static string NormalizeUri(string uri)
    {
        if (string.IsNullOrEmpty(uri))
            return null;

        //string newlink = uri.Replace("\"", "");

        return Uri.TryCreate(uri, UriKind.Absolute, out Uri u) ? u.ToString() : null;
    }
    public static async Task<List<Board>> ConvertToResponse(Stream stream)
    {
        var jsonOptions = new JsonSerializerOptions() { NumberHandling = JsonNumberHandling.AllowReadingFromString };

        try
        {
            return await JsonSerializer.DeserializeAsync<List<Board>>(stream, jsonOptions);
        }
        catch (JsonException)
        {
            stream.Position = 0;
            var internalBoards = await JsonSerializer.DeserializeAsync<InternalBmaBoard[]>(stream, jsonOptions);
            return internalBoards.Select(board => board.MapToBoard()).ToList();
        }
    }    
}
