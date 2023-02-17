namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Common;

public enum Language
{
    /// <summary>
    /// English
    /// </summary>
    English = 3,

    /// <summary>
    /// Russian
    /// </summary>
    Russian = 1,

    /// <summary>
    /// Ukrainian
    /// </summary>
    Ukrainian = 2
}
internal static class LanguageExtention
{
    public static string ToValue(this Language language) => language switch
    {
        Language.English => "en",
        Language.Russian => "ru",
        Language.Ukrainian => "ukr",
        _ => throw new InvalidOperationException("Unsupported request laguage: " + language)
    };
}
