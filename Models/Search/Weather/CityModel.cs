using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Search.Weather
{
    public class CityModel
    {
        public int Id { get; set; }
        public string? City { get; set; }
        public string? Growth_from_2000_to_2013 { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Population { get; set; }
        public int Rank { get; set; }
        public string? State { get; set; }
    }
}