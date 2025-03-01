using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Documents.LanguageFeatures.Multilingual
{
    public class MultilingualEntity
    {
        public List<Entity>? English { get; set; }
        public List<Entity>? Spanish { get; set; }
    }
}