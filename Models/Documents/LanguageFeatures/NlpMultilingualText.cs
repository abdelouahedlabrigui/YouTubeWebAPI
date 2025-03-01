using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouTubeWebAPI.Models.Documents.LanguageFeatures.Multilingual;

namespace YouTubeWebAPI.Models.Documents.LanguageFeatures
{
    public class NlpMultilingualText
    {
        public string? EnglishText { get; set; }
        public string? SpanishText { get; set; }
        public List<MultilingualEntity>? Entities { get; set; }
        public List<MultilingualSentiment>? Sentiments { get; set; }
        public List<MultilingualNounChunk>? NounChunks { get; set; }
    }
}