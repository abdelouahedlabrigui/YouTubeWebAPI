using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.NLP
{
    public class SentMessageChunks
    {
        public string? Text { get; set; }
        public string? RootText { get; set; }
        public string? RootDep { get; set; }
        public string? RootHead { get; set; }
        public string? CreatedAT { get; set; }
    }
}