using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YouTubeWebAPI.Models;
using YouTubeWebAPI.Models.Responses;
using YouTubeWebAPI.Models.Search.Weather;
using YouTubeWebAPI.Models.Search.Weather.Staistics;

namespace YouTubeWebAPI.Controllers.Weather
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WeatherApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("get-weather")]
        public async Task<IActionResult> GetWeather(int? id){
            try
            {
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    return NotFound();
                }

                var query = await _context.Weathers
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
        [HttpGet("search-weather")]
        public async Task<IActionResult> SearchWeathers(string search){
            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    return NotFound();
                }

                var query = await _context.Weathers
                    .Where(w => w.Interpretation.Contains($"{search}"))
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
        [HttpGet("get-weathers")]
        public async Task<IActionResult> GetWeathers(){
            try
            {
                var query = await _context.Weathers
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
        [HttpGet("DeleteWeather")]
        public async Task<IActionResult> DeleteWeather(int id){
            try
            {
                var query = await _context.Weathers.Where(w => w.Id == id).ToListAsync();
                _context.Weathers?.Remove(query[0]);
                await _context.SaveChangesAsync();
                return Ok(new RequestResponse {Message = $"Weather deleted successfully."});
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("post-weather-visualization")]
        public async Task<IActionResult> PostWeather(
            string latitude, 
            string longitude, 
            string city, 
            string state){
            try
            { 
                if (
                    string.IsNullOrEmpty(latitude) || 
                    string.IsNullOrEmpty(longitude) || 
                    string.IsNullOrEmpty(city) || 
                    string.IsNullOrEmpty(state))
                {
                    return NotFound();
                }
                HttpClient _httpClient = new HttpClient();
                WeatherVisualization weather = await _httpClient.GetFromJsonAsync<WeatherVisualization>($"http://127.0.0.1:5005/weather/weather_visualization?latitude={latitude}&longitude={longitude}&city={city}&state={state}");
                if (weather == null)
                {
                    return NotFound();
                }                
                await _context.Weathers.AddAsync(weather);
                await _context.SaveChangesAsync();
                return Ok(new RequestResponse {Message = $"Weather created successfully."});
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }        
        [HttpGet("get-weather-statistical-indicators-by-city")]
        public async Task<IActionResult> GetWeatherStatisticalIndicatorsByCity(string city){
            try
            {
                if (string.IsNullOrEmpty(city))
                {
                    return NotFound();
                }
                List<Humidity> humidities = await _context.Humidities.Where(w => w.City == city).ToListAsync();
                List<WeatherTemperature> temperatures = await _context.Temperatures.Where(w => w.City == city).ToListAsync();
                List<WindSpeed> windSpeeds = await _context.WindSpeeds.Where(w => w.City == city).ToListAsync();

                if (humidities.Count == 0 || temperatures.Count == 0 || windSpeeds.Count == 0){
                    return NoContent();
                }

                WeatherStatisticalIndicator indicators = new WeatherStatisticalIndicator{
                    Humidities = humidities,
                    WeatherTemperatures = temperatures,
                    WindSpeeds = windSpeeds,
                };

                return Ok(indicators);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }   


        [HttpGet("post-windSpeed-statistical-indicators")]
        public async Task<IActionResult> PostWindSpeedStatisticalIndicators(            
            double? mean,
            double? median,
            double? std_dev,
            double? min,
            double? max, 
            string city, 
            string state){
            try
            {
                if (
                    mean == null ||
                    median == null ||
                    std_dev == null ||
                    min == null ||
                    max == null ||
                    state == null ||
                    city == null)
                {
                    return NotFound();
                }
                WindSpeed windSpeed = new WindSpeed{
                    City = city,
                    State = state,
                    Mean = mean ??  0.0,
                    Median = median ??  0.0,
                    Std_dev = std_dev ??  0.0,
                    Min = min ??  0.0,
                    Max = max ??  0.0,
                    CreatedAT = DateTime.Now.ToString(),
                };
                await _context.WindSpeeds.AddAsync(windSpeed);
                await _context.SaveChangesAsync();
                return Ok(new RequestResponse {Message = $"Wind Speed statistical indicators created successfully."});
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("post-humidity-statistical-indicators")]
        public async Task<IActionResult> PostHumidityStatisticalIndicators(
            double? mean,
            double? median,
            double? std_dev,
            double? min,
            double? max, 
            string city, 
            string state){
            try
            {
                if (
                    mean == null ||
                    median == null ||
                    std_dev == null ||
                    min == null ||
                    max == null ||
                    state == null ||
                    city == null)
                {
                    return NotFound();
                }
                Humidity humidity = new Humidity{
                    City = city,
                    State = state,
                    Mean = mean ??  0.0,
                    Median = median ??  0.0,
                    Std_dev = std_dev ??  0.0,
                    Min = min ??  0.0,
                    Max = max ??  0.0,
                    CreatedAT = DateTime.Now.ToString(),
                };
                await _context.Humidities.AddAsync(humidity);
                await _context.SaveChangesAsync();
                return Ok(new RequestResponse {Message = $"Humidity statistical indicators created successfully."});
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("post-temperature-statistical-indicators")]
        public async Task<IActionResult> PostTemperatureStatisticalIndicators(
            double? mean,
            double? median,
            double? std_dev,
            double? min,
            double? max, 
            string city, 
            string state){
            try
            {
                if (
                    mean == null ||
                    median == null ||
                    std_dev == null ||
                    min == null ||
                    max == null ||
                    state == null ||
                    city == null)
                {
                    return NotFound();
                }
                WeatherTemperature temperature = new WeatherTemperature{
                    City = city,
                    State = state,
                    Mean = mean ??  0.0,
                    Median = median ??  0.0,
                    Std_dev = std_dev ??  0.0,
                    Min = min ??  0.0,
                    Max = max ??  0.0,
                    CreatedAT = DateTime.Now.ToString(),
                };
                await _context.Temperatures.AddAsync(temperature);
                await _context.SaveChangesAsync();
                return Ok(new RequestResponse {Message = $"Temperature statistical indicators created successfully."});
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}