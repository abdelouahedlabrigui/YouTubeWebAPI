using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.NLP.RecursiveSearch.MatchSearch
{
    public class MatchPersonContext
    {
        public int Id { get; set; }
        public string? Person { get; set; }
        public string? Context { get; set; }
        public string? SearchTerm { get; set; }
        public string? SelectedChunks { get; set; }
        public string? FileName { get; set; }
        public string? CreatedAT { get; set; }
    }
}