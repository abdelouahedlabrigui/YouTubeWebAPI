using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Trading.StockPrice.Indicators
{
    public class SummaryStatsModel
    {
        public string? Ticker { get; set; }
        public decimal AvgClose { get; set; }
        public decimal MinClose { get; set; }
        public decimal MaxClose { get; set; }
        public decimal MedianClose { get; set; }
        public decimal Percentile25Close { get; set; }
        public decimal Percentile75Close { get; set; }
        public decimal MinVolume { get; set; }
        public decimal MaxVolume { get; set; }
        public decimal MedianVolume { get; set; }
        public decimal Percentile25Volume { get; set; }
        public decimal Percentile75Volume { get; set; }
    }
}