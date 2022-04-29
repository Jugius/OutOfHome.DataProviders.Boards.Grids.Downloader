using System;
using System.Text.Json.Serialization;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities;

public abstract class BaseRequest : Interfaces.IRequest
{
    [JsonIgnore]
    protected internal abstract string BaseUrl { get; }

    [JsonIgnore]
    public bool UseSSL { get; set; } = true;

    public virtual Uri GetUri()
    {
        string SCHEME = UseSSL ? "https://" : "http://";
        var uri = new Uri($"{SCHEME}{this.BaseUrl}");
        return uri;
    }

}
