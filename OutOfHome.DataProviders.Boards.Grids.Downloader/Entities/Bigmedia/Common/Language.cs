namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Bigmedia.Common;

public enum Language
{
    /// <summary>
    /// English
    /// </summary>
    English,

    /// <summary>
    /// Russian
    /// </summary>
    Russian,

    /// <summary>
    /// Ukrainian
    /// </summary>
    Ukrainian
}
internal static class LanguageExtention
{
    public static string ToValue(this Language language) => language switch
    {
        Language.English => "en",
        Language.Russian => "ru",
        Language.Ukrainian => "ukr"
    };
}
