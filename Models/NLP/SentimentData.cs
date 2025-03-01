using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.NLP
{
    public class SentimentData
    {
        public string? Text { get; set; }
        public bool Label { get; set; }
    }
}