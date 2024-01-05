namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.AdvVg.Common;
public class ResponseContent
{
    public List<Board> Boards { get; set; }
    public DateTime Time { get; set; }
    public byte[] Source { get; set; }
    public string Uri { get; set; }
}
