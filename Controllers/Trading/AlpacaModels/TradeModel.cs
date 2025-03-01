using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Controllers.Trading.AlpacaModels
{
    public class TradeModel
    {
        public DateTime TimestampUtc { get; set; }
        public string Exchange { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal? Size { get; set; }
        public ulong TradeId { get; set; }
        public string Tape { get; set; } = string.Empty;
        public string Update { get; set; } = string.Empty;
        // public List<string> ConditionsList { get; set; } = new();
        public int TakerSide { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public IReadOnlyList<string>? Conditions { get; set; }
    }
}