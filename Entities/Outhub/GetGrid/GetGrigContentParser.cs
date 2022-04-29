using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Outhub.GetGrid;

public class GetGrigContentParser : Interfaces.IContentDeserializer<GetGridResponse>
{
    public async Task<GetGridResponse> DeserializeAsync(HttpResponseMessage message)
    {
        throw new NotImplementedException();
    }
}
