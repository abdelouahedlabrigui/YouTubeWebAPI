using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Astronomy
{
    public class SolarFlareRecord
    {
        public string? FlareID { get; set; }
        public string? BeginTime { get; set; }
        public string? PeakTime { get; set; }
        public string? EndTime { get; set; }
        public string? ClassType { get; set; }
        public string? SourceLocation { get; set; }
    }
}