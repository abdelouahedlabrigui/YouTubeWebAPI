using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.NLP.RecursiveSearch.ContextSearch
{
    public class NationalityContext
    {
        public int Id { get; set; }
        public string? Nationalities { get; set; }
        public string? Context { get; set; }
        public string? FileName { get; set; }
        public string? CreatedAT { get; set; }
        
    }
}