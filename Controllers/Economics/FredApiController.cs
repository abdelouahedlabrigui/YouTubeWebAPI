using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YouTubeWebAPI.Models;
using YouTubeWebAPI.Models.Economics;
using YouTubeWebAPI.Models.Responses;

namespace YouTubeWebAPI.Controllers.Economics
{
    [ApiController]
    [Route("api/[controller]")]
    public class FredApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FredApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("get-fred-api-endpoint")]
        public async Task<IActionResult> GetFredAPIEndpoint(int? id){
            try
            {
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    return NotFound();
                }

                var query = await _context.FredAPIEndpoints
                    .Where(w => w.Id == id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return NoContent();
                } else {
                    return Ok(query);
                }
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("search-fred-api-endpoints")]
        public async Task<IActionResult> SearchFredAPIEndpoints(string search){
            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    return NotFound();
                }

                var query = await _context.FredAPIEndpoints
                    .Where(w => w.Description.Contains($"{search}") || w.GetRequest.Contains($"{search}"))
                    .OrderByDescending(o => o.Id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return NoContent();
                } else {
                    return Ok(query);
                }
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("get-fred-api-endpoints")]
        public async Task<IActionResult> GetFredAPIEndpoints(){
            try
            {
                var query = await _context.FredAPIEndpoints
                    .Take(10)
                    .OrderByDescending(o => o.Id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return NoContent();
                } else {
                    return Ok(query);
                }
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("DeleteFredAPIEndpoint")]
        public async Task<IActionResult> DeleteFredAPIEndpoint(int id){
            try
            {
                var query = await _context.FredAPIEndpoints.Where(w => w.Id == id).ToListAsync();
                _context.FredAPIEndpoints?.Remove(query[0]);
                await _context.SaveChangesAsync();
                return Ok(new RequestResponse {Message = $"FredAPIEndpoint deleted successfully."});
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("post-fred-api-endpoints")]
        public async Task<IActionResult> PostFredAPIEndpoint(string description, string getRequest){
            try
            {
                if (string.IsNullOrEmpty(getRequest) || string.IsNullOrEmpty(description))
                {
                    return NotFound();
                }

                FredAPIEndpoint fredAPI = new FredAPIEndpoint{
                    Description = description.Trim(),
                    GetRequest = getRequest.Trim(),
                    CreatedAT = DateTime.Now.ToString()
                };
                var check = await _context.FredAPIEndpoints
                    .Where(w => w.GetRequest == getRequest)
                    .ToListAsync(); 
                if (check.Count > 0)
                {
                    return Ok(new RequestResponse {Message = $"FredAPIEndpoint already exists."});;
                } else {
                    await _context.FredAPIEndpoints.AddAsync(fredAPI);
                    await _context.SaveChangesAsync();
                    return Ok(new RequestResponse {Message = $"FredAPIEndpoint created successfully."});
                }
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}