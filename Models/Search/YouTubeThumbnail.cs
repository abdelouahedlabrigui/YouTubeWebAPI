using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Search
{
    public class YouTubeThumbnail
    {
        public string? Url { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}