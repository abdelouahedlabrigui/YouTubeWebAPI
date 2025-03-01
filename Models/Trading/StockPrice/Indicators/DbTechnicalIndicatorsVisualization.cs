using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Trading.StockPrice.Indicators
{
    public class DbTechnicalIndicatorsVisualization
    {
        public int Id { get; set; }
        public string? Visualization { get; set; }
        public string? Ticker { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? CreatedAT { get; set; }
    }
}