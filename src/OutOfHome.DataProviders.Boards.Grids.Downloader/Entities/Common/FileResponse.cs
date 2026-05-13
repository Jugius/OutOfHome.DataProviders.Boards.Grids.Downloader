namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Common;

public sealed record FileResponse(string FileName, FileType FileType, byte[] Data);
