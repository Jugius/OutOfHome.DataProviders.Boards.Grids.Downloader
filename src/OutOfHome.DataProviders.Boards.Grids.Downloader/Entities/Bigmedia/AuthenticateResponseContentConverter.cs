
using OutOfHome.DataProviders.Boards.Grids.Downloader.Exceptions;
using System.Net;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Bigmedia;
internal class AuthenticateResponseContentConverter : Interfaces.IResponseConverter<Cookie>
{
    private const string SessionIdCookieName = "connect.sid";
    public async Task<Cookie> Convert(HttpResponseMessage httpResponse)
    {
        if (httpResponse.Headers.TryGetValues("Set-Cookie", out var cookieHeaders))
        {
            foreach (var header in cookieHeaders)
            {
                var cookie = ParseSingleSetCookieHeader(header);
                if (cookie != null && cookie.Name.Equals(SessionIdCookieName, StringComparison.OrdinalIgnoreCase))
                {
                    return cookie;
                }
            }
            throw DownloaderException.ServerError($"Not found session cookie");
        }
        throw DownloaderException.ServerError($"Not found Set-Cookie section in BMA response");
    }
    private static Cookie? ParseSingleSetCookieHeader(string headerValue)
    {
        if (string.IsNullOrWhiteSpace(headerValue))
            return null;

        // Берем только первый сегмент до точки с запятой (имя=значение)
        var cookieNameValuePair = headerValue.Split(';')[0].Trim();

        // Делим STRICTLY по ПЕРВОМУ знаку '=', так как в самом значении тоже могут быть '='
        var equalsIndex = cookieNameValuePair.IndexOf('=');
        if (equalsIndex <= 0)
            return null; // Нет имени или знака равенства

        var name = cookieNameValuePair.Substring(0, equalsIndex).Trim();
        var value = cookieNameValuePair.Substring(equalsIndex + 1).Trim();

        return new Cookie(name, value);
    }
}
