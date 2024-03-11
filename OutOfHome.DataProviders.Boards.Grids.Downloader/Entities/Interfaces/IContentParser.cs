namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;

internal interface IContentParser<TResult>
    where TResult : notnull
{
    Task<TResult> ParseContent(HttpResponseMessage message);
}   
