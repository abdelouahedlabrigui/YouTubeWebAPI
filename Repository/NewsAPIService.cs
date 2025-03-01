using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using YouTubeWebAPI.Models.Search.NewsAPI;

namespace YouTubeWebAPI.Repository
{
    public class NewsAPIService
    {
        private readonly HttpClient _httpClient;
        private readonly string? _baseUrl;
        private readonly string? _apiKey;

        public NewsAPIService(IConfiguration configuration, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["NewsApi:BaseUrl"];
            _apiKey = configuration["NewsApi:ApiKey"];
        }

        public async Task<List<NewsArticle>> GetNewsAsync(string searchQuery){
            try
            {                
                var response = await _httpClient.GetAsync($"{_baseUrl}/everything?q={searchQuery}&apiKey={_apiKey}");
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonSerializer.Deserialize<JsonElement>(jsonString);

                var articleTasks = jsonObject.GetProperty("articles").EnumerateArray()
                    .Select(async article => {
                        var urlToImage = article.GetProperty("urlToImage").GetString();
                        var imageEncoding = await _httpClient.GetFromJsonAsync<ImageEncoding>($"http://127.0.0.1:5007/image/encoding?urlToImage={urlToImage}");

                        return new NewsArticle{
                            Title = article.GetProperty("title").GetString(),
                            Description = article.GetProperty("description").GetString(),
                            Url = article.GetProperty("url").GetString(),
                            UrlToImage = imageEncoding?.UrlToImage,
                            PublishedAt = article.GetProperty("publishedAt").GetString()
                        };
                    });

                var articles = await Task.WhenAll(articleTasks);

                return articles.ToList();
            }
            catch (System.Exception ex)
            {            
                throw new Exception(ex.Message);
            }
        }
    }
}