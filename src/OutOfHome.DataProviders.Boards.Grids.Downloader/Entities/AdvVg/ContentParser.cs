using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.AdvVg.Common;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.AdvVg;
internal class ContentParser : Interfaces.IResponseConverter<ResponseContent>
{
    public async Task<ResponseContent> Convert(HttpResponseMessage response)
    {
        var data = await response.Content.ReadAsByteArrayAsync();
        var fileName = GetFileName(response, response.RequestMessage.RequestUri?.ToString());
        string baseUrl = GetBaseURL(response.RequestMessage.RequestUri?.ToString());

        return new ResponseContent(fileName, data, baseUrl);
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
    private static string GetFileName(
        HttpResponseMessage response,
        string uri)
    {
        var contentDisposition =
            response.Content.Headers.ContentDisposition;

        if (!string.IsNullOrWhiteSpace(contentDisposition?.FileNameStar))
            return contentDisposition.FileNameStar;

        if (!string.IsNullOrWhiteSpace(contentDisposition?.FileName))
            return contentDisposition.FileName.Trim('"');

        return Path.GetFileName(new Uri(uri).AbsolutePath);
    }
}
