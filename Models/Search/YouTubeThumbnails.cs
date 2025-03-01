using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Search
{
    public class YouTubeThumbnails
    {
        public YouTubeThumbnail? Default { get; set; }
        public YouTubeThumbnail? Medium { get; set; }
        public YouTubeThumbnail? High { get; set; }
    }
}