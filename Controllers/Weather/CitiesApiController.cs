using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using YouTubeWebAPI.Models;
using YouTubeWebAPI.Models.Responses;
using YouTubeWebAPI.Models.Search.Weather;

namespace YouTubeWebAPI.Controllers.Weather
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesApiController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CitiesApiController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("DeleteAllCities")]
        public async Task<IActionResult> DeleteAllCities(){
            try
            {
                var query = await _context.Cities.ToListAsync();
                if (query.Count == 0)
                {
                    return NoContent();
                } else {
                    _context.Cities.RemoveRange(query);
                    await _context.SaveChangesAsync();
                    return Ok(new RequestResponse {Message = $"All cities deleted successfully."});
                }
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("search-latitude-longitude")]
        public async Task<IActionResult> SearchCity(string city){
            try
            {
                if (string.IsNullOrEmpty(city))
                {
                    return NotFound();
                }

                var query = await _context.Cities
                    .Where(w => w.City.Contains($"{city}"))
                    .OrderByDescending(o => o.Id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return NoContent();
                } else {
                    return Ok(query[0]);
                }
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("get-cities")]
        public async Task<IActionResult> GetCities(){
            try
            {
                var query = await _context.Cities
                    // .Take(10)
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
        [HttpPost("post-cities-from-file")]
        public async Task<IActionResult> PostCitiesFromFile()
        {
            try
            {
                // Path to the JSON file
                var filePath = @"C:\Users\dell\Entrepreneurship\Engineering\Scripts\Algorithms\AtmosphericStudies\APIs\cities.json";

                // Check if the file exists
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("The file cities.json was not found.");
                }

                // Read the JSON file
                var jsonData = await System.IO.File.ReadAllTextAsync(filePath);

                // Deserialize JSON to a list of CityModel
                var cities = JsonConvert.DeserializeObject<List<CityModel>>(jsonData);

                if (cities == null || !cities.Any())
                {
                    return BadRequest("No valid data found in the JSON file.");
                }

                // Add the cities to the database
                await _context.Cities.AddRangeAsync(cities);
                await _context.SaveChangesAsync();

                return Ok(new { Message = $"{cities.Count} cities were added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}