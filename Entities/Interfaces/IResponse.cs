using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Common.Enums;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Interfaces;

public interface IResponse
{
    /// <summary>
    /// Raw querystring of the request.
    /// </summary>
    string RawQueryString { get; set; }

    /// <summary>
    /// The status returned with the response.
    /// <see cref="Status.Ok"/> indicates success.
    /// </summary>
    Status? Status { get; set; }

    /// <summary>
    /// When the status code is other than 'Ok', there may be an additional error_message field within the response object. 
    /// This field contains more detailed information about the reasons behind the given status code.
    /// Note: This field is not guaranteed to be always present, and its content is subject to change.
    /// </summary>
    string ErrorMessage { get; set; }
}
