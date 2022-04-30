namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces
{
    public interface IContentDeserializer<TResponse>
        where TResponse : IResponse, new()
    {
        Task<TResponse> DeserializeAsync(HttpResponseMessage message);
    }   
}
