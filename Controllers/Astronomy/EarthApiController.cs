using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YouTubeWebAPI.Models.Astronomy;
using YouTubeWebAPI.Models.Responses;
using YouTubeWebAPI.Repository;

namespace YouTubeWebAPI.Controllers.Astronomy
{
    [ApiController]
    [Route("api/[controller]")]
    public class EarthApiController : ControllerBase
    {
        private readonly NasaApiService _nasaApiService;

        public EarthApiController(NasaApiService nasaApiService)
        {
            _nasaApiService = nasaApiService;
        }

        [HttpGet("process-csv")]
        public async Task<IActionResult> ProcessCsv(string startDate, string endDate, string export_file){
            try
            {
                if (int.Parse(startDate.Split('-')[0]) > int.Parse(endDate.Split('-')[0]) || !export_file.Contains(".csv"))
                {
                    return BadRequest(new RequestResponse {Message = "Invalid input(s)."});
                }
                var jsonData = await _nasaApiService.GetNearEarthObjectsAsync(startDate, endDate);
                return Ok(jsonData);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse { Message = ex.Message });
            }
        }
        static IFormFile ConvertToIFormFile(string filePath)
        {   
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            MemoryStream memoryStream = new MemoryStream(fileBytes);
            IFormFile file = new FormFile(memoryStream, 0, memoryStream.Length, "file", Path.GetFileName(filePath));
            return file;
        }
        private List<DateTime> ReadCsvDates(string filePath){
            var dates = new List<DateTime>();

            using (var reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (DateTime.TryParseExact(line, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                    {
                        dates.Add(date);
                    }
                }
            }

            return dates;
        }

        private async Task SaveToCsv(List<string> data, string filePath){
            using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                foreach (var json in data)
                {
                    var records = JsonSerializer.Deserialize<List<SolarFlareRecord>>(json);
                    if (records != null)
                    {
                        foreach (var record in records)
                        {
                            await writer.WriteLineAsync($"{record.FlareID};{record.BeginTime};{record.PeakTime};{record.EndTime};{record.ClassType};{record.SourceLocation}");
                        }
                    }
                }
            }
        }
    }
}