using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.SpyOutdoor.Common;
using System.Text.RegularExpressions;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.SpyOutdoor;
public class ContentParser : Interfaces.IResponseConverter<ResponseContent>
{
    private const string pattern = @"<script\s+type=""application/json""\s+data-js-react-on-rails-store=""appStore"">(.*?)</script>";
    public async Task<ResponseContent> Convert(HttpResponseMessage message)
    {
        var stringContent = await message.Content.ReadAsStringAsync();
        return ConvertToResponse(stringContent);
    }
    public static string GetJsonContent(string content)
    {
        Match match = Regex.Match(content, pattern, RegexOptions.Singleline);

        if (match.Success)
        {
            string jsonContent = match.Groups[1].Value;
            return jsonContent;
        }
        else
        {
            throw new Exception("JSON не найден.");            
        }
    }
    public static ResponseContent ConvertToResponse(string stringContent)
    {
        var jsonContent = GetJsonContent(stringContent);

        return System.Text.Json.JsonSerializer.Deserialize<ResponseContent>(jsonContent);
    }
}
