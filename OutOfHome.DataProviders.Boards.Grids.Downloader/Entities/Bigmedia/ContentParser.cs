using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Bigmedia.Common;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Bigmedia;
internal class ContentParser : Interfaces.IContentParser<List<BmaBoard>>
{
    public async Task<List<BmaBoard>> ParseContent(HttpResponseMessage message)
    {
        var jsonOptions = new JsonSerializerOptions()
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };
        //var boards = await JsonSerializer.DeserializeAsync(await message.Content.ReadAsStreamAsync(), BigmediaJsonContext.Default.ListBmaBoard);
        var boards = await JsonSerializer.DeserializeAsync<BmaBoard[]>(await message.Content.ReadAsStreamAsync(), jsonOptions);
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

//[JsonSerializable(typeof(List<BmaBoard>))]
//[JsonSourceGenerationOptions(GenerationMode =JsonSourceGenerationMode.Metadata, number )]
//[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
//internal partial class BigmediaJsonContext : JsonSerializerContext
//{ 

//}
