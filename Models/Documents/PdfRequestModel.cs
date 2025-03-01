using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeWebAPI.Models.Documents
{
    public class PdfRequestModel
    {
        public string? FileName { get; set; }
        public string? Base64Content { get; set; }
    }
}