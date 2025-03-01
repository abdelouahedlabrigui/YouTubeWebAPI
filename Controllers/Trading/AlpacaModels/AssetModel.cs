using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Controllers.Trading.AlpacaModels
{
    public class AssetModel
    {
        public string? AssetId { get; set; }
        public int Class { get; set; }  // Enum might be better
        public int Exchange { get; set; }  // Enum might be better
        public string Symbol { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Status { get; set; }  // Enum might be better
        public bool IsTradable { get; set; }
        public bool Marginable { get; set; }
        public bool Shortable { get; set; }
        public bool EasyToBorrow { get; set; }
        public bool Fractionable { get; set; }
        public decimal? MinOrderSize { get; set; }
        public decimal? MinTradeIncrement { get; set; }
        public decimal? PriceIncrement { get; set; }
        public decimal? MaintenanceMarginRequirement { get; set; }
        public List<int> Attributes { get; set; } = new();
    }
}