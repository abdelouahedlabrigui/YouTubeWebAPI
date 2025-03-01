using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YouTubeWebAPI.Models;
using YouTubeWebAPI.Models.Prompts.NewsPrompts;
using YouTubeWebAPI.Models.Responses;

namespace YouTubeWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsPromptsApiController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;

        public NewsPromptsApiController(IConfiguration configuration, AppDbContext context)
        {
            _configuration = configuration;
            _context = context;
            _httpClient = new HttpClient();
        }

        [HttpGet("get-news-prompt")]
        public async Task<IActionResult> GetNewsPrompt(int? id){
            try
            {
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    return NotFound();
                }

                var query = await _context.NewsPrompts
                    .Where(w => w.Id == id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return NoContent();
                } else {
                    return Ok(query);
                }
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("search-news-prompt")]
        public async Task<IActionResult> SearchPrompts(string search){
            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    return NotFound();
                }

                var query = await _context.NewsPrompts
                    .Where(w => w.PromptString.Contains($"{search}") || w.Output.Contains($"{search}"))
                    .OrderByDescending(o => o.Id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return NoContent();
                } else {
                    return Ok(query);
                }
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("get-news-prompts")]
        public async Task<IActionResult> GetNewsPrompts(){
            try
            {
                var query = await _context.NewsPrompts
                    .Take(1)
                    .OrderByDescending(o => o.Id)
                    .ToListAsync();
                if (query.Count == 0)
                {
                    return NoContent();
                } else {
                    return Ok(query);
                }
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("DeleteNewsPrompt")]
        public async Task<IActionResult> DeleteNewsPrompt(int id){
            try
            {
                var query = await _context.NewsPrompts.Where(w => w.Id == id).ToListAsync();
                _context.NewsPrompts?.Remove(query[0]);
                await _context.SaveChangesAsync();
                return Ok(new RequestResponse {Message = $"PDF deleted successfully."});
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("generate-news-prompt")]
        public async Task<IActionResult> GeneratePrompt(string title, string keywords, string promptString)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(keywords) || string.IsNullOrWhiteSpace(promptString))
                {
                    return BadRequest("Title, keywords, and promptString are required.");
                }

                string apiKey = _configuration["Gemini:ApiKey"]; 

                string response = await CallGeminiApiAsync(apiKey, promptString);

                var check = await _context.NewsPrompts
                    .Where(w => w.Title == title && w.Keywords == keywords && w.PromptString == promptString)
                    .ToListAsync();
                
                if (check.Count == 0 && !string.IsNullOrEmpty(response)){
                    NewsPrompt newsPrompt = new NewsPrompt
                    {
                        Title = title,
                        Keywords = keywords,
                        PromptString = promptString,
                        Output = response,
                        CreatedAT = DateTime.Now.ToString(),
                    };
                    await _context.NewsPrompts.AddAsync(newsPrompt);
                    await _context.SaveChangesAsync();
                    return Ok(new RequestResponse { Message = "Prompt generated successfully" });
                } else {
                    return Ok(new RequestResponse { Message = "Prompt already exists" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        } 

        private async Task<string> CallGeminiApiAsync(string apiKey, string prompt){
            using var client = new HttpClient();
            string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={apiKey}";
            var payload = new {
                contents = new[]{
                    new {
                        parts = new[]{
                            new { text = prompt }
                        }
                    }
                }
            };

            string jsonPayload = JsonSerializer.Serialize(payload);
            var request = new HttpRequestMessage(HttpMethod.Post, url){
                Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json")
            };

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string responseContent = await response.Content.ReadAsStringAsync();
            var jsonResponse = System.Text.Json.JsonDocument.Parse(responseContent);

            // Navigate to the desired text content
            var text = jsonResponse.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            return text ?? string.Empty; // Return the extracted text
        }
    }
}