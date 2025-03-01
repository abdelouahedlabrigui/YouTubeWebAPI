using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using YouTubeWebAPI.Models.Search.GoogleSearch;

namespace YouTubeWebAPI.Repository
{
    public class GoogleSearchService
    {
        private readonly HttpClient _httpClient;
        private readonly string? _apiKey;
        private readonly string? _searchEngineId;

        public GoogleSearchService(IConfiguration configuration, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiKey = configuration["GoogleCustomSearch:ApiKey"];
            _searchEngineId = configuration["GoogleCustomSearch:SearchEngineId"];
        }

        public async Task<List<SearchResult>> SearchAsync(string query){
            var url = $"https://www.googleapis.com/customsearch/v1?q={query}&key={_apiKey}&cx={_searchEngineId}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var searchResponse = JsonSerializer.Deserialize<GoogleSearchResponse>(content, new JsonSerializerOptions{
                    PropertyNameCaseInsensitive = true
                });
                var results = new List<SearchResult>();
                if (searchResponse.Items != null)
                {
                    foreach (var item in searchResponse.Items)
                    {
                        if (item.Link.Contains("https")){
                            results.Add(new SearchResult
                            {
                                Title = item.Title,
                                Link = item.Link,
                                Snippet = item.Snippet
                            });
                        }
                    }
                }
                return results;
            } else {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error: {response.StatusCode} - {error}");
            }
        }
    }
}