using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Prompts
{
    public class PromptRawResult
    {
        public int Id { get; set; }
        [Required]
        public string? PromptString { get; set; }
        [Required]
        public string? TextPath { get; set; }
        [Required]
        public string? CreatedAT { get; set; }

    }
}