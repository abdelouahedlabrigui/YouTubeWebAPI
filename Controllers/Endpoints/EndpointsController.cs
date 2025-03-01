using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YouTubeWebAPI.Middlewares.Interfaces;
using YouTubeWebAPI.Models.Endpoints;
using YouTubeWebAPI.Models.Responses;

namespace YouTubeWebAPI.Controllers.Endpoints
{
    [ApiController]
    [Route("api/[controller]")]
    public class EndpointsController : ControllerBase
    {
        private readonly IRouteProvider _routeProvider;
        public EndpointsController(IRouteProvider routeProvider)
        {
            _routeProvider = routeProvider;
        }
        [HttpGet("get-endpoints")]
        public IActionResult GetEndpointAsync()
        {
            try
            {
                // using (var httpClient = new HttpClient())
                // {
                //     var response = await httpClient.GetAsync("http://localhost:5082/api/Endpoints");
                //     response.EnsureSuccessStatusCode();
                //     string content = await response.Content.ReadAsStringAsync();
                //     var data = JsonConvert.DeserializeObject<IEnumerable<UrlMetadata>>(content);
                //     return Ok(data); 
                // }
                var routes = _routeProvider.GetRoutes();
                return Ok(routes);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = $"HTTP request failed: {ex.Message}"});
            } 
        }
    }
}