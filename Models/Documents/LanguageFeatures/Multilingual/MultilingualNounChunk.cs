using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Documents.LanguageFeatures.Multilingual
{
    public class MultilingualNounChunk
    {
        public List<NounChunk>? English { get; set; }
        public List<NounChunk>? Spanish { get; set; }
    }
}