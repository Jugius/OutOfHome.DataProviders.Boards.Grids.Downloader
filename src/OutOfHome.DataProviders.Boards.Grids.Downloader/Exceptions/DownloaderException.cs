namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Exceptions;
public class DownloaderException : Exception
{
    public ErrorCode ErrorCode { get; }
    public DownloaderException(ErrorCode code, string message) : base(message)
    {
        this.ErrorCode = code;
    }
}