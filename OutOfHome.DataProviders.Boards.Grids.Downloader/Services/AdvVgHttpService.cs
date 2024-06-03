using PuppeteerSharp;
using System.Net;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Services;
public abstract class AdvVgHttpService<TRequest> : IAdvVgHttpEngine where TRequest : Entities.AdvVg.GetGridRequest, new()
{
    private const string SessionIdCookieName = "PHPSESSID";

    private HttpEngine<TRequest, Entities.AdvVg.Common.ResponseContent, Entities.AdvVg.ContentParser> _getAdvVgGridEngine;
    
    public static async Task<Cookie> GetInitializedSessionCookie(string uri)
    {
        var browserFetcher = new BrowserFetcher();
        await browserFetcher.DownloadAsync();
        await using var browser = await Puppeteer.LaunchAsync(
            new LaunchOptions { Headless = true });
        await using var page = await browser.NewPageAsync();
        await page.GoToAsync(uri);
        
        var cookies = await page.GetCookiesAsync();
        var sesIdCookie = cookies.FirstOrDefault(a => a.Name.Equals(SessionIdCookieName, StringComparison.OrdinalIgnoreCase));

        return new Cookie
        {
            Name = sesIdCookie.Name,
            Value = sesIdCookie.Value,
            Domain = sesIdCookie.Domain,
            Path = sesIdCookie.Path,
            HttpOnly = false,
            Secure = true
        };
    }
    internal static async Task<HttpEngine<TRequest, Entities.AdvVg.Common.ResponseContent, Entities.AdvVg.ContentParser>> CreateHttpEngine(string uri)
    {
        var cookie = await GetInitializedSessionCookie(uri);
        var handler = HttpClientFactory.GetDefaultHttpClientHandler();
        handler.CookieContainer.Add(cookie);
        var client = HttpClientFactory.CreateDefaultHttpClient(handler);
        return new HttpEngine<TRequest, Entities.AdvVg.Common.ResponseContent, Entities.AdvVg.ContentParser>(client);
    }
    public async Task<Entities.AdvVg.Common.ResponseContent> GetAdvVgGrid()
    {
        TRequest request = new TRequest();
        _getAdvVgGridEngine ??= await CreateHttpEngine(request.BaseUri);
        return await this._getAdvVgGridEngine.QueryAsync(request);
    }
}
