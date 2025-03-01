using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using YouTubeWebAPI.Models;
using YouTubeWebAPI.Models.ApiKeys;
using YouTubeWebAPI.Models.Search;
using System.IdentityModel.Tokens.Jwt;
using YouTubeWebAPI.Repository;
using YouTubeWebAPI.Models.Auhentication;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using YouTubeWebAPI.Models.Search.APIResponse;
using Nest;
using YouTubeWebAPI.Models.Responses;

namespace YouTubeWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class YouTubeApiSearchController : ControllerBase
    {
        private readonly YouTubeApiSettings? _settings;
        private readonly AppDbContext _context;
        private readonly IJWTManagerRepository _managerRepository;
        private readonly HttpClient _httpClient;

        public YouTubeApiSearchController(
            IOptions<YouTubeApiSettings> settings, AppDbContext context, IJWTManagerRepository managerRepository)
        {
            _settings = settings.Value;
            _context = context;
            _managerRepository = managerRepository;
            _httpClient = new HttpClient();
        }

        private void SetAuthHeader(string _token){
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        }

        private async Task<Tokens> AccessTokenRequest(string userId, string username, string email, string passwordHash){
            Tokens token = await _httpClient.GetFromJsonAsync<Tokens>($"http://localhost:5082/api/YouTubeApiSearch/authenticate?userId={userId}&userName={username}&email={email}&passwordHash={passwordHash}");
            return token;
        }
        [HttpGet("search-logs")]
        public async Task<IActionResult> GetSearchLogs()
        {
            var searchLogs = await _context.SearchLogs.OrderByDescending(o => o.Id).ToListAsync();
            if (searchLogs == null)
            {
                return BadRequest(new {Message = $"Count rows: {searchLogs?.Count}" });
            }
            return Ok(searchLogs);
        } 

        [HttpGet("authenticate")]
        public IActionResult Authenticate(string userId, string userName, string email, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(userId.ToString()) || string.IsNullOrWhiteSpace(userName.ToString()) || string.IsNullOrEmpty(email.ToString()) || 
                string.IsNullOrEmpty(passwordHash.ToString()))
            {
                return BadRequest(new {Message = "One or many parameters are required"});
            } else {
                try
                {
                    User user = new User {
                        UserId = userId,
                        UserName = userName,
                        Email = email,
                        HashedPassword = passwordHash
                    };

                    var token = _managerRepository.Authenticate(user);
                    if (token == null)
                    {
                        return Unauthorized(new {Message = "Username or password is incorrect"});
                    } else {
                        return Ok(new Tokens {Token = token.Token});
                    }
                }
                catch (System.Exception ex)
                {
                    return StatusCode(500, new {Message = $"Internal server error: {ex.Message}"});
                }
            }
        }
        
        [HttpGet("TestDataAccess")]
        public async Task<IActionResult> TestDataAccess()
        {
            Tokens token = await AccessTokenRequest(
                "19831cdf-3bf7-4d8e-ae5f-f69cbd039e87", 
                "abdelouahedlabrigui@gmail.com", 
                "abdelouahedlabrigui@gmail.com", 
                "AQAAAAIAAYagAAAAEKtCn3i40eSZOFG9clScHPXGSsHPiRDli0Z5QkP0qBXd0uaqmaHgslRRYMnmotAGEg==");
            SetAuthHeader(token.Token);
            List<QueryResult>? searchResult = await _httpClient.GetFromJsonAsync<List<QueryResult>>($"http://localhost:5082/api/YouTubeApiSearch/search?query=OSPF Configuration Lab&maxResults=7");
            if (searchResult == null)
            {
                return BadRequest(new {Message = $"Count rows: {searchResult?.Count}" });
            }
            return Ok(searchResult);
        }

        [HttpGet("GetVideoDetails")]
        //[Authorize]
        public async Task<IActionResult> GetVideoDetails(string videoId){
            if (string.IsNullOrEmpty(videoId)){
                return BadRequest(new {Message = "One or many parameters are required"});
            }
            try
            {
                var youtubeService = new YouTubeService(new BaseClientService.Initializer
                {
                    ApiKey = _settings.ApiKey,
                    ApplicationName = this.GetType().ToString()
                });

                var videoRequest = youtubeService.Videos.List("snippet"); 
                videoRequest.Id = videoId;

                var videoResponse = await videoRequest.ExecuteAsync();

                var video = videoResponse.Items.FirstOrDefault();
                if (video == null)
                {
                    return NotFound(new {Message = "No results found"});
                } else {
                    var searchResult = new QueryResult {
                        VideoId = video.Id,
                        Title = video.Snippet.Title,
                        Description = video.Snippet.Description,
                        PublishedAt = video.Snippet?.PublishedAt.ToString(),
                        ChannelTitle = video.Snippet?.ChannelTitle,
                    };
                    return Ok(searchResult);
                }
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new {Message = $"Internal server error: {ex.Message}"});
            }
        }
        [HttpGet("SearchShorts")]
        [Authorize]
        public async Task<IActionResult> SearchShorts(
            string query,
            int maxResults = 5,
            DateTime? publishedAfter = null,
            DateTime? publishedBefore = null)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest(new { Message = "Query parameter is required." });
            }

            if (maxResults <= 0 || maxResults > 50)
            {
                return BadRequest(new { Message = "MaxResults must be between 1 and 50." });
            }

            try
            {
                // Initialize the YouTube API service
                var youtubeService = new YouTubeService(new BaseClientService.Initializer
                {
                    ApiKey = _settings.ApiKey,
                    ApplicationName = this.GetType().ToString()
                });

                // Create the search request
                var searchRequest = youtubeService.Search.List("snippet");
                searchRequest.Q = query;
                searchRequest.MaxResults = maxResults;
                searchRequest.PublishedAfter = publishedAfter?.ToUniversalTime();
                searchRequest.PublishedBefore = publishedBefore?.ToUniversalTime();
                searchRequest.VideoDuration = SearchResource.ListRequest.VideoDurationEnum.Short__; // Filter for shorts
                searchRequest.Type = "video"; // Only search for videos

                // Execute the request
                var searchResponse = await searchRequest.ExecuteAsync();

                // Map the results
                var results = searchResponse.Items.Select(item => new QueryResult
                {
                    VideoId = item.Id.VideoId,
                    Title = item.Snippet.Title,
                    Description = item.Snippet.Description,
                    PublishedAt = item.Snippet.PublishedAt.ToString() ?? DateTime.MinValue.ToString(),
                    ChannelTitle = item.Snippet.ChannelTitle
                }).ToList();

                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while fetching YouTube shorts.", Details = ex.Message });
            }
        }


        // [HttpGet("SearchVideosByDateIntervals")]
        // [Authorize]
        // public async Task<IActionResult> SearchVideosByDateIntervals(string query, int maxResults, DateTime? publishedAfter = null, DateTime? publishedBefore = null){
        //     try
        //     {
        //         if (string.IsNullOrWhiteSpace(query) || string.IsNullOrEmpty(publishedAfter.ToString()) || string.IsNullOrEmpty(publishedBefore.ToString()) || string.IsNullOrEmpty(maxResults.ToString()))
        //         {
        //             return BadRequest(new {Message = "One or many parameters are required"});
        //         } else {
        //             var baseUrl = "https://www.googleapis.com/youtube/v3/search";
        //             var url = $"{baseUrl}?part=snippet&q={Uri.EscapeDataString(query)}&key={_settings.ApiKey}&type=video&maxResults={maxResults}";
        //             if (publishedAfter.HasValue)
        //             {
        //                 url += $"&publishedAfter={publishedAfter.Value.ToString("yyyy-MM-ddTHH:mm:ssZ")}";
        //             }
        //             if (publishedBefore.HasValue)
        //             {
        //                 url += $"&publishedBefore={publishedBefore.Value.ToString("yyyy-MM-ddTHH:mm:ssZ")}";
        //             }

        //             var response = await _httpClient.GetAsync(url); 
        //             if (!response.IsSuccessStatusCode)
        //             {
        //                 throw new Exception($"Failed to fetch YouTube videos: {response.StatusCode} {await response.Content.ReadAsStringAsync()}");
        //             }
        //             var content = await response.Content.ReadFromJsonAsync<YouTubeApiResponse>();
        //             var videos = content?.Items?.Select(item => new QueryResult {
        //                 VideoId = item.Id?.VideoId,
        //                 Title = item.Snippet?.Title,
        //                 Description = item.Snippet?.Description,
        //                 PublishedAt = item.Snippet?.PublishedAt,
        //                 ChannelTitle = item.Snippet?.ChannelTitle,
        //             }).ToList();
                    
        //             var searchResults = videos;
        //             if (searchResults?.Count == 0)
        //             {
        //                 return NotFound(new {Message = "No results found"});
        //             } else {
        //                 return Ok(searchResults);
        //             }                
        //         }
        //     }
        //     catch (System.Exception ex)
        //     {
        //         return BadRequest(new {Message = $"Internal server error: {ex.Message}"});
        //     }
        // }
        [HttpGet("search-saved-yt-videos")]
        public async Task<IActionResult> SearchSavedYouTubeVideos(string search){
            try
            {
                if (string.IsNullOrEmpty(search)){
                    return NotFound(new RequestResponse { Message = $"Param state {search}." });
                }

                var searchRequest = await _context.QueryResults
                    .Where(w => w.Title.Contains(search) || w.Description.Contains(search) || w.ChannelTitle.Contains(search))
                    .ToListAsync();
                if (searchRequest.Count > 0)
                {
                    return Ok(searchRequest);
                } else {
                    return Ok(new List<QueryResult>());
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Message = $"Internal server error: {ex.Message}" });
            }
        }
        [HttpGet("post-yt-video-search")]
        public async Task<IActionResult> PostYouTubeVideoSearch(string videoId, string title, string description, string publishedAt, string channelTitle, string thumbnailUrl, TimeSpan duration){
            try
            {
                if (string.IsNullOrEmpty(videoId) || string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(publishedAt.ToString()) || string.IsNullOrEmpty(channelTitle) || string.IsNullOrEmpty(thumbnailUrl) || string.IsNullOrEmpty(duration.ToString()))
                {
                    return NotFound(new RequestResponse {Message = "Parameter(s) empty."});
                }

                QueryResult queryResult = new QueryResult{
                    VideoId = videoId,
                    Title = title,
                    Description = description,
                    PublishedAt = publishedAt,
                    ChannelTitle = channelTitle,
                    ThumbnailUrl = thumbnailUrl,
                    Duration = duration,
                };
                await _context.QueryResults.AddAsync(queryResult);
                await _context.SaveChangesAsync();
                return Ok(new RequestResponse {Message = "Added YT video."});
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Message = $"Internal server error: {ex.Message}" });
            }
        }
        [HttpGet("SearchLongVideosByDateIntervals")]
        // [Authorize]
        public async Task<IActionResult> SearchLongVideosByDateIntervals(string query, int maxResults, DateTime? publishedAfter = null, DateTime? publishedBefore = null, int minDurationSeconds = 600) // minDurationSeconds = 10 minutes default
        {
            try
            {
                if (string.IsNullOrWhiteSpace(query) || string.IsNullOrEmpty(publishedAfter.ToString()) || string.IsNullOrEmpty(publishedBefore.ToString()) || string.IsNullOrEmpty(maxResults.ToString()))
                {
                    return BadRequest(new { Message = "One or many parameters are required" });
                }
                else
                {
                    var baseUrl = "https://www.googleapis.com/youtube/v3/search";
                    var url = $"{baseUrl}?part=snippet&q={Uri.EscapeDataString(query)}&key={_settings.ApiKey}&type=video&maxResults={maxResults}";
                    if (publishedAfter.HasValue)
                    {
                        url += $"&publishedAfter={publishedAfter.Value.ToString("yyyy-MM-ddTHH:mm:ssZ")}";
                    }
                    if (publishedBefore.HasValue)
                    {
                        url += $"&publishedBefore={publishedBefore.Value.ToString("yyyy-MM-ddTHH:mm:ssZ")}";
                    }

                    var response = await _httpClient.GetAsync(url);
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"Failed to fetch YouTube videos: {response.StatusCode} {await response.Content.ReadAsStringAsync()}");
                    }
                    var content = await response.Content.ReadFromJsonAsync<YouTubeApiResponse>();
                    var videos = content?.Items?.ToList();

                    if (videos == null || videos.Count == 0)
                    {
                        return NotFound(new { Message = "No results found" });
                    }

                    var videoIds = videos.Select(v => v.Id?.VideoId).Where(id => !string.IsNullOrEmpty(id)).ToList();

                    if (videoIds.Count == 0)
                    {
                        return NotFound(new { Message = "No results found" });
                    }

                    var durationUrl = $"https://www.googleapis.com/youtube/v3/videos?part=contentDetails&id={string.Join(",", videoIds)}&key={_settings.ApiKey}";
                    var durationResponse = await _httpClient.GetAsync(durationUrl);

                    if (!durationResponse.IsSuccessStatusCode)
                    {
                        throw new Exception($"Failed to fetch video durations: {durationResponse.StatusCode} {await durationResponse.Content.ReadAsStringAsync()}");
                    }

                    var durationContent = await durationResponse.Content.ReadFromJsonAsync<YouTubeVideoDetailsResponse>();

                    var searchResults = videos.Select(item =>
                    {
                        var durationItem = durationContent?.Items?.FirstOrDefault(d => d.Id == item.Id?.VideoId);
                        TimeSpan duration = TimeSpan.Zero;
                        if (durationItem != null && !string.IsNullOrEmpty(durationItem.ContentDetails?.Duration))
                        {
                            duration = ParseDuration(durationItem.ContentDetails.Duration);
                        }

                        return new QueryResult
                        {
                            VideoId = item.Id?.VideoId,
                            Title = item.Snippet?.Title,
                            Description = item.Snippet?.Description,
                            PublishedAt = item.Snippet?.PublishedAt.ToString(),
                            ChannelTitle = item.Snippet?.ChannelTitle,
                            ThumbnailUrl = item.Snippet?.Thumbnails?.High?.Url ?? item.Snippet?.Thumbnails?.Medium?.Url ?? item.Snippet?.Thumbnails?.Default?.Url,
                            Duration = duration
                        };
                    }).Where(r => r.Duration.TotalSeconds >= minDurationSeconds).ToList(); // Filter by duration

                    if (searchResults.Count == 0)
                    {
                        return NotFound(new { Message = "No results found with specified duration" });
                    }

                    return Ok(searchResults);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Internal server error: {ex.Message}" });
            }
        }
        [HttpGet("search")]
        [Authorize]
        public async Task<IActionResult> Search(string query, int maxResults)
        {
            if (string.IsNullOrWhiteSpace(query) || string.IsNullOrEmpty(maxResults.ToString()))
            {
                return BadRequest(new {Message = "One or many parameters are required"});
            } else {
                try
                {
                    SearchLog searchLog = new SearchLog {
                        Search = query,
                        MaxResults = maxResults,
                        SearchDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    };

                    _context.SearchLogs?.Add(searchLog);
                    await _context.SaveChangesAsync();

                    // return token;
                    var youtubeService = new YouTubeService(new BaseClientService.Initializer(){
                        ApiKey = _settings.ApiKey,
                        ApplicationName = this.GetType().ToString() 
                    });

                    var searchRequest = youtubeService.Search.List("snippet");
                    searchRequest.Q = query;
                    searchRequest.MaxResults = maxResults;

                    var searchResponse = await searchRequest.ExecuteAsync();
                    var results = searchResponse.Items.Select(item => new QueryResult {
                        VideoId = item.Id.VideoId,
                        Title = item.Snippet.Title,
                        Description = item.Snippet.Description,
                        PublishedAt = item.Snippet.PublishedAtDateTimeOffset.ToString(),
                        ChannelTitle = item.Snippet?.ChannelTitle
                    }).ToList();

                    if (results.Count > 0)
                    {
                        return Ok(results);
                    } else {
                        return NotFound(new {Message = "No results found"});
                    }                                     
                }
                catch (System.Exception ex)
                {
                    return StatusCode(500, new {Message = $"Internal server error: {ex.Message}"});
                }
            }
        }
        private TimeSpan ParseDuration(string duration)
        {
            // Parse ISO 8601 duration
            return System.Xml.XmlConvert.ToTimeSpan(duration);
        }
    }
    public class YouTubeVideoDetailsResponse
    {
        public List<YouTubeVideoDetailsItem>? Items { get; set; }
    }
    public class YouTubeVideoDetailsItem
    {
        public string? Id { get; set; }
        public YouTubeVideoContentDetails? ContentDetails { get; set; }
    }

    public class YouTubeVideoContentDetails
    {
        public string? Duration { get; set; }
    }
}