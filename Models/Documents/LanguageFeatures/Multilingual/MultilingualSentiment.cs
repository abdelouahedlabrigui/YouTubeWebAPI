using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Documents.LanguageFeatures.Multilingual
{
    public class MultilingualSentiment
    {
        public List<Sentiment>? English { get; set; }
        public List<Sentiment>? Spanish { get; set; }
    }
}