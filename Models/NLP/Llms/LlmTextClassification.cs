using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.NLP.Llms
{
    public class LlmTextClassification
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public string? Label { get; set; }
        public float Score { get; set; }
        public string? CreatedAT { get; set; }
    }
}