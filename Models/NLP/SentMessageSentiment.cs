using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.NLP
{
    public class SentMessageSentiment
    {
        public string? Sentence { get; set; } 
        public string? Positive { get; set; }
        public string? Negative { get; set; }
        public string? Neutral { get; set; }
        public string? Compound { get; set; }
        public string? CreatedAT { get; set; }
    }
}