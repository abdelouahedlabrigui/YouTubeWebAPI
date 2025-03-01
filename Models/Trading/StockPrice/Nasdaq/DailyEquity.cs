using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Trading.StockPrice.Nasdaq
{
    public class DailyEquity
    {
        public int Id {get;set;}
        public string? Plot {get;set;}
        public string? ESTranslation {get;set;}
        public string? Description {get;set;}
        public string? CreatedAT {get;set;}
    }
}