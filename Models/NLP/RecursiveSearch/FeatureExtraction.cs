using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.NLP.RecursiveSearch
{
    public class FeatureExtraction
    {
        public int Id { get; set; }
        public string? Context { get; set; }
        public string? Sentiment { get; set; }
        public string? SentimentScore { get; set; }
        public string? ExtractedAnswer { get; set; }
        public string? ExtractedScore { get; set; }
        public string? Events { get; set; }
        public string? EventScore { get; set; }
        public string? CreatedAT { get; set; }
    }
}