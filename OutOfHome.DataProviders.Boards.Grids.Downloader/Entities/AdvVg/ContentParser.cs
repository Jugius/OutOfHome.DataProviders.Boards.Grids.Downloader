using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.AdvVg.Common;


namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.AdvVg;
public class ContentParser : Interfaces.IResponseConverter<ResponseContent>
{
    public async Task<ResponseContent> Convert(HttpResponseMessage message)
    {
        byte[] bytes = await message.Content.ReadAsByteArrayAsync();
        string baseUrl = GetBaseURL(message.RequestMessage.RequestUri?.ToString());

        return new ResponseContent
        {
            Source = bytes,
            Uri = baseUrl,
        };
    }
    public static string GetBaseURL(string Url)
    {
        if (string.IsNullOrEmpty(Url)) return null;

        int inx = Url.IndexOf("://") + "://".Length;
        int end = Url.IndexOf('/', inx);

        if (end != -1)
            return Url.Substring(0, end);
        else
            return null;
    }
}
