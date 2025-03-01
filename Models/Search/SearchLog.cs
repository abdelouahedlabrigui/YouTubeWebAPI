using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Search
{
    public class SearchLog
    {
        public int Id { get; set; }
        [Required]
        public string? Search { get; set; }
        [Required]
        public int MaxResults { get; set; }
        [Required]
        public string? SearchDate { get; set; }
    }
}