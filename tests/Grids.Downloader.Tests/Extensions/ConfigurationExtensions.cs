using Microsoft.Extensions.Configuration;
using System.Net;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Tests.Extensions;

internal static class ConfigurationExtensions
{
    public static NetworkCredential GetBMACredentials(this IConfiguration configuration) =>
        new NetworkCredential
        {
            UserName = configuration["BMACredentials:UserName"]!,
            Password = configuration["BMACredentials:Password"]!
        };

    public static NetworkCredential GetOoHelpCredentials(this IConfiguration configuration) =>
        new NetworkCredential
        {
            UserName = configuration["OoHelpCredentials:UserName"]!,
            Password = configuration["OoHelpCredentials:Password"]!,
        };
    public static NetworkCredential GetOuthubCredentials(this IConfiguration configuration) =>
        new NetworkCredential
        {
            UserName = configuration["OuthubCredentials:UserName"]!,
            Password = configuration["OuthubCredentials:Password"]!
        };
    public static string GetOoHelpApiKey(this IConfiguration configuration) =>
        configuration["OoHelpApiKey"]!;

    public static string GetWorkFolder(this IConfiguration configuration) =>
        configuration["WorkFolder"]!;
    public static string GetChromiumFolder(this IConfiguration configuration) =>
        configuration["ChromiumFolder"]!;
}
