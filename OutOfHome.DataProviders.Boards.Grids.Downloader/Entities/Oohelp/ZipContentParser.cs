using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp.Common;
using OutOfHome.DataProviders.Boards.Grids.Downloader.JsonConverters;
using System.IO.Compression;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp;
internal class ZipContentParser : Interfaces.IContentParser<ResponseContent>
{
    public async Task<ResponseContent> ParseContent(HttpResponseMessage message)
    {
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            //Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters = { new JsonDateOnlyIntConverter(), new JsonDateTimeLongConverter() }
        };
        await using GZipStream decompressionStream = new GZipStream(await message.Content.ReadAsStreamAsync(), CompressionMode.Decompress);
        return await JsonSerializer.DeserializeAsync<ResponseContent>(decompressionStream, jsonOptions);
    }
}
