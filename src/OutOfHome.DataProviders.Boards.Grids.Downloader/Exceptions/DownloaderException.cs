namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Exceptions;
public class DownloaderException : Exception
{
    public ErrorCode ErrorCode { get; }
    private DownloaderException(ErrorCode code, string message) : base(message)
    {
        this.ErrorCode = code;
    }
    private DownloaderException(ErrorCode code, string message, Exception innerException) : base(message, innerException)
    {
        this.ErrorCode = code;
    }

    public static DownloaderException ContentParsingError(string message) => new DownloaderException(ErrorCode.ContentParsingError, message);
    public static DownloaderException ContentParsingError(string message, Exception innerException) => new DownloaderException(ErrorCode.ContentParsingError, message, innerException);
    public static DownloaderException HttpError(string message) => new DownloaderException(ErrorCode.HttpError, message);
    public static DownloaderException InvalidRequest(string message) => new DownloaderException(ErrorCode.InvalidRequest, message);
    public static DownloaderException ServerError(string message) => new DownloaderException(ErrorCode.ServerError, message);
}