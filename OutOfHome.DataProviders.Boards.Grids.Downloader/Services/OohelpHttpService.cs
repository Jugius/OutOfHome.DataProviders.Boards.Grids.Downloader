﻿using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp.Common;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Services;
public class OohelpHttpService
{
    private readonly HttpClient _httpClient;
    private readonly HttpEngine<GetGridExtendedRequest, ResponseContent, ZipContentParser> _getGridExtendedEngine;
    private readonly HttpEngine<GetGridRequest, ResponseContent, ZipContentParser> _getGridEngine;

    public OohelpHttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _getGridExtendedEngine = new HttpEngine<GetGridExtendedRequest, ResponseContent, ZipContentParser>(_httpClient);
        _getGridEngine = new HttpEngine<GetGridRequest, ResponseContent, ZipContentParser>(_httpClient);
    }
    public OohelpHttpService() : this(HttpClientFactory.CreateDefaultHttpClient())
    {
    }


    public async Task<ResponseContent> GetGrid(GetGridRequest request) => await _getGridEngine.QueryAsync(request);
    public async Task<ResponseContent> GetGrid(GetGridExtendedRequest request) => await _getGridExtendedEngine.QueryAsync(request);

}