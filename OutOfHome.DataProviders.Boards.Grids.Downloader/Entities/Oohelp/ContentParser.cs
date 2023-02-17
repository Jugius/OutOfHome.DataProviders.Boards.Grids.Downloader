using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp.Common;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp;
internal class ContentParser : Interfaces.IContentParser<ResponseContent>
{
    public async Task<ResponseContent> ParseContent(HttpResponseMessage message)
    {
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            //Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters = { new JsonDateOnlyIntConverter(), new JsonDateTimeLongConverter() }
        };

        return await JsonSerializer.DeserializeAsync<ResponseContent>(await message.Content.ReadAsStreamAsync(), jsonOptions);
    }    
}
