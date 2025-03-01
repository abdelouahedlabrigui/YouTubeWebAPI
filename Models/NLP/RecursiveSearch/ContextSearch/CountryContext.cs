using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.NLP.RecursiveSearch.ContextSearch
{
    public class CountryContext
    {
        public int Id { get; set; }
        public string? Countries { get; set; }
        public string? Context { get; set; }
        public string? FileName { get; set; }
        public string? CreatedAT { get; set; }
        
    }
}