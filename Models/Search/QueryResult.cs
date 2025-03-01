using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Search
{
    public class QueryResult
    {
        public int Id { get; set; }
        public string? VideoId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? PublishedAt { get; set; }
        public string? ChannelTitle { get; set; }
        public string? ThumbnailUrl { get; set; } // Added ThumbnailUrl
        public TimeSpan Duration { get; set; } //Added Duration
    }
}

// string.IsNullOrEmpty(videoId) || string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(publishedAt) || string.IsNullOrEmpty(channelTitle) || string.IsNullOrEmpty(thumbnailUrl) || string.IsNullOrEmpty(duration)