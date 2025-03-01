using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Search.Weather.Staistics
{
    public class WeatherTemperature
    {
        public int Id { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public double Mean { get; set; }
        public double Median { get; set; }
        public double Std_dev { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        public string? CreatedAT { get; set; }
    }
}