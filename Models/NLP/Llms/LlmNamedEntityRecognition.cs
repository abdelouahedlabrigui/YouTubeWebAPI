using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.NLP.Llms
{
    public class LlmNamedEntityRecognition
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? EntityGroup { get; set; }
        public float Score { get; set; }
        public string? Word { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public string? CreatedAT { get; set; }
    }
}