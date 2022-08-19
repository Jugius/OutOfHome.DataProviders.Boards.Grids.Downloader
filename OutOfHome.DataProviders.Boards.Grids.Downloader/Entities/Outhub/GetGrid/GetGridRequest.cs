using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

using System.Threading.Tasks;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Outhub.GetGrid;

public class GetGridRequest : BaseOuthubRequest, Interfaces.IRequestPost
{
    protected internal override string BaseUrl => base.BaseUrl + "api/booking/GetBoardsData";
    public HttpContent GetContent() => null;

    
}
