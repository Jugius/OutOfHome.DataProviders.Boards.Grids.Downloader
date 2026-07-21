using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.AdvVg;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.AdvVg.Common;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Exceptions;
using PuppeteerSharp;
using System.Net;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Services;
public sealed class AdvVgHttpService(HttpClient httpClient, IChromiumInitializer chromiumInitializer)
{
    private const string SessionIdCookieName = "PHPSESSID";

    private readonly HttpEngine<GetGridRequest, ResponseContent, ContentParser> _getAdvVgGridEngine =
        new HttpEngine<GetGridRequest, ResponseContent, ContentParser>(httpClient);
    
    public async Task<Cookie> GetInitializedSessionCookie(string uri)
    {
        await using var pageContext = await chromiumInitializer.CreatePageAsync();
        var page = pageContext.Page;
        
        // Отключаем загрузку тяжелых ресурсов (картинки, шрифты, стили)
        await page.SetRequestInterceptionAsync(true);
        page.Request += (sender, e) =>
        {
            var type = e.Request.ResourceType;
            if (type is ResourceType.Image or ResourceType.Font or ResourceType.StyleSheet or ResourceType.Media)
            {
                _ = e.Request.AbortAsync();
            }
            else
            {
                _ = e.Request.ContinueAsync();
            }
        };

        await page.GoToAsync(uri);

        var cookies = await page.GetCookiesAsync();
        var sesIdCookie = cookies.FirstOrDefault(a => a.Name.Equals(SessionIdCookieName, StringComparison.OrdinalIgnoreCase));

        return sesIdCookie is null
            ? throw DownloaderException.ServerError($"Not found session cookie on page {uri}")
            : new Cookie
        {
            Name = sesIdCookie.Name,
            Value = sesIdCookie.Value,
        };
    }
    public async Task<ResponseContent> GetGrid(Supplier supplier)
    {
        var request = new GetGridRequest(supplier);
        var cookie = await GetInitializedSessionCookie(request.BaseUri);
        request.Cookies.Add(cookie);
        return await this._getAdvVgGridEngine.QueryAsync(request);
    }
}
