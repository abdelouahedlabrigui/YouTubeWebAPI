using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouTubeWebAPI.Models.Search.GoogleSearch;
using Microsoft.AspNetCore.Mvc;
using YouTubeWebAPI.Repository;
using Microsoft.AspNetCore.Authorization;

namespace YouTubeWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GoogleSearchApiController : ControllerBase
    {
        private readonly GoogleSearchService _googleSearchService;

        public GoogleSearchApiController(GoogleSearchService googleSearchService)
        {
            _googleSearchService = googleSearchService;
        }

        [HttpGet("search")]
        [Authorize]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest(new {Message = "Query Parameter is required"});
            }
            try
            {
                List<SearchResult> results = await _googleSearchService.SearchAsync(query);
                return Ok(results);               
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new {Message = ex.Message});
            }            
        }
    }
}