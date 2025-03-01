using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouTubeWebAPI.Models.Endpoints;

namespace YouTubeWebAPI.Middlewares.Interfaces
{
    public interface IRouteProvider
    {
        IEnumerable<UrlMetadata> GetRoutes();
    }
}