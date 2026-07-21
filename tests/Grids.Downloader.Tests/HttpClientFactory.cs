using System.Net;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Tests;

internal static class HttpClientFactory
{
    private static readonly HttpClient httpClient;
    static HttpClientFactory()
    {
        var handler = new SocketsHttpHandler
        {
            AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip,

            // Защита от проблемы с DNS: обновляем соединения каждые 2 минуты
            PooledConnectionLifetime = TimeSpan.FromMinutes(2),

            // Ограничение на количество параллельных соединений к ОДНОМУ хосту (опционально)
            MaxConnectionsPerServer = 100, 
            
            UseCookies = false
        };

        httpClient = new HttpClient(handler)
        {
            Timeout = TimeSpan.FromSeconds(30)
        };
    }
    public static HttpClient Create() => httpClient;
}
