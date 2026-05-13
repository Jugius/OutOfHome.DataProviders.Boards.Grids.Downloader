using System.Net.Http.Headers;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Common;

internal sealed class FileContentParser : Interfaces.IResponseConverter<FileResponse>
{
    public async Task<FileResponse> Convert(HttpResponseMessage response)
    {
        var data = await response.Content.ReadAsByteArrayAsync();
        var fileName = GetFileName(response, response.RequestMessage.RequestUri?.ToString());

        var fileType = DetectFileType(fileName, response.Content.Headers.ContentType);

        return new FileResponse(fileName, fileType, data);
    }
    private static string GetFileName(
        HttpResponseMessage response,
        string uri)
    {
        var contentDisposition =
            response.Content.Headers.ContentDisposition;

        if (!string.IsNullOrWhiteSpace(contentDisposition?.FileNameStar))
            return contentDisposition.FileNameStar;

        if (!string.IsNullOrWhiteSpace(contentDisposition?.FileName))
            return contentDisposition.FileName.Trim('"');

        return Path.GetFileName(new Uri(uri).AbsolutePath);
    }
    private static FileType DetectFileType(
        string fileName,
        MediaTypeHeaderValue contentType)
    {
        var extension = Path.GetExtension(fileName)
            .ToLowerInvariant();

        return extension switch
        {
            ".xlsx" => FileType.Xlsx,
            ".xls" => FileType.Xls,
            ".csv" => FileType.Csv,            
            _ => DetectByContentType(contentType)
        };
    }
    private static FileType DetectByContentType(
        MediaTypeHeaderValue contentType)
    {
        var mediaType = contentType?.MediaType?
            .ToLowerInvariant();

        return mediaType switch
        {
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                => FileType.Xlsx,

            "text/csv"
                => FileType.Csv,            

            _ => FileType.Unknown
        };
    }
}
