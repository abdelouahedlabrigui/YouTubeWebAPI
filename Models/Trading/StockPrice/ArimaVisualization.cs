using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Trading.StockPrice
{
    public class ArimaVisualization
    {
        
        public int Id { get; set; }
        public string? Ticker { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? VisualizedTrendsAndSeasonality { get; set; }
        public string? VisualizedDifferencedTimeSeries { get; set; }
        public string? VisualizedARIMAModelEvaluation { get; set; }
        public string? VisualizedForecastFutureValues { get; set; }
        public string? VisualizedARIMAModelSummary { get; set; }
        public string? CreatedAT { get; set; }
    }
}