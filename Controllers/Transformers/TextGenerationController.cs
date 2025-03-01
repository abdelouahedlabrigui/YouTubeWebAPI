using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YouTubeWebAPI.Models;
using YouTubeWebAPI.Models.Prompts.Transformers;
using YouTubeWebAPI.Models.Responses;
using YouTubeWebAPI.Repository;
using YouTubeWebAPI.Models.Documents.LanguageFeatures;
using YouTubeWebAPI.Models.Documents.LanguageFeatures.Multilingual;
using YouTubeWebAPI.Models.NLP.Llms;
using YouTubeWebAPI.Models.NLP.RecursiveSearch.ContextSearch;

namespace YouTubeWebAPI.Controllers.Transformers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TextGenerationController : ControllerBase
    {
        private readonly TextGenerationService _textGenerationService;
        private readonly AppDbContext _context;
        private readonly HttpClient? _httpClient;

        public TextGenerationController(TextGenerationService textGenerationService, AppDbContext context)
        {
            _textGenerationService = textGenerationService;
            _context = context;
            _httpClient = new HttpClient();
        }
        [HttpGet("get-generated-texts")]
        public async Task<IActionResult> GetGeneratedTexts(){
            try
            {
                var query = await _context.GeneratedTexts.Take(4).OrderByDescending(o => o.Id).ToListAsync();
                if (query.Count == 0)
                {
                    return NotFound(new RequestResponse {Message = $"Query count : {query.Count}, at {DateTime.Now}" });
                } else {
                    return Ok(query);
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = $"Error: {ex.Message}"});
            }
        }
        [HttpGet("get-generated-text")]
        public async Task<IActionResult> GetGeneratedText(int id){
            try
            {
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    return NotFound(new RequestResponse {Message = $"Id is null: {id}"});
                }
                var query = await _context.GeneratedTexts.Where(o => o.Id == id).ToListAsync();                            

                if (query.Count == 0)
                {
                    return NotFound(new RequestResponse {Message = $"Query count : {query.Count}, at {DateTime.Now}" });
                } else {
                    
                    string english = query[0].Text ?? "";
                    string spanish = query[0].EsTranslation ?? "";

                    List<MultilingualEntity> entities = await _httpClient.GetFromJsonAsync<List<MultilingualEntity>>(
                        $"http://127.0.0.1:5005/nlp/generate_raw_entities?english_text={english ?? "empty"}&spanish_text={spanish ?? "empty"}");

                    List<MultilingualNounChunk> chunks = await _httpClient.GetFromJsonAsync<List<MultilingualNounChunk>>(
                        $"http://127.0.0.1:5005/nlp/generate_raw_noun_chunks?english_text={english ?? "empty"}&spanish_text={spanish ?? "empty"}");

                    List<MultilingualSentiment> sentiments = await _httpClient.GetFromJsonAsync<List<MultilingualSentiment>>(
                        $"http://127.0.0.1:5005/nlp/generate_raw_sentiments?english_text={english ?? "empty"}&spanish_text={spanish ?? "empty"}");

                    NlpMultilingualText multilingualText = new NlpMultilingualText {
                        EnglishText = english,
                        SpanishText = spanish,
                        Entities = entities,
                        Sentiments = sentiments,
                        NounChunks = chunks,
                    };
                    return Ok(multilingualText);
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = $"Error: {ex.Message}"});
            }
        }
        [HttpGet("search-generated-text")]
        public async Task<IActionResult> SearchGeneratedTexts(string? search){
            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    return NotFound(new RequestResponse {Message = $"Search value: {search}"});
                }
                var query = await _context.GeneratedTexts.Where(o => o.Text.Contains($"{search}")).ToListAsync();
                List<NlpMultilingualText>? generatedText = new List<NlpMultilingualText>();
                if (query.Count == 0)
                {
                    return NotFound(new RequestResponse {Message = $"Query count : {query.Count}, at {DateTime.Now}" });
                } else {
                    foreach (var item in query)
                    {
                        string english = item.Text ?? "";
                        string spanish = item.EsTranslation ?? "";

                        List<MultilingualEntity> entities = await _httpClient.GetFromJsonAsync<List<MultilingualEntity>>(
                            $"http://127.0.0.1:5005/nlp/generate_raw_entities?english_text={english ?? "empty"}&spanish_text={spanish ?? "empty"}");

                        List<MultilingualNounChunk> chunks = await _httpClient.GetFromJsonAsync<List<MultilingualNounChunk>>(
                            $"http://127.0.0.1:5005/nlp/generate_raw_noun_chunks?english_text={english ?? "empty"}&spanish_text={spanish ?? "empty"}");

                        List<MultilingualSentiment> sentiments = await _httpClient.GetFromJsonAsync<List<MultilingualSentiment>>(
                            $"http://127.0.0.1:5005/nlp/generate_raw_sentiments?english_text={english ?? "empty"}&spanish_text={spanish ?? "empty"}");

                        NlpMultilingualText multilingualText = new NlpMultilingualText {
                            EnglishText = english,
                            SpanishText = spanish,
                            Entities = entities,
                            Sentiments = sentiments,
                            NounChunks = chunks,
                        };
                        generatedText.Add(multilingualText);
                    }
                    if (generatedText.Count == 0)
                    {
                        return NotFound(new RequestResponse {Message = "Couldn't process request."});
                    } else {
                        return Ok(generatedText);
                    }
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = $"Error: {ex.Message}"});
            }
        }
        [HttpGet("delete-generated-text")]
        public async Task<IActionResult> DeleteGeneratedText(int id){
            try
            {
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    return NotFound(new RequestResponse {Message = $"Id is null: {id}"});
                }
                var query = await _context.GeneratedTexts.FindAsync(id);
                _context.GeneratedTexts.Remove(query ?? new GeneratedText());
                await _context.SaveChangesAsync();
                
                return Ok(new RequestResponse {Message = $"Deleted record ta: {DateTime.Now}"});
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = $"Error: {ex.Message}"});
            }
        }
        [HttpGet("generate-text-filtered-entities")]
        public async Task<IActionResult> GetTextFilteredEntities(string text){
            try
            {
                if(string.IsNullOrEmpty(text))
                {
                    return NotFound($"Some values are empty.");
                }

                List<Entity> generatedSentiments = await _httpClient.GetFromJsonAsync<List<Entity>>($"http://127.0.0.1:5005/nlp/generate_prompt_entities_by_filter?prompt={text}");
                if(generatedSentiments?.Count > 0){
                    return Ok(generatedSentiments);
                } else {
                    return BadRequest(new RequestResponse {Message = $"Count rows: {generatedSentiments?.Count}"});
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = $"Error: {ex.Message}"});
            }
        }
        [HttpGet("generate-text-sentiments")]
        public async Task<IActionResult> GetTextSentiments(string text){
            try
            {
                if(string.IsNullOrEmpty(text))
                {
                    return NotFound($"Some values are empty.");
                }

                List<Sentiment> generatedSentiments = await _httpClient.GetFromJsonAsync<List<Sentiment>>($"http://127.0.0.1:5005/nlp/generate_prompt_sentiments?prompt={text}");
                if(generatedSentiments?.Count > 0){
                    return Ok(generatedSentiments);
                } else {
                    return BadRequest(new RequestResponse {Message = $"Count rows: {generatedSentiments?.Count}"});
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = $"Error: {ex.Message}"});
            }
        }
        [HttpGet("get-llm-text-generations")]
        public async Task<IActionResult> GetLlmTextGenerations(){
            try
            {
                var query = await _context.LlmTextGenerations.Take(4).OrderByDescending(o => o.Id).ToListAsync();
                if (query.Count == 0)
                {
                    return NotFound(new RequestResponse {Message = $"Query count : {query.Count}, at {DateTime.Now}" });
                } else {
                    return Ok(query);
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = $"Error: {ex.Message}"});
            }
        }
	    [HttpGet("search-llm-text-generations")]
        public async Task<IActionResult> SearchLlmTextGenerations(string search){
            try
            {
		if(string.IsNullOrEmpty(search))
                {
                    return NotFound($"Some values are empty.");
                }
                var query = await _context.LlmTextGenerations.Where(w => w.GeneratedText.Contains($"{search}")).OrderByDescending(o => o.Id).ToListAsync();
                if (query.Count == 0)
                {
                    return NotFound(new RequestResponse {Message = $"Query count : {query.Count}, at {DateTime.Now}" });
                } else {
                    return Ok(query);
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = $"Error: {ex.Message}"});
            }
        }
        
        [HttpGet("generate-information-extraction-decision")]
        public async Task<IActionResult> GenerateInformationExtractionDecision(string sentence){
            try
            {
                if(string.IsNullOrEmpty(sentence)){
                    return NotFound(new RequestResponse {Message = "Sentence is empty or null."});
                } else {

                    List<Entity>? entities = await _httpClient.GetFromJsonAsync<List<Entity>>(
                        $"http://127.0.0.1:5005/nlp/generate_entities_tags?sentence={sentence}");

                    List<Sentiment>? sentimentsList = await _httpClient.GetFromJsonAsync<List<Sentiment>>(
                        $"http://127.0.0.1:5005/nlp/generate_sentiments_score?sentence={sentence}");

                    if (entities?.Count == 0 || sentimentsList?.Count == 0)  
                    {
                        return NotFound(
                            $"Queries Counts: entities: {entities?.Count} | sentiments: {sentimentsList?.Count}");
                    } else {

                        // Generate questions based on named entities
                        foreach (var entity in entities)
                        {
                            if (entity.Label == "EVENT")
                            {                                
                                Answering answer = await _httpClient.GetFromJsonAsync<Answering>($"http://127.0.0.1:5005/nlp/generate_question_answering?context=What does the event: '{entity.Text}' refer to split_text {sentence}");
                                if (answer != null){  
                                    List<Sentiment>? sentiments = await _httpClient.GetFromJsonAsync<List<Sentiment>>(
                                        $"http://127.0.0.1:5005/nlp/generate_sentiments_score?sentence={sentence}"); 
                                    QuestionAnswering answering = new QuestionAnswering{
                                        Sentence = sentence,
                                        Question = $"What does the event '{entity.Text}' refer to?",
                                        Type = "Generate questions based on EVENT entity",
                                        Answer = $"{answer.Answer}",
                                        End = answer.End?.ToString(),
                                        Score = answer.Score?.ToString(),
                                        Start = answer.Start?.ToString(),
                                        Match = $"{entity.Text}",
                                        Positive = sentiments?[0].Positive?.ToString() ?? "0.0",
                                        Negative = sentiments?[0].Negative?.ToString() ?? "0.0",
                                        Neutral = sentiments?[0].Neutral?.ToString() ?? "0.0",
                                        Compound = sentiments?[0].Compound?.ToString() ?? "0.0",
                                        CreatedAT = DateTime.UtcNow.ToString("yyyy-MM-dd")
                                    };
                                    await _context.QuestionAnswerings.AddAsync(answering);
                                    await _context.SaveChangesAsync();
                                }
                            } else if (entity.Label == "GPE")
                            {                                
                                Answering answer = await _httpClient.GetFromJsonAsync<Answering>($"http://127.0.0.1:5005/nlp/generate_question_answering?context=What does the Geo-Political Entity: '{entity.Text}' refer to split_text {sentence}");
                                if (answer != null){  
                                    List<Sentiment>? sentiments = await _httpClient.GetFromJsonAsync<List<Sentiment>>(
                                        $"http://127.0.0.1:5005/nlp/generate_sentiments_score?sentence={sentence}"); 
                                    QuestionAnswering answering = new QuestionAnswering{
                                        Sentence = sentence,
                                        Question = $"What does the Geo-Political Entity '{entity.Text}' refer to?",
                                        Type = "Generate questions based on GPE entity",
                                        Answer = $"{answer.Answer}",
                                        End = answer.End?.ToString(),
                                        Score = answer.Score?.ToString(),
                                        Start = answer.Start?.ToString(),
                                        Match = $"{entity.Text}",
                                        Positive = sentiments?[0].Positive?.ToString() ?? "0.0",
                                        Negative = sentiments?[0].Negative?.ToString() ?? "0.0",
                                        Neutral = sentiments?[0].Neutral?.ToString() ?? "0.0",
                                        Compound = sentiments?[0].Compound?.ToString() ?? "0.0",
                                        CreatedAT = DateTime.UtcNow.ToString("yyyy-MM-dd")
                                    };
                                    await _context.QuestionAnswerings.AddAsync(answering);
                                    await _context.SaveChangesAsync();
                                }
                            } else if (entity.Label == "PERSON")
                            {                                
                                Answering answer = await _httpClient.GetFromJsonAsync<Answering>($"http://127.0.0.1:5005/nlp/generate_question_answering?context=What does the People's names Entity: '{entity.Text}' refer to split_text {sentence}");
                                if (answer != null){  
                                    List<Sentiment>? sentiments = await _httpClient.GetFromJsonAsync<List<Sentiment>>(
                                        $"http://127.0.0.1:5005/nlp/generate_sentiments_score?sentence={sentence}"); 
                                    QuestionAnswering answering = new QuestionAnswering{
                                        Sentence = sentence,
                                        Question = $"What does the People's names Entity '{entity.Text}' refer to?",
                                        Type = "Generate questions based on PERSON entity",
                                        Answer = $"{answer.Answer}",
                                        End = answer.End?.ToString(),
                                        Score = answer.Score?.ToString(),
                                        Start = answer.Start?.ToString(),
                                        Match = $"{entity.Text}",
                                        Positive = sentiments?[0].Positive?.ToString() ?? "0.0",
                                        Negative = sentiments?[0].Negative?.ToString() ?? "0.0",
                                        Neutral = sentiments?[0].Neutral?.ToString() ?? "0.0",
                                        Compound = sentiments?[0].Compound?.ToString() ?? "0.0",
                                        CreatedAT = DateTime.UtcNow.ToString("yyyy-MM-dd")
                                    };
                                    await _context.QuestionAnswerings.AddAsync(answering);
                                    await _context.SaveChangesAsync();
                                }
                            } else if (entity.Label == "NORP")
                            {                                
                                Answering answer = await _httpClient.GetFromJsonAsync<Answering>($"http://127.0.0.1:5005/nlp/generate_question_answering?context=What does the Nationalities or religious or political groups Entity: '{entity.Text}' refer to split_text {sentence}");
                                if (answer != null){  
                                    List<Sentiment>? sentiments = await _httpClient.GetFromJsonAsync<List<Sentiment>>(
                                        $"http://127.0.0.1:5005/nlp/generate_sentiments_score?sentence={sentence}"); 
                                    QuestionAnswering answering = new QuestionAnswering{
                                        Sentence = sentence,
                                        Question = $"What does the Nationalities or religious or political groups Entity '{entity.Text}' refer to?",
                                        Type = "Generate questions based on NORP entity",
                                        Answer = $"{answer.Answer}",
                                        End = answer.End?.ToString(),
                                        Score = answer.Score?.ToString(),
                                        Start = answer.Start?.ToString(),
                                        Match = $"{entity.Text}",
                                        Positive = sentiments?[0].Positive?.ToString() ?? "0.0",
                                        Negative = sentiments?[0].Negative?.ToString() ?? "0.0",
                                        Neutral = sentiments?[0].Neutral?.ToString() ?? "0.0",
                                        Compound = sentiments?[0].Compound?.ToString() ?? "0.0",
                                        CreatedAT = DateTime.UtcNow.ToString("yyyy-MM-dd")
                                    };
                                    await _context.QuestionAnswerings.AddAsync(answering);
                                    await _context.SaveChangesAsync();
                                }
                            } else if (entity.Label == "LOC")
                            {                                
                                Answering answer = await _httpClient.GetFromJsonAsync<Answering>($"http://127.0.0.1:5005/nlp/generate_question_answering?context=What does the Locations (more general than GPE; can include natural places) Entity: '{entity.Text}' refer to split_text {sentence}");
                                if (answer != null){  
                                    List<Sentiment>? sentiments = await _httpClient.GetFromJsonAsync<List<Sentiment>>(
                                        $"http://127.0.0.1:5005/nlp/generate_sentiments_score?sentence={sentence}"); 
                                    QuestionAnswering answering = new QuestionAnswering{
                                        Sentence = sentence,
                                        Question = $"What does the Locations (more general than GPE; can include natural places) Entity '{entity.Text}' refer to?",
                                        Type = "Generate questions based on LOC entity",
                                        Answer = $"{answer.Answer}",
                                        End = answer.End?.ToString(),
                                        Score = answer.Score?.ToString(),
                                        Start = answer.Start?.ToString(),
                                        Match = $"{entity.Text}",
                                        Positive = sentiments?[0].Positive?.ToString() ?? "0.0",
                                        Negative = sentiments?[0].Negative?.ToString() ?? "0.0",
                                        Neutral = sentiments?[0].Neutral?.ToString() ?? "0.0",
                                        Compound = sentiments?[0].Compound?.ToString() ?? "0.0",
                                        CreatedAT = DateTime.UtcNow.ToString("yyyy-MM-dd")
                                    };
                                    await _context.QuestionAnswerings.AddAsync(answering);
                                    await _context.SaveChangesAsync();
                                }
                            }
                            else if (entity.Label == "LAW")
                            {                                
                                Answering answer = await _httpClient.GetFromJsonAsync<Answering>($"http://127.0.0.1:5005/nlp/generate_question_answering?context=What Law, act, or statut Entity: '{entity.Text}' refer to split_text {sentence}");
                                if (answer != null){  
                                    List<Sentiment>? sentiments = await _httpClient.GetFromJsonAsync<List<Sentiment>>(
                                        $"http://127.0.0.1:5005/nlp/generate_sentiments_score?sentence={sentence}"); 
                                    QuestionAnswering answering = new QuestionAnswering{
                                        Sentence = sentence,
                                        Question = $"What Law, act, or statut Entity '{entity.Text}' refer to?",
                                        Type = "Generate questions based on LAW entity",
                                        Answer = $"{answer.Answer}",
                                        End = answer.End?.ToString(),
                                        Score = answer.Score?.ToString(),
                                        Start = answer.Start?.ToString(),
                                        Match = $"{entity.Text}",
                                        Positive = sentiments?[0].Positive?.ToString() ?? "0.0",
                                        Negative = sentiments?[0].Negative?.ToString() ?? "0.0",
                                        Neutral = sentiments?[0].Neutral?.ToString() ?? "0.0",
                                        Compound = sentiments?[0].Compound?.ToString() ?? "0.0",
                                        CreatedAT = DateTime.UtcNow.ToString("yyyy-MM-dd")
                                    };
                                    await _context.QuestionAnswerings.AddAsync(answering);
                                    await _context.SaveChangesAsync();
                                }
                            } else if (entity.Label == "ORG")
                            {                                
                                Answering answer = await _httpClient.GetFromJsonAsync<Answering>($"http://127.0.0.1:5005/nlp/generate_question_answering?context=What does the Geo-Political Entity: '{entity.Text}' refer to split_text {sentence}");
                                if (answer != null){  
                                    List<Sentiment>? sentiments = await _httpClient.GetFromJsonAsync<List<Sentiment>>(
                                        $"http://127.0.0.1:5005/nlp/generate_sentiments_score?sentence={sentence}"); 
                                    QuestionAnswering answering = new QuestionAnswering{
                                        Sentence = sentence,
                                        Question = $"What Organization Entity '{entity.Text}' refer to?",
                                        Type = "Generate questions based on ORG entity",
                                        Answer = $"{answer.Answer}",
                                        End = answer.End?.ToString(),
                                        Score = answer.Score?.ToString(),
                                        Start = answer.Start?.ToString(),
                                        Match = $"{entity.Text}",
                                        Positive = sentiments?[0].Positive?.ToString() ?? "0.0",
                                        Negative = sentiments?[0].Negative?.ToString() ?? "0.0",
                                        Neutral = sentiments?[0].Neutral?.ToString() ?? "0.0",
                                        Compound = sentiments?[0].Compound?.ToString() ?? "0.0",
                                        CreatedAT = DateTime.UtcNow.ToString("yyyy-MM-dd")
                                    };
                                    await _context.QuestionAnswerings.AddAsync(answering);
                                    await _context.SaveChangesAsync();
                                }
                            } else if (entity.Label == "DATE")
                            {                                
                                Answering answer = await _httpClient.GetFromJsonAsync<Answering>($"http://127.0.0.1:5005/nlp/generate_question_answering?context=What does the Dates, times, and periods Entity: '{entity.Text}' refer to split_text {sentence}");
                                if (answer != null){  
                                    List<Sentiment>? sentiments = await _httpClient.GetFromJsonAsync<List<Sentiment>>(
                                        $"http://127.0.0.1:5005/nlp/generate_sentiments_score?sentence={sentence}"); 
                                    QuestionAnswering answering = new QuestionAnswering{
                                        Sentence = sentence,
                                        Question = $"What Dates, times, and periods Entity '{entity.Text}' refer to?",
                                        Type = "Generate questions based on ORG entity",
                                        Answer = $"{answer.Answer}",
                                        End = answer.End?.ToString(),
                                        Score = answer.Score?.ToString(),
                                        Start = answer.Start?.ToString(),
                                        Match = $"{entity.Text}",
                                        Positive = sentiments?[0].Positive?.ToString() ?? "0.0",
                                        Negative = sentiments?[0].Negative?.ToString() ?? "0.0",
                                        Neutral = sentiments?[0].Neutral?.ToString() ?? "0.0",
                                        Compound = sentiments?[0].Compound?.ToString() ?? "0.0",
                                        CreatedAT = DateTime.UtcNow.ToString("yyyy-MM-dd")
                                    };
                                    await _context.QuestionAnswerings.AddAsync(answering);
                                    await _context.SaveChangesAsync();
                                }
                            }
                        }

                        foreach(var sentiment in sentimentsList){
                            // Generate questions based on sentiment
                            if (!string.IsNullOrEmpty(sentiment.Positive) && float.Parse(sentiment.Positive) >= float.Parse(sentiment.Negative))
                            {
                                Answering answer = await _httpClient.GetFromJsonAsync<Answering>($"http://127.0.0.1:5005/nlp/generate_question_answering?context=What makes this sentence positive split_text {sentence}");
                                if (answer != null){  
                                    List<Sentiment>? sentiments = await _httpClient.GetFromJsonAsync<List<Sentiment>>(
                                        $"http://127.0.0.1:5005/nlp/generate_sentiments_score?sentence={sentence}"); 
                                    
                                    QuestionAnswering answering = new QuestionAnswering{
                                        Sentence = sentence,
                                        Question = $"What makes this sentence positive?",
                                        Type = "Generate questions based on positive sentiment",
                                        Answer = $"{answer.Answer}",
                                        End = answer.End?.ToString(),
                                        Score = answer.Score?.ToString(),
                                        Start = answer.Start?.ToString(),
                                        Match = $"sentiment",
                                        Positive = sentiments?[0].Positive?.ToString() ?? "0.0",
                                        Negative = sentiments?[0].Negative?.ToString() ?? "0.0",
                                        Neutral = sentiments?[0].Neutral?.ToString() ?? "0.0",
                                        Compound = sentiments?[0].Compound?.ToString() ?? "0.0",
                                        CreatedAT = DateTime.UtcNow.ToString("yyyy-MM-dd")
                                    };
                                    await _context.QuestionAnswerings.AddAsync(answering);
                                    await _context.SaveChangesAsync();
                                }
                            }
                            if (!string.IsNullOrEmpty(sentiment.Negative) && float.Parse(sentiment.Positive) <= float.Parse(sentiment.Negative))
                            {
                                Answering answer = await _httpClient.GetFromJsonAsync<Answering>($"http://127.0.0.1:5005/nlp/generate_question_answering?context=What makes this sentence negative split_text {sentence}");
                                    if (answer != null){  
                                        List<Sentiment>? sentiments = await _httpClient.GetFromJsonAsync<List<Sentiment>>(
                                            $"http://127.0.0.1:5005/nlp/generate_sentiments_score?sentence={sentence}"); 
                                        
                                        QuestionAnswering answering = new QuestionAnswering{
                                            Sentence = sentence,
                                            Question = $"What makes this sentence negative?",
                                            Type = "Generate questions based on negative sentiment",
                                            Answer = $"{answer.Answer}",
                                            End = answer.End?.ToString(),
                                            Score = answer.Score?.ToString(),
                                            Start = answer.Start?.ToString(),
                                            Match = $"sentiment",
                                            Positive = sentiments?[0].Positive?.ToString() ?? "0.0",
                                            Negative = sentiments?[0].Negative?.ToString() ?? "0.0",
                                            Neutral = sentiments?[0].Neutral?.ToString() ?? "0.0",
                                            Compound = sentiments?[0].Compound?.ToString() ?? "0.0",
                                            CreatedAT = DateTime.UtcNow.ToString("yyyy-MM-dd")
                                        };
                                        await _context.QuestionAnswerings.AddAsync(answering);
                                        await _context.SaveChangesAsync();
                                    }
                            }
                        }
                        var query = await _context.QuestionAnswerings.ToListAsync();
                        return Ok(new RequestResponse {
                            Message = $"Ended process at Min: {DateTime.Now.Minute.ToString()}, Sec: {DateTime.Now.Second.ToString()}. Count rows: {query.Count}."});
                    }
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = $"Error: {ex.Message}"});
            }
        }
        [HttpGet("generate-text-without-translation")]
        public async Task<IActionResult> GetTextGenerationWithoutTranslation(string text, string actor, string response){
            try
            {
                if(string.IsNullOrEmpty(text) || string.IsNullOrEmpty(actor) || string.IsNullOrEmpty(response))
                {
                    return NotFound($"Some values are empty.");
                }

                List<GeneratedText> generatedTexts = await _httpClient.GetFromJsonAsync<List<GeneratedText>>($"http://127.0.0.1:5005/llms/text_generation_without_translation?text={text}&actor={actor}&response={response}");
                if(generatedTexts.Count > 0){
                    List<Sentiment> sentiments = await _httpClient.GetFromJsonAsync<List<Sentiment>>($"http://127.0.0.1:5005/nlp/generate_prompt_sentiments?prompt={generatedTexts[0].Text}");
                    List<Entity> entities = await _httpClient.GetFromJsonAsync<List<Entity>>($"http://127.0.0.1:5005/nlp/generate_prompt_entities_by_filter?prompt={generatedTexts[0].Text}");
                    GeneratedTextFeatures generatedTextFeatures = new GeneratedTextFeatures {
                        Text = generatedTexts[0].Text, Sentiments = sentiments, Entities = entities
                    };
                    List<string> entitiesText = entities.Select(s => s.Text).ToList();
                    LlmTextGeneration textGeneration = new LlmTextGeneration{
                        Title = string.Join(",", entitiesText),
                        Text = text,
                        Actor = actor,
                        Response = response,
                        GeneratedText = generatedTextFeatures.Text,
                        CreatedAT = DateTime.Now.ToString("yyy-MM-dd")
                    };
                    await _context.LlmTextGenerations.AddAsync(textGeneration);
                    await _context.SaveChangesAsync();
                    return Ok(generatedTextFeatures);
                } else {
                    return BadRequest(new RequestResponse {Message = $"Count rows: {generatedTexts.Count}"});
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = $"Error: {ex.Message}"});
            }
        }
        [HttpGet("generate-text")]
        public async Task<IActionResult> GetTextGeneration(string text, string actor, string response){
            try
            {
                if(string.IsNullOrEmpty(text) || string.IsNullOrEmpty(actor) || string.IsNullOrEmpty(response))
                {
                    return NotFound($"Some values are empty.");
                }
                
                List<GeneratedText> generatedTexts = await _httpClient.GetFromJsonAsync<List<GeneratedText>>($"http://127.0.0.1:5005/llms/text_generation?text={text}&actor={actor}&response={response}");
                if(generatedTexts.Count > 0){
                    await _context.GeneratedTexts.AddAsync(generatedTexts[0]);
                    await _context.SaveChangesAsync();
                    
                    return Ok(new RequestResponse {Message = $"Saved record at {DateTime.Now}."});
                } else {
                    return BadRequest(new RequestResponse {Message = $"Count rows: {generatedTexts.Count}"});
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = $"Error: {ex.Message}"});
            }
        }
    }
    public class GeneratedTextFeatures
    {
        public string? Text {get;set;}
        public List<Sentiment>? Sentiments {get;set;}
        public List<Entity>? Entities {get;set;}
    }
    public class PartOfSpeech{
        public string? Text_ {get;set;}
        public string? Lemma_ {get;set;}
        public string? Pos_ {get;set;}
        public string? Tag_ {get;set;}
        public string? Dep_ {get;set;}
        public string? Shape_ {get;set;}
        public string? Is_Alpha {get;set;}
        public string? Is_Stop {get;set;}
    }
    public class Entity
    {
        public string? Text { get; set; }
        public string? StartChat { get; set; }
        public string? EndChar { get; set; }
        public string? Label { get; set; }
    }

    public class NounChunk
    {
        public string? Text { get; set; }
        public string? RootText { get; set; }
        public string? RootDep { get; set; }
        public string? RootHead { get; set; }
    }

    public class Sentiment
    {
        public string? Sentence { get; set; }
        public string? Positive { get; set; }
        public string? Negative { get; set; }
        public string? Neutral { get; set; }
        public string? Compound { get; set; }
    }
    public class Answering {
        public string? Answer { get; set; }
        public string? End { get; set; }
        public string? Score { get; set; }
        public string? Start { get; set; }
    }
}