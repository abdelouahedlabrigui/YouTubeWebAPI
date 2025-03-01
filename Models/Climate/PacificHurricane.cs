using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Climate
{
    public class PacificHurricane
    {
        public int LocalId { get; set; }
        public string? ID { get; set; }
        public string? Name { get; set; }
        public string? Date { get; set; }
        public string? Time { get; set; }
        public string? Event { get; set; }
        public string? Status { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? Maximum_Wind { get; set; }
        public string? Minimum_Pressure { get; set; }
        public string? Low_Wind_NE { get; set; }
        public string? Low_Wind_SE { get; set; }
        public string? Low_Wind_SW { get; set; }
        public string? Low_Wind_NW { get; set; }
        public string? Moderate_Wind_NE { get; set; }
        public string? Moderate_Wind_SE { get; set; }
        public string? Moderate_Wind_SW { get; set; }
        public string? Moderate_Wind_NW { get; set; }
        public string? High_Wind_NE { get; set; }
        public string? High_Wind_SE { get; set; }
        public string? High_Wind_SW { get; set; }
        public string? High_Wind_N { get; set; }
    }
}