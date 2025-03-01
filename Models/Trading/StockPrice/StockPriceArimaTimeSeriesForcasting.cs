using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Trading.StockPrice
{
    public class StockPriceArimaTimeSeriesForcasting
    {
        public List<ArimaVisualization>? ArimaVisualizationList { get; set; }
        public List<StockPriceAdfTestCriticalValue>? StockPriceAdfTestCriticalValueList { get; set; }
        public List<StockPriceAdfTestResult>? StockPriceAdfTestResultList { get; set; }
        public List<StockPriceArimaCoefficient>? StockPriceArimaCoefficientList { get; set; }
        public List<StockPriceArimaMetric>? StockPriceArimaMetricList { get; set; }
        
    }
}