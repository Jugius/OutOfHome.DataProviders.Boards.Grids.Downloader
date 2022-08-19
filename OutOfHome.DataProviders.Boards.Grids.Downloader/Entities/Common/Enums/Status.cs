
namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Common.Enums;

public enum Status
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
