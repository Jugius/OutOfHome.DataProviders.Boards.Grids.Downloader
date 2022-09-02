namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Exceptions;

public enum ErrorCode
{
    Undefined = 0,
    Ok,
    RequestError,
    HttpError,
    ContentParsingError,
    ServerError,


    //RequestDenied,        
    //InvalidRequest,       
    //ZeroResults,
    //UnknownServerError,   

}

