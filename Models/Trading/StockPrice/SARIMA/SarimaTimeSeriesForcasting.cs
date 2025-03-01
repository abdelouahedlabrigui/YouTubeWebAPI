using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Trading.StockPrice.SARIMA
{
    public class SarimaTimeSeriesForcasting
    {
        public List<SarimaMetric>? SarimaMetricsList { get; set; }
        public List<SarimaVisualization>? SarimaVisualizationsList { get; set; }
        public List<StockPriceSarimaCoefficient>? StockPriceSarimaCoefficientsList { get; set; }

    }
}