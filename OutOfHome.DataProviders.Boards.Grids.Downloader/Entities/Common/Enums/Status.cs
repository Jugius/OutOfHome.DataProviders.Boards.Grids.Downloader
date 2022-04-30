
namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Common.Enums;

public enum Status
{
    /// <summary>
    /// Set when deserialization fails (default).
    /// </summary>
    Undefined = 0,

    /// <summary>
    /// Indicates that no errors occurred; the place was successfully detected and at least one result was returned.
    /// </summary>
    Ok,

    /// <summary>
    /// Indicates that your request was denied.
    /// </summary>
    RequestDenied,

    /// <summary>
    /// Indicates that the query parameter is missing.
    /// </summary>        
    InvalidRequest,

    /// <summary>
    /// Indicates that the request was successful but returned no results.
    /// </summary>        
    ZeroResults,

    /// <summary>
    /// Indicates the request could not be processed due to a server error. The request may succeed if you try again.
    /// </summary>
    UnknownError,

    /// <summary>
    /// Indicates the request resulted in a Http error code.
    /// </summary>
    HttpError,

}
