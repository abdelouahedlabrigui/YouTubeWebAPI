using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using YouTubeWebAPI.Models.ApiKeys;
using YouTubeWebAPI.Models.Astronomy;

namespace YouTubeWebAPI.Repository
{
    public class NasaApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string? _apiKey;

        public NasaApiService(HttpClient httpClient, IOptions<NasaApiSettings> options)
        {
            _httpClient = httpClient;
            _apiKey = options.Value.ApiKey;
        }

        // public async Task<string> GetNearEarthObjectsAsync(string startDate, string endDate){
        //     var url = $"neo/rest/v1/feed?start_date={startDate}&end_date={endDate}&api_key={_apiKey}";
        //     var response = await _httpClient.GetAsync(url);
        //     if (!response.IsSuccessStatusCode)
        //     {
        //         throw new Exception($"NASA API request failed: {response.StatusCode}");
        //     }

        //     return await response.Content.ReadAsStringAsync();
        // }
        public async Task<List<NearEarthObjects>> GetNearEarthObjectsAsync(string startDate, string endDate)
        {
            var url = $"neo/rest/v1/feed?start_date={startDate}&end_date={endDate}&api_key={_apiKey}";
            var response = await _httpClient.GetAsync(url);
            
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"NASA API request failed: {response.StatusCode}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<JsonElement>(jsonResponse);

            var neoList = new List<NearEarthObjects>();
            if (data.TryGetProperty("near_earth_objects", out JsonElement neoObjects))
            {
                foreach (var dateProperty in neoObjects.EnumerateObject())
                {
                    foreach (var obj in dateProperty.Value.EnumerateArray())
                    {
                        var approachData = obj.GetProperty("close_approach_data").EnumerateArray().FirstOrDefault();
                        
                        neoList.Add(new NearEarthObjects
                        {
                            Id = int.Parse(obj.GetProperty("id").GetString() ?? "0"),
                            Name = obj.GetProperty("name").GetString(),
                            Date = DateTime.TryParse(dateProperty.Name, out var parsedDate) ? parsedDate : (DateTime?)null,
                            AbsoluteMagnitudeH = obj.GetProperty("absolute_magnitude_h").GetDouble(),
                            EstimatedDiameterMin = obj.GetProperty("estimated_diameter")
                                                    .GetProperty("kilometers")
                                                    .GetProperty("estimated_diameter_min")
                                                    .GetDouble(),
                            EstimatedDiameterMax = obj.GetProperty("estimated_diameter")
                                                    .GetProperty("kilometers")
                                                    .GetProperty("estimated_diameter_max")
                                                    .GetDouble(),
                            IsPotentiallyHazardousAsteroid = obj.GetProperty("is_potentially_hazardous_asteroid").GetBoolean(),
                            CloseApproachDate = approachData.TryGetProperty("close_approach_date", out var closeApproachDate) 
                                            ? DateTime.Parse(closeApproachDate.GetString()) 
                                            : (DateTime?)null,
                            RelativeVelocityKilometersPerSecond = approachData.GetProperty("relative_velocity").GetProperty("kilometers_per_second").GetString(),
                            RelativeVelocityKilometersPerHour = approachData.GetProperty("relative_velocity").GetProperty("kilometers_per_hour").GetString(),
                            RelativeVelocityMilesPerHour = approachData.GetProperty("relative_velocity").GetProperty("miles_per_hour").GetString(),
                            MissDistanceKilometers = approachData.GetProperty("miss_distance").GetProperty("kilometers").GetString(),
                            MissDistanceAstronomical = approachData.GetProperty("miss_distance").GetProperty("astronomical").GetString(),
                            MissDistanceLunar = approachData.GetProperty("miss_distance").GetProperty("lunar").GetString(),
                            MissDistanceMiles = approachData.GetProperty("miss_distance").GetProperty("miles").GetString(),
                            OrbitingBody = approachData.GetProperty("orbiting_body").GetString()
                        });
                    }
                }
            }
            
            return neoList;
        }

    }
}