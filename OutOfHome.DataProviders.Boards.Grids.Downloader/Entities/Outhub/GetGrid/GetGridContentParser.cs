using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Common.Enums;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Outhub.GetGrid;

public class GetGridContentParser : Interfaces.IContentDeserializer<GetGridResponse>
{
    public async Task<GetGridResponse> DeserializeAsync(HttpResponseMessage message)
    {
        var schema = await System.Text.Json.JsonSerializer.DeserializeAsync<GetGridResponse>(await message.Content.ReadAsStreamAsync());

        if (schema.ServerStatus < 0)
            throw new Exception($"Cервер вернул статус: {schema.ServerStatus}");

        schema.Downloaded = System.DateTime.Now;
        schema.Language = Language.Ukrainian;
        return schema;
    }
}
