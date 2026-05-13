using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Outhub;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Outhub.Common;
using PuppeteerSharp;
using System.Net;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Services;

public class OuthubHttpService
{
    private readonly HttpClientHandler _httpClientHandler;
    private readonly HttpClient _httpClient;
    private readonly HttpEngine<GetGridRequest, ResponseContent, GetGridResponseContentConverter> _getGridEngine;

    public OuthubHttpService(HttpClient httpClient)
    {        
        _httpClient = httpClient;
        _getGridEngine = new HttpEngine<GetGridRequest, ResponseContent, GetGridResponseContentConverter>(_httpClient);
    }
    public OuthubHttpService()
    {
        _httpClientHandler = HttpClientFactory.GetDefaultHttpClientHandler();
        _httpClient = HttpClientFactory.CreateDefaultHttpClient(_httpClientHandler);
        _getGridEngine = new HttpEngine<GetGridRequest, ResponseContent, GetGridResponseContentConverter>(_httpClient);
    }
    public async Task<ResponseContent> GetGrid(GetGridRequest request) => await _getGridEngine.QueryAsync(request);
    public async Task<bool> Authenticate(AuthenticateRequest request) 
    {
        if (_httpClientHandler == null) return false;

        var cookies = await GetLoginCookies(request).ConfigureAwait(false);        

        if (cookies.Count == 0 || !cookies.Any(a => a.Name == ".mainauth"))
        {
            return false;
        }

        foreach (var cookie in cookies)
        {
            _httpClientHandler.CookieContainer.Add(cookie);
        }

        return true;
    }

    private static async Task<List<Cookie>> GetLoginCookies(AuthenticateRequest request)
    {
        await new BrowserFetcher().DownloadAsync();        
        using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = true 
        });
        await using var page = await browser.NewPageAsync();
        await page.GoToAsync(request.Uri);

        await page.TypeAsync("#UserName", request.UserName);
        await page.TypeAsync("#Password", request.Password);
        await page.ClickAsync("input[type=submit]");
        await page.WaitForNavigationAsync();

        var cookies = await page.GetCookiesAsync();

        return cookies.Select(c => new Cookie
        {
            Name = c.Name,
            Value = c.Value,
            Domain = c.Domain,
            HttpOnly = false,
            Secure = true
        }).ToList();
    }
    public async Task<string> GetResponseStringAsync(GetGridRequest request) => await _getGridEngine.GetResponseStringAsync(request);
}
