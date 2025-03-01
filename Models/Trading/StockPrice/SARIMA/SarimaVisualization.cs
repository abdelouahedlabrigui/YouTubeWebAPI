using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Trading.StockPrice.SARIMA
{
    public class SarimaVisualization
    {
        public int Id { get; set; }
        public string? Ticker { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? VisualizatedForecast { get; set; }
        public string? VisualizedSARIMAModelSummary { get; set; }
        public string? CreatedAT { get; set; }
    }
}