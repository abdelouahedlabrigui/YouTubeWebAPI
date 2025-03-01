using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Search.NewsAPI
{
    public class Person
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Role { get; set; }
        public string? Birthdate { get; set; }
        public string? State { get; set; }
        public string? CreatedAT { get; set; }
    }
}