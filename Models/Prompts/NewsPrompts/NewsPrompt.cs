using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Prompts.NewsPrompts
{
    public class NewsPrompt
    {
        public int Id { get; set; }
        public string? Title { get; set; } 
        public string? Keywords { get; set; }
        public string? PromptString { get; set; }
        public string? Output { get; set; }
        public string? CreatedAT { get; set; }
    }
}