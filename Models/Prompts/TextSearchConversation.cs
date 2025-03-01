using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Prompts
{
    public class TextSearchConversation
    {
        public int Id { get; set; }
        public string? PromptString { get; set; }
        public string? Message { get; set; }
        public string? CreatedAT { get; set; }
    }
}