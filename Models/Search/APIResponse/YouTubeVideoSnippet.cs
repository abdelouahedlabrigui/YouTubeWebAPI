using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Search.APIResponse
{
    public class YouTubeVideoSnippet
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset? PublishedAt { get; set; }
        public string? ChannelTitle { get; set; }
    }
}