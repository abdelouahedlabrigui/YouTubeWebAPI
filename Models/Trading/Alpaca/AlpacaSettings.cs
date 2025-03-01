using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Trading.Alpaca
{
    public class AlpacaSettings
    {
        public string? BaseUrl { get; set; } = string.Empty;
        public string? Key { get; set; } = string.Empty;
        public string? Secret { get; set; } = string.Empty;
    }
}