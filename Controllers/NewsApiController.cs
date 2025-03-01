using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YouTubeWebAPI.Models;
using YouTubeWebAPI.Models.Responses;
using YouTubeWebAPI.Models.Search.NewsAPI;
using YouTubeWebAPI.Repository;

namespace YouTubeWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsApiController : ControllerBase
    {
        
        private readonly HttpClient? _httpClient;
        private readonly AppDbContext _context;

        public NewsApiController(HttpClient httpClient, AppDbContext context)
        {
            _httpClient =  httpClient;
            _context = context;
        }
        [HttpGet("get-news-article")]
        public async Task<IActionResult> GetNewsArticle(int? id){
            try
            {
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    return NotFound();
                }

                var query = await _context.LatestNewsArticles
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
        [HttpGet("search-news-articles")]
        public async Task<IActionResult> SearchNewsArticles(string search, string byDate){
            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    return NotFound();
                }
                switch (byDate)
                {
                    case "true":
                        var queryDate = await _context.LatestNewsArticles
                            .Where(w => w.CreatedAT.Contains($"{search}"))
                            .OrderByDescending(o => o.Id)
                            .ToListAsync();
                        if (queryDate.Count == 0)
                        {
                            return NoContent();
                        } else {
                            return Ok(queryDate);
                        }
                    case "false":
                        var query = await _context.LatestNewsArticles
                            .Where(w => w.Title.Contains($"{search}"))
                            .OrderByDescending(o => o.Id)
                            .ToListAsync();
                        if (query.Count == 0)
                        {
                            return NoContent();
                        } else {
                            return Ok(query);
                        }
                    default:
                        return BadRequest(new RequestResponse { Message = "Cannot process request." });
                }
                
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("get-news-articles")]
        public async Task<IActionResult> GetNewsArticles(){
            try
            {
                var query = await _context.LatestNewsArticles
                    // .Take(10)
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
        [HttpGet("DeleteNewsArticle")]
        public async Task<IActionResult> DeleteNewsArticle(int id){
            try
            {
                var query = await _context.LatestNewsArticles.Where(w => w.Id == id).ToListAsync();
                _context.LatestNewsArticles?.Remove(query[0]);
                await _context.SaveChangesAsync();
                return Ok(new RequestResponse {Message = $"NewsArticle deleted successfully."});
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("post-news-article")]
        public async Task<IActionResult> PostNewsArticle(string title, string description, string url, string urlToImage, string publishedAt)
        {
            try
            {
                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(url) || string.IsNullOrEmpty(urlToImage) || string.IsNullOrEmpty(publishedAt))
                {
                    return BadRequest(new {Message = "All parameters are required."});
                }
                LatestNews newsArticle = new LatestNews
                {
                    Title = title,
                    Description = description,
                    Url = url,
                    UrlToImage = urlToImage,
                    PublishedAt = publishedAt,
                    CreatedAT = DateTime.Now.ToString(),
                };
                await _context.LatestNewsArticles.AddAsync(newsArticle);
                await _context.SaveChangesAsync();

                return Ok(new RequestResponse {Message = "News Article added successfully."});
            }
            catch (System.Exception ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }

        [HttpGet("search")]
        [Authorize]
        public async Task<IActionResult> SearchNews(string query)
        {
            try
            {                
                if (string.IsNullOrEmpty(query))
                {
                    return BadRequest(new {Message = "Query parameter is required."});
                }
                
                var response = await _httpClient.GetAsync($"https://newsapi.org/v2/everything?q={query}&from=2025-02-24&sortBy=publishedAt&apiKey=269f15600588493e9ffaa87c0fa66dca");
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonSerializer.Deserialize<JsonElement>(jsonString);

                var articles = jsonObject.GetProperty("articles").EnumerateArray()
                    .Select(article => new NewsArticle
                    {
                        Title = article.GetProperty("title").GetString(),
                        Description = article.GetProperty("description").GetString(),
                        Url = article.GetProperty("url").GetString(),
                        UrlToImage = article.GetProperty("urlToImage").GetString(),
                        PublishedAt = article.GetProperty("publishedAt").GetString()
                    }).ToList();
                    
                return Ok(articles);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }
    }
}