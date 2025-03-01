using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Trading.StockPrice
{
    public class StockPriceAdfTestCriticalValue
    {
        public int Id { get; set; }
        public string? Ticker { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? Stationarity { get; set; }
        public string? CriticalValues { get; set; }
        public string? CreatedAT { get; set; }
    }
}