using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Documents
{
    public class TextDocument
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? OfflineUrl { get; set; }
        public string? Name { get; set; }
        public string? LastWriteTime { get; set; }
        public string? LastAccessTime { get; set; }
        public string? Length { get; set; }
        public string? CreatedAT { get; set; }
    }
}