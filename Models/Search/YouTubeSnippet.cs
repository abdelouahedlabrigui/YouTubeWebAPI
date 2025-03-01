using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Search
{
    public class YouTubeSnippet
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? PublishedAt { get; set; }
        public string? ChannelTitle { get; set; }
        public YouTubeThumbnails? Thumbnails { get; set; }
    }
}