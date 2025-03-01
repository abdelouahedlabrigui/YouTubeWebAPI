using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Trading.StockPrice.Indicators
{
    public class DbTechnicalIndicator
    {
        public int Id { get; set; }
        public string? Ticker { get; set; }
        public string? Date { get; set; }
        public double Close { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Open { get; set; }
        public double Volume { get; set; }
        public double SMA_20 { get; set; }
        public double EMA_20 { get; set; }
        public double RSI_14 { get; set; }
        public double MACD { get; set; }
        public double MACD_Signal { get; set; }
        public double Upper_BB { get; set; }
        public double Lower_BB { get; set; }
        public double Stochastic_K { get; set; }
    }
}