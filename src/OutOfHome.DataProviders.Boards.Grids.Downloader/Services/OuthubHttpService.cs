using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Outhub;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Outhub.Common;
using PuppeteerSharp;
using System.Net;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Services;

public class OuthubHttpService(HttpClient httpClient, IChromiumInitializer chromiumInitializer)
{
    private readonly HttpEngine<GetGridRequest, ResponseContent, GetGridResponseContentConverter> _getGridEngine =
        new HttpEngine<GetGridRequest, ResponseContent, GetGridResponseContentConverter>(httpClient);

    public async Task<ResponseContent> GetGrid(NetworkCredential? credentials = null)
    {
        var getGrid = new GetGridRequest();
        if (credentials != null)
        {
            var cookies = await GetLoginCookies(credentials).ConfigureAwait(false);
            getGrid.Cookies.AddRange(cookies);
        }
        return await _getGridEngine.QueryAsync(getGrid);

    }
    public async Task<List<Cookie>> GetLoginCookies(NetworkCredential? credentials)
    {
        const string uri = "https://booking.outhub.online/Account/LogOn";
        if (credentials is null) return [];

        await using var pageContext = await chromiumInitializer.CreatePageAsync();
        var page = pageContext.Page;

        await page.GoToAsync(uri, new NavigationOptions
        {
            WaitUntil = new[] { WaitUntilNavigation.DOMContentLoaded }
        });

        await page.TypeAsync("#UserName", credentials.UserName);
        await page.TypeAsync("#Password", credentials.Password);

        await Task.WhenAll(
            page.WaitForNavigationAsync(new NavigationOptions { Timeout = 15000 }),
            page.ClickAsync("input[type=submit]"));


        var cookies = await page.GetCookiesAsync();

        if (cookies == null || cookies.Length == 0 || !cookies.Any(a => a.Name == ".mainauth"))
        {
            return [];
        }

        return cookies.Select(c => new Cookie
        {
            Name = c.Name,
            Value = c.Value
        }).ToList();
    }
    public async Task<string> GetResponseStringAsync(NetworkCredential? credentials = null)
    {
        var getGrid = new GetGridRequest();
        if (credentials != null)
        {
            var cookies = await GetLoginCookies(credentials).ConfigureAwait(false);
            getGrid.Cookies.AddRange(cookies);
        }
        return await _getGridEngine.GetResponseStringAsync(getGrid);
    }    
}
