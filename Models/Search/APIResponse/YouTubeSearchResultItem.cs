using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Search.APIResponse
{
    public class YouTubeSearchResultItem
    {
        public YouTubeVideoId? Id { get; set; }
        public YouTubeSnippet? Snippet { get; set; }
    }
}