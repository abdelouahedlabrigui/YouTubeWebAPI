using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Search.APIResponse
{
    public class YouTubeVideoItem
    {
        public YouTubeVideoId? Id { get; set; }
        public YouTubeVideoSnippet? Snippet { get; set; } = null;
    }
}