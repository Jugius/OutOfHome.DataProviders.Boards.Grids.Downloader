using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp.Common;
using System.Net;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Services;
public class OohelpHttpService(HttpClient httpClient)
{
    private readonly HttpEngine<GetGridRequest, ResponseContent, GetGridResponseContentConverter> _getGridEngine = new(httpClient);
    private readonly HttpEngine<GetActualGridsRequest, List<GridInfo>, GetActualGridsResponseContentConverter> _getActualGridsEngine = new(httpClient);
    private readonly HttpEngine<AuthenticateRequest, string, AuthenticateResponseContentConverter> _authenticateEngine = new(httpClient);


    public async Task<ResponseContent> GetGrid(GetGridRequest request)
    {
        if (request.Credential is not null)
        {
            var token = await GetLoginToken(request.Credential);
            request.Token = token;
        }

        return await _getGridEngine.QueryAsync(request);
    }
    public async Task<List<GridInfo>> GetActualGrids(GetActualGridsRequest request)
    {
        if (request.Credential is not null)
        {
            var token = await GetLoginToken(request.Credential);
            request.Token = token;
        }

        return await _getActualGridsEngine.QueryAsync(request);
    }
    public async Task<string> GetLoginToken(NetworkCredential credentials)
    {
        ArgumentNullException.ThrowIfNull(credentials);

        var request = new AuthenticateRequest
        {
            Email = credentials.UserName, 
            Password  = credentials.Password 
        };

        return await _authenticateEngine.QueryAsync(request);
    }
}
