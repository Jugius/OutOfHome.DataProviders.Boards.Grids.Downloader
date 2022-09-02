namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;

internal interface IContentParser<TResult>
    where TResult : new()
{
    Task<TResult> ParseContent(HttpResponseMessage message);
}   
