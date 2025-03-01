using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Search.GoogleSearch
{
    public class Item
    {
        public string? Title { get; set; }
        public string? Link { get; set; }
        public string? Snippet { get; set; }
    }
}