using System;
using System.Collections.Generic;
using System.Net.Http;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Outhub.Authorization;

public class AuthorizationRequest : BaseOuthubRequest, Interfaces.IRequestPost
{
    protected internal override string BaseUrl => base.BaseUrl + "Account/LogOn";
    public string UserName { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }

    public HttpContent GetContent()
    {
        if (string.IsNullOrEmpty(this.UserName))
            throw new InvalidOperationException("Empty UserName Field");

        if (string.IsNullOrEmpty(this.Password))
            throw new InvalidOperationException("Empty Password Field");

        return new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("UserName", UserName),
                new KeyValuePair<string, string>("Password", Password),
                new KeyValuePair<string, string>("RememberMe", RememberMe.ToString())
            });
    }
}
