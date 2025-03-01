using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.NLP.RecursiveSearch
{
    public class ExtractEntity
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public string? Label { get; set; }
        public string? FileName { get; set; }
        public string? CreatedAT { get; set; }
    }
}