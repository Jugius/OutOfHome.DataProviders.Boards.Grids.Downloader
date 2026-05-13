namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;

internal interface IResponseConverter<TResult>
    where TResult : notnull
{
    Task<TResult> Convert(HttpResponseMessage message);
}   
