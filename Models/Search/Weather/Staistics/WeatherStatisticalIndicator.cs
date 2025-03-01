using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Search.Weather.Staistics
{
    public class WeatherStatisticalIndicator
    {
        public List<Humidity>? Humidities { get; set; }
        public List<WeatherTemperature>? WeatherTemperatures { get; set; }
        public List<WindSpeed>? WindSpeeds { get; set; }
    }
}