namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;

public interface IResponseContent : IResponse
{
    /// <summary>
    /// Raw string of the response.
    /// </summary>
    string RawResponseString { get; set; }
}
