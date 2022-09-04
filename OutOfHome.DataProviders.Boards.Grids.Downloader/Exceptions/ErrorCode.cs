namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Exceptions;

public enum ErrorCode
{
    Undefined = 0,

    InvalidRequest,    
    HttpError,
    ContentParsingError,
    ServerError,


    //RequestDenied,        
    //InvalidRequest,       
    //ZeroResults,
    //UnknownServerError, 
}

