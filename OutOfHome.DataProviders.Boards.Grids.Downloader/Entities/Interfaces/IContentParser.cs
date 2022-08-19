namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;

public interface IContentParser<TResult>
    where TResult : new()
{
    Task<TResult> ParseContent(HttpResponseMessage message);
}   
