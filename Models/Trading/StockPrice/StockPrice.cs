using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Trading.StockPrice
{
    public class StockPrice
    {
        public int Id { get; set; }
        public string? Company { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? Filename { get; set; }
        public string? CreatedAT { get; set; }
    }
}