using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon.Common;
using System.Xml.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Octagon;

public class ContentParser : Interfaces.IContentParser<ResponseContent>
{
    public async Task<ResponseContent> ParseContent(HttpResponseMessage message)
    {
        var rawXml = await message.Content.ReadAsStringAsync().ConfigureAwait(false);

        if (string.IsNullOrEmpty(rawXml))
            throw new Exception("Сервер вернул нулевой результат.}");

        XmlSerializer formatter = new XmlSerializer(typeof(ResponseContent));

        ResponseContent response = (ResponseContent)formatter.Deserialize(new StringReader(rawXml));
        return response;
    }        
}