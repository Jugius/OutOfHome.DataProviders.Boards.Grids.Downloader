namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;

public interface IContentResponse<TResult> : IResponse
    where TResult : new()
{
    TResult Result { get; set; }
    string RawResponseString { get; set; }
}
