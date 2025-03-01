using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.NLP
{
    public class TextSearchMatch
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? SearchString { get; set; }
        public int Occurrence { get; set; }
        public string? Context { get; set; }
        public string? CreatedAT { get; set; }
    }
}