using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Astronomy
{
    public class NearEarthObjects
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? Date { get; set; }
        public double AbsoluteMagnitudeH { get; set; }
        public double EstimatedDiameterMin { get; set; }
        public double EstimatedDiameterMax { get; set; }
        public bool IsPotentiallyHazardousAsteroid { get; set; }
        public DateTime? CloseApproachDate { get; set; }
        public string? RelativeVelocityKilometersPerSecond { get; set; }
        public string? RelativeVelocityKilometersPerHour { get; set; }
        public string? RelativeVelocityMilesPerHour { get; set; }
        public string? MissDistanceKilometers { get; set; }
        public string? MissDistanceAstronomical { get; set; }
        public string? MissDistanceLunar { get; set; }
        public string? MissDistanceMiles { get; set; }
        public string? OrbitingBody { get; set; }

    }
}