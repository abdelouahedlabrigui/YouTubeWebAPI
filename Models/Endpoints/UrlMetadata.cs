using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Endpoints
{
    public class UrlMetadata
    {
        public string? ControllerName { get; set; }
        public string? ActionName { get; set; }
        public string? HttpMethod { get; set; }
        public List<string>? MethodParameterName { get; set; }
        public List<string>? MethodParameterType { get; set; }
        public string? RouteTemplate { get; set; }
        public UrlMetadata()
        {
            MethodParameterName = new List<string>();
            MethodParameterType = new List<string>();
        }
    }
}