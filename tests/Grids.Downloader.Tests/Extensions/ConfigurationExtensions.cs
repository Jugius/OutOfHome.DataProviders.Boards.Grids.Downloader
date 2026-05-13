using OutOfHome.DataProviders.Boards.Grids.Downloader.Tests.Configurations;
using Microsoft.Extensions.Configuration;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Tests.Extensions;

internal static class ConfigurationExtensions
{
    public static Credentials GetBMACredentials(this IConfiguration configuration) =>
        new Credentials
        {
            UserName = configuration["BMACredentials:UserName"]!,
            Password = configuration["BMACredentials:Password"]!
        };

    public static Credentials GetOoHelpCredentials(this IConfiguration configuration) =>
        new Credentials
        {
            UserName = configuration["OoHelpCredentials:UserName"]!,
            Password = configuration["OoHelpCredentials:Password"]!,
        };
    public static Credentials GetOuthubCredentials(this IConfiguration configuration) =>
        new Credentials
        {
            UserName = configuration["OuthubCredentials:UserName"]!,
            Password = configuration["OuthubCredentials:Password"]!
        };
    public static string GetOoHelpApiKey(this IConfiguration configuration) =>
        configuration["OoHelpApiKey"]!;

    public static string GetWorkFolder(this IConfiguration configuration) =>
        configuration["WorkFolder"]!;
}
