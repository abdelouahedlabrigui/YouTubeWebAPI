using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.NLP.RecursiveSearch.ContextSearch
{
    public class QuestionAnswering
    {
        public int Id { get; set; }
        public string? Sentence { get; set; }
        public string? Question { get; set; }
        public string? Type { get; set; }
        public string? Answer { get; set; }
        public string? End { get; set; }
        public string? Score { get; set; }
        public string? Start { get; set; }
        public string? Match { get; set; }
        public string? Positive { get; set; }
        public string? Negative { get; set; }
        public string? Neutral { get; set; }
        public string? Compound { get; set; }
        public string? CreatedAT { get; set; }
    }
}