using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.NLP.Llms.Questions
{
    public class QuestionsStore
    {
        public int Id { get; set; }
        public string? TextPath { get; set; }
        public string? Title { get; set; }
        public string? SearchString { get; set; }
        public string? Question { get; set; }
        public string? CreatedAT { get; set; }
    }
}