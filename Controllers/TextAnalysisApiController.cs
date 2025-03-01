using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using YouTubeWebAPI.Models;
using YouTubeWebAPI.Models.Documents;
using YouTubeWebAPI.Models.Documents.LanguageFeatures;
using YouTubeWebAPI.Models.NLP;
using YouTubeWebAPI.Models.Prompts;
using YouTubeWebAPI.Models.Responses;

namespace YouTubeWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TextAnalysisApiController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;

        public TextAnalysisApiController(AppDbContext context)
        {
            _context = context;
            _httpClient = new HttpClient();
        }
        [HttpGet("DeleteSentimentSearch")]
        public async Task<IActionResult> DeleteSentimentSearch(int id){
            try
            {
                var query = await _context.SentimentSearches.FindAsync(id);
                _context.SentimentSearches.Remove(query);
                await _context.SaveChangesAsync();
                return Ok(new RequestResponse {Message = "Deleted record."});
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("get-top-sentiments-search")]
        public async Task<IActionResult> GetTopSentimentsSearch(){
            try
            {
                var query = await _context.SentimentSearches.OrderByDescending(o => o.Id).Take(2).ToListAsync();
                if (query.Count == 0)
                {
                    return NotFound();
                } else {
                    return Ok(query);
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("get-sentiment-search-by-id")]
        public async Task<IActionResult> GetSentimentSearchById(int id){
            try
            {
                var query = await _context.SentimentSearches.Where(w => w.Id == id).ToListAsync();
                if (query.Count == 0)
                {
                    return NotFound();
                } else {
                    return Ok(query);
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("get-sentiment-search-by-prompt")]
        public async Task<IActionResult> GetSentimentSearchByPrompt(string prompt){
            try
            {
                var query = await _context.SentimentSearches.Where(w => w.PromptString.Contains($"{prompt}")).ToListAsync();
                if (query.Count == 0)
                {
                    return NotFound();
                } else {
                    return Ok(query);
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("generate-prompt-context")]
        public async Task<IActionResult> GeneratePromptContext(string prompt, string file, string state)
        {
            try
            {
                if (string.IsNullOrEmpty(prompt))
                {
                    return NotFound();
                }

                List<OccurenceSearch> occurenceSearches = new List<OccurenceSearch>();
                List<Sentiment> contextSentiments = new List<Sentiment>();

                // Fetch noun chunks related to the prompt
                List<SentMessageChunks> chunks = await _httpClient.GetFromJsonAsync<List<SentMessageChunks>>(
                    $"http://127.0.0.1:5005/nlp/generate_prompt_noun_chunks?prompt={prompt}");

                if (System.IO.File.Exists(file))
                {                    
                    foreach (var item in chunks)
                    {
                        occurenceSearches.AddRange(await _httpClient.GetFromJsonAsync<List<OccurenceSearch>>(
                            $"http://localhost:5082/api/TextAnalysisApi/analyze-text?textFilePath={file}&searchString={item.Text}&contextLength=300"));
                    }
                }
                // Fetch sentiment analysis for each context occurrence
                foreach (var occurence in occurenceSearches)
                {
                    string apiUrl = $"http://127.0.0.1:5005/nlp/querying_sentiments?title=empty&searchString=empty&text={occurence.Context}&state={state}";

                    List<Sentiment> sentiments = await _httpClient.GetFromJsonAsync<List<Sentiment>>(apiUrl);
                    
                    if (sentiments?.Any() == true)
                    {
                        contextSentiments.AddRange(sentiments);
                    }
                }

                if (contextSentiments.Count > 0)
                {                    
                    // Concatenate all sentences from contextSentiments into a single string
                    string concatenatedSentences = string.Join(" ", contextSentiments.Distinct().Select(s => s.Sentence));

                    // Create a single response object
                    SentimentSearch responseObject = new SentimentSearch
                    {
                        PromptString = prompt,
                        FilePath = file,
                        State = state,
                        ConcatenatedSentences = concatenatedSentences,
                        TotalSentences = contextSentiments.Count,
                        CreatedAT = DateTime.Now.ToString()
                    };
                    await _context.SentimentSearches.AddAsync(responseObject);
                    await _context.SaveChangesAsync();
                    return Ok(new RequestResponse {Message = "Search added successfully."});
                } else {
                    return Ok(new RequestResponse {Message = "Couldn't process request."});
                }                
            }
            catch (Exception ex)
            {
                return BadRequest(new RequestResponse { Message = ex.Message });
            }
        }


        [HttpGet("generate-prompt-conversation")]
        public async Task<IActionResult> GeneratePromptConversation(string prompt_string){
            try
            {
                if (string.IsNullOrEmpty(prompt_string))
                {
                    return NotFound();
                }
                Conversation conversation = await _httpClient.GetFromJsonAsync<Conversation>(
                    $"http://127.0.0.1:5005/gen/generate_prompt_conversation?prompt_string={prompt_string}");

                if (conversation == null)
                {
                    return Ok(new Conversation {Response = "Could not process request."});
                } else {
                    TextSearchConversation searchConversation = new TextSearchConversation{
                        PromptString = prompt_string,
                        Message = conversation.Response,
                        CreatedAT = DateTime.Now.ToString()
                    };

                    await _context.TextSearchConversations.AddAsync(searchConversation);
                    await _context.SaveChangesAsync();

                    return Ok(new Conversation {Response = conversation.Response});
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse { Message = ex.Message });
            }
        }

        [HttpGet("get-occurence")]
        public async Task<IActionResult> GetOccurence(int? id){
            try
            {
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    return NotFound();
                }

                var query = await _context.TextSearchMatches
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
        [HttpGet("search-occurences")]
        public async Task<IActionResult> SearchOccurences(string search){
            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    return NotFound();
                }

                var query = await _context.TextSearchMatches
                    .Where(w => w.Title.Contains($"{search}") || w.Context.Contains($"{search}"))
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
        [HttpGet("get-occurences")]
        public async Task<IActionResult> GetOccurences(){
            try
            {
                var query = await _context.TextSearchMatches
                    .Take(10)
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
        [HttpGet("DeleteOccurence")]
        public async Task<IActionResult> DeleteOccurence(int id){
            try
            {
                var query = await _context.TextSearchMatches.Where(w => w.Id == id).ToListAsync();
                _context.TextSearchMatches?.Remove(query[0]);
                await _context.SaveChangesAsync();
                return Ok(new RequestResponse {Message = $"occurence deleted successfully."});
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("post-search-match")]
        public async Task<IActionResult> PostSearchMatch(string searchString, string context, string title, int occurence){
            try
            {
                if (string.IsNullOrEmpty(searchString) || string.IsNullOrEmpty(context) || string.IsNullOrEmpty(title) || occurence <= 0)
                {
                    return BadRequest("Invalid parameters");
                }
                var searchMatch = new TextSearchMatch
                {
                    Title = title,
                    SearchString = searchString,
                    Occurrence = occurence,
                    Context = context
                };

                var check = await _context.TextSearchMatches
                    .Where(w => w.Title.Contains($"{title}") && w.Context.Contains($"{context}"))
                    .ToListAsync();
                if (check.Count > 0)
                {
                    return Ok(new RequestResponse {Message = $"search match already exists."});
                } else {
                    _context.TextSearchMatches?.Add(searchMatch);
                    await _context.SaveChangesAsync();
                    return Ok(new RequestResponse {Message = $"search match added successfully."});
                }
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        private string GetSentenceBoundaryContext(string text, int startIndex, int endIndex, int contextLength)
        {
            int contextStart = Math.Max(0, startIndex - contextLength); 
            int contextEnd = Math.Min(text.Length - 1, endIndex + contextLength);   

            while (contextStart > 0 && !".!?".Contains(text[contextStart - 1]))
            {
                contextStart--;
            }

            while (contextEnd < text.Length - 1 && !".!?".Contains(text[contextEnd]))
            {
                contextEnd++;
            }

            return text.Substring(contextStart, contextEnd - contextStart + 1).Trim();
        }


        [HttpGet("analyze-text")]
        public async Task<IActionResult> AnalyzeText(string textFilePath, string searchString, int contextLength, string sector){            
            var results = new List<OccurenceSearch>();

            try
            {
                if (string.IsNullOrEmpty(textFilePath) || string.IsNullOrEmpty(searchString) || contextLength <= 0 || !System.IO.File.Exists(textFilePath) || string.IsNullOrEmpty(sector))
                {
                    return BadRequest("Invalid parameters");
                }
                var titlesList = await _context.TextDocuments
                    .Where(w => w.OfflineUrl.Contains($"{new FileInfo(textFilePath).Name}"))
                    .ToListAsync();
                
                string title = titlesList[0].Title ?? string.Empty;

                var text = System.IO.File.ReadAllText(textFilePath);
                var matches = Regex.Matches(text, Regex.Escape(searchString));

                int occurrence = 0;
                foreach (Match match in matches)
                {
                    occurrence++;
                    int startIndex = match.Index;
                    int endIndex = match.Index + match.Length - 1;


                    string context = GetSentenceBoundaryContext(text, startIndex, endIndex, contextLength);

                    results.Add(new OccurenceSearch
                    {
                        Title = title,
                        SearchString = searchString,
                        Occurrence = occurrence,
                        Context = context,
                        Sector = sector
                    });
                }

                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("get-language-features")]
        public async Task<IActionResult> GetLanguageFeatures(string title, string searchString){
            try
            {
                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(searchString))
                {
                    return BadRequest("Invalid parameters");
                }
                var nounChunks = await _context.NounChunks
                    .Where(w => w.Title.Contains($"{title}") && w.SearchString.Contains($"{searchString}"))
                    .ToListAsync();
                var sentiments = await _context.Sentiments
                    .Where(w => w.Title.Contains($"{title}") && w.SearchString.Contains($"{searchString}"))
                    .ToListAsync();
                var entities = await _context.Entities
                    .Where(w => w.Title.Contains($"{title}") && w.SearchString.Contains($"{searchString}"))
                    .ToListAsync();
                if (nounChunks.Count == 0 || sentiments.Count == 0 || entities.Count == 0)
                {
                    return NotFound();
                }
                LanguageFeature languageFeature = new LanguageFeature
                {
                    NounChunksList = nounChunks,
                    SentimentsList = sentiments,
                    EntitiesList = entities,
                };
                return Ok(languageFeature);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("get-noun-chunks-from-db")]
        public async Task<IActionResult> GetNounChunksFromDb(string title, string searchString){
            try
            {
                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(searchString))
                {
                    return BadRequest("Invalid parameters");
                }
                var nounChunks = await _context.NounChunks
                    .Where(w => w.Title.Contains($"{title}") && w.SearchString.Contains($"{searchString}"))
                    .ToListAsync();
                if (nounChunks.Count == 0)
                {
                    return NotFound();
                } else {
                }
                return Ok(nounChunks);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("get-entities-from-db")]
        public async Task<IActionResult> GetEntitiesFromDb(string title, string searchString){
            try
            {
                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(searchString))
                {
                    return BadRequest("Invalid parameters");
                }
                var nounChunks = await _context.Entities
                    .Where(w => w.Title.Contains($"{title}") && w.SearchString.Contains($"{searchString}"))
                    .ToListAsync();
                if (nounChunks.Count == 0)
                {
                    return NotFound();
                } else {
                }
                return Ok(nounChunks);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("get-sentiments-from-db")]
        public async Task<IActionResult> GetSentimentsFromDb(string title, string searchString){
            try
            {
                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(searchString))
                {
                    return BadRequest("Invalid parameters");
                }
                var nounChunks = await _context.Sentiments
                    .Where(w => w.Title.Contains($"{title}") && w.SearchString.Contains($"{searchString}"))
                    .ToListAsync();
                if (nounChunks.Count == 0)
                {
                    return NotFound();
                } else {
                }
                return Ok(nounChunks);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        

        [HttpGet("get-noun-chunks")]
        public async Task<IActionResult> GetNounChunks(string title, string searchString, string text){
            try
            {
                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(searchString) || string.IsNullOrEmpty(text))
                {
                    return BadRequest("Invalid parameters");
                }
                List<NounChunk> nounChunks = await _httpClient.GetFromJsonAsync<List<NounChunk>>($"http://127.0.0.1:5005/nlp/generate_noun_chunks?title={title}&searchString={searchString}&text={text}");
                if (nounChunks.Count == 0)
                {
                    return NotFound();
                }
                _context.NounChunks.AddRange(nounChunks);
                await _context.SaveChangesAsync();
                return Ok(nounChunks);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("get-sentiments")]
        public async Task<IActionResult> GetSentiments(string title, string searchString, string text){
            try
            {
                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(searchString) || string.IsNullOrEmpty(text))
                {
                    return BadRequest("Invalid parameters");
                }
                List<Sentiment> sentiments = await _httpClient.GetFromJsonAsync<List<Sentiment>>($"http://127.0.0.1:5005/nlp/generate_sentiments?title={title}&searchString={searchString}&text={text}");
                if (sentiments.Count == 0)
                {
                    return NotFound();
                } else {
                    await _context.Sentiments.AddRangeAsync(sentiments);
                    await _context.SaveChangesAsync();

                    return Ok(sentiments);
                }
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("get-entities")]
        public async Task<IActionResult> GetEntities(string title, string searchString, string text){
            try
            {
                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(searchString) || string.IsNullOrEmpty(text))
                {
                    return BadRequest("Invalid parameters");
                }
                List<Entity> entities = await _httpClient.GetFromJsonAsync<List<Entity>>($"http://127.0.0.1:5005/nlp/generate_entities?title={title}&searchString={searchString}&text={text}");
                if (entities.Count == 0)
                {
                    return NotFound();
                } else {
                    await _context.Entities.AddRangeAsync(entities);
                    await _context.SaveChangesAsync();
                    return Ok(entities);
                }
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("delete-language-features")]
        public async Task<IActionResult> DeleteLanguageFeatures(string title, string searchString, string options){
            try
            {
                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(searchString))
                {
                    return BadRequest("Invalid parameters");
                }
                switch (options)
                {
                    case "NounChunks":
                        var nounChunks = await _context.NounChunks
                            .Where(w => w.Title.Contains($"{title}"))
                            .ToListAsync();
                        if (nounChunks.Count == 0)
                        {
                            return NotFound();
                        }
                        _context.NounChunks.RemoveRange(nounChunks);
                        await _context.SaveChangesAsync();
                        return Ok(new RequestResponse {Message = $"Deleted nounChunks data."});
                    case "Sentiments":
                        var sentiments = await _context.Sentiments
                            .Where(w => w.Title.Contains($"{title}"))
                            .ToListAsync();
                        if (sentiments.Count == 0)
                        {
                            return NotFound();
                        }
                        _context.Sentiments.RemoveRange(sentiments);
                        await _context.SaveChangesAsync();
                        return Ok(new RequestResponse {Message = $"Deleted sentiments data."});
                    case "Entities":
                        var entities = await _context.Entities
                            .Where(w => w.Title.Contains($"{title}"))
                            .ToListAsync();
                        if (entities.Count == 0)
                        {
                            return NotFound();
                        }
                        _context.Entities.RemoveRange(entities);
                        await _context.SaveChangesAsync();
                        return Ok(new RequestResponse {Message = $"Deleted entities data."});
                    default:
                        return BadRequest("");
                }
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}