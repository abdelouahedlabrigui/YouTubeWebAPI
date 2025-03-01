using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Trading.StockPrice
{
    public class StockPriceArimaMetric
    {
        public int Id { get; set; }
        public string? Ticker { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public double AIC { get; set; }
        public double BIC { get; set; }
        public string? CreatedAT { get; set; }
    }
}