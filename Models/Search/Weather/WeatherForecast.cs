using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Search.Weather
{
    public class WeatherVisualization
    {
        public int Id { get; set; }
        public string? City { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? State { get; set; }
        public string? TemperatureStats { get; set; }
        public string? HumidityStats { get; set; }
        public string? WindSpeedStats { get; set; }
        public string? EncodedImageCurrent { get; set; }
        public string? EncodedImageHourly { get; set; }
        public string? Interpretation { get; set; }
        public string? CreatedAT { get; set; }
    }
}