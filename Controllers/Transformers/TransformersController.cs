using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YouTubeWebAPI.Models;
using YouTubeWebAPI.Models.NLP.Llms;
using YouTubeWebAPI.Models.Responses;

namespace YouTubeWebAPI.Controllers.Transformers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransformersController : ControllerBase
    {
        private readonly AppDbContext? _context;
        private readonly HttpClient? _httpClient;

        public TransformersController(AppDbContext? context)
        {
            _context = context;
            _httpClient = new HttpClient();
        }
        [HttpGet("delete-text-classification")]
        public async Task<IActionResult> DeleteTextClassification(int? id){
            try
            {
                var query = await _context.LlmTextClassifications.Where(w => w.Id == id).ToListAsync();
                if (query.Count == 0){
                    return NotFound(new RequestResponse {Message = $"N° rows: {query.Count}"});
                }
                _context.LlmTextClassifications.Remove(query[0]);
                await _context.SaveChangesAsync();
                return Ok(new RequestResponse {Message = "Deleted record."});
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("get-text-classification-by-id")]
        public async Task<IActionResult> GetTextClassificationById(int? id){
            try
            {
                var query = await _context.LlmTextClassifications.Where(w => w.Id == id).OrderByDescending(o => o.Id).ToListAsync();
                if (query.Count == 0){
                    return NotFound(new RequestResponse {Message = $"N° rows: {query.Count}"});
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("get-text-classification-by-search")]
        public async Task<IActionResult> GetTextClassificationBySearch(string? search){
            try
            {
                var query = await _context.LlmTextClassifications.Where(w => w.Title.Contains($"{search}")).OrderByDescending(o => o.Id).ToListAsync();
                if (query.Count == 0){
                    return NotFound(new RequestResponse {Message = $"N° rows: {query.Count}"});
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("get-text-classification")]
        public async Task<IActionResult> GetTextClassification(){
            try
            {
                var query = await _context.LlmTextClassifications.Take(3).OrderByDescending(o => o.Id).ToListAsync();
                if (query.Count == 0){
                    return NotFound(new RequestResponse {Message = $"N° rows: {query.Count}"});
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("text-classification")]
        public async Task<IActionResult> TextClassification(string title, string text, string sector){
            try
            {
                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(text) || string.IsNullOrEmpty(sector))
                {
                    return NotFound(new RequestResponse {Message = "Possible null value(s)"});
                }
                List<LlmTextClassification>? data = await _httpClient.GetFromJsonAsync<List<LlmTextClassification>>($"http://127.0.0.1:5005/llms/text_classification?title={title}&text={text}&sector={sector}");
                if (data?.Count == 0)
                {
                    return Ok(new RequestResponse {Message = $"Count rows: {data?.Count}"});
                } else {
                    await _context.LlmTextClassifications.AddRangeAsync(data);
                    await _context.SaveChangesAsync();
                    return Ok(new RequestResponse {Message = $"Stored {data?.Count} rows."});
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("delete-ner-tagger")]
        public async Task<IActionResult> DeleteNerTagger(int? id){
            try
            {
                var query = await _context.LlmNamedEntityRecognitions.Where(w => w.Id == id).ToListAsync();
                if (query.Count == 0){
                    return NotFound(new RequestResponse {Message = $"N° rows: {query.Count}"});
                }
                _context.LlmNamedEntityRecognitions.Remove(query[0]);
                await _context.SaveChangesAsync();
                return Ok(new RequestResponse {Message = "Deleted record."});
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("get-ner-tagger-by-id")]
        public async Task<IActionResult> GetNerTaggerById(int? id){
            try
            {
                var query = await _context.LlmNamedEntityRecognitions.Where(w => w.Id == id).OrderByDescending(o => o.Id).ToListAsync();
                if (query.Count == 0){
                    return NotFound(new RequestResponse {Message = $"N° rows: {query.Count}"});
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("get-ner-tagger-by-search")]
        public async Task<IActionResult> GetNerTaggerBySearch(string? search){
            try
            {
                var query = await _context.LlmNamedEntityRecognitions.Where(w => w.Title.Contains($"{search}")).OrderByDescending(o => o.Id).ToListAsync();
                if (query.Count == 0){
                    return NotFound(new RequestResponse {Message = $"N° rows: {query.Count}"});
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("get-ner-tagger")]
        public async Task<IActionResult> GetNerTagger(){
            try
            {
                var query = await _context.LlmNamedEntityRecognitions.Take(3).OrderByDescending(o => o.Id).ToListAsync();
                if (query.Count == 0){
                    return NotFound(new RequestResponse {Message = $"N° rows: {query.Count}"});
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("ner-tagger")]
        public async Task<IActionResult> NerTagger(string title, string text, string sector){
            try
            {
                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(text) || string.IsNullOrEmpty(sector))
                {
                    return NotFound(new RequestResponse {Message = "Possible null value(s)"});
                }
                List<LlmNamedEntityRecognition>? data = await _httpClient.GetFromJsonAsync<List<LlmNamedEntityRecognition>>($"http://127.0.0.1:5005/llms/ner_tagger?title={title}&text={text}&sector={sector}");
                if (data?.Count == 0)
                {
                    return Ok(new RequestResponse {Message = $"Count rows: {data?.Count}"});
                } else {
                    await _context.LlmNamedEntityRecognitions.AddRangeAsync(data);
                    await _context.SaveChangesAsync();
                    return Ok(new RequestResponse {Message = $"Stored {data?.Count} rows."});
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("delete-question-answering")]
        public async Task<IActionResult> DeleteQuestionAnswering(int? id){
            try
            {
                var query = await _context.LlmQuestionAnswerings.Where(w => w.Id == id).ToListAsync();
                if (query.Count == 0){
                    return NotFound(new RequestResponse {Message = $"N° rows: {query.Count}"});
                }
                _context.LlmQuestionAnswerings.Remove(query[0]);
                await _context.SaveChangesAsync();
                return Ok(new RequestResponse {Message = "Deleted record."});
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("get-question-answering-by-id")]
        public async Task<IActionResult> GetQuestionAnsweringById(int? id){
            try
            {
                var query = await _context.LlmQuestionAnswerings.Where(w => w.Id == id).OrderByDescending(o => o.Id).ToListAsync();
                if (query.Count == 0){
                    return NotFound(new RequestResponse {Message = $"N° rows: {query.Count}"});
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("get-question-answering-by-search")]
        public async Task<IActionResult> GetQuestionAnsweringBySearch(string? search){
            try
            {
                var query = await _context.LlmQuestionAnswerings.Where(w => w.Title.Contains($"{search}")).OrderByDescending(o => o.Id).ToListAsync();
                if (query.Count == 0){
                    return NotFound(new RequestResponse {Message = $"N° rows: {query.Count}"});
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("get-question-answering")]
        public async Task<IActionResult> GetQuestionAnswering(){
            try
            {
                var query = await _context.LlmQuestionAnswerings.Take(3).OrderByDescending(o => o.Id).ToListAsync();
                if (query.Count == 0){
                    return NotFound(new RequestResponse {Message = $"N° rows: {query.Count}"});
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("question-answering")]
        public async Task<IActionResult> QuestionAnswering(string title, string text, string question, string sector){
            try
            {
                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(text) || string.IsNullOrEmpty(question) || string.IsNullOrEmpty(sector))
                {
                    return NotFound(new RequestResponse {Message = "Possible null value(s)"});
                }
                List<LlmQuestionAnswering>? data = await _httpClient.GetFromJsonAsync<List<LlmQuestionAnswering>>($"http://127.0.0.1:5005/llms/question_answering?title={title}&text={text}&question={question}&sector={sector}");
                if (data?.Count == 0)
                {
                    return Ok(new RequestResponse {Message = $"Count rows: {data?.Count}"});
                } else {
                    await _context.LlmQuestionAnswerings.AddRangeAsync(data);
                    await _context.SaveChangesAsync();
                    return Ok(new RequestResponse {Message = $"Stored {data?.Count} rows."});
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("delete-summarization")]
        public async Task<IActionResult> DeleteSummarization(int? id){
            try
            {
                var query = await _context.LlmTextSummarizations.Where(w => w.Id == id).ToListAsync();
                if (query.Count == 0){
                    return NotFound(new RequestResponse {Message = $"N° rows: {query.Count}"});
                }
                _context.LlmTextSummarizations.Remove(query[0]);
                await _context.SaveChangesAsync();
                return Ok(new RequestResponse {Message = "Deleted record."});
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("get-summarization-by-id")]
        public async Task<IActionResult> GetSummarizationById(int? id){
            try
            {
                var query = await _context.LlmTextSummarizations.Where(w => w.Id == id).OrderByDescending(o => o.Id).ToListAsync();
                if (query.Count == 0){
                    return NotFound(new RequestResponse {Message = $"N° rows: {query.Count}"});
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("get-summarization-by-search")]
        public async Task<IActionResult> GetSummarizationBySearch(string? search){
            try
            {
                var query = await _context.LlmTextSummarizations.Where(w => w.Title.Contains($"{search}")).OrderByDescending(o => o.Id).ToListAsync();
                if (query.Count == 0){
                    return NotFound(new RequestResponse {Message = $"N° rows: {query.Count}"});
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("get-summarization")]
        public async Task<IActionResult> GetSummarization(){
            try
            {
                var query = await _context.LlmTextSummarizations.Take(3).OrderByDescending(o => o.Id).ToListAsync();
                if (query.Count == 0){
                    return NotFound(new RequestResponse {Message = $"N° rows: {query.Count}"});
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("summarization")]
        public async Task<IActionResult> Summarization(string title, string text, string sector){
            try
            {
                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(text) || string.IsNullOrEmpty(sector))
                {
                    return NotFound(new RequestResponse {Message = "Possible null value(s)"});
                }
                List<LlmTextSummarization>? data = await _httpClient.GetFromJsonAsync<List<LlmTextSummarization>>($"http://127.0.0.1:5005/llms/summarization?title={title}&text={text}&sector={sector}");
                if (data?.Count == 0)
                {
                    return Ok(new RequestResponse {Message = $"Count rows: {data?.Count}"});
                } else {
                    await _context.LlmTextSummarizations.AddRangeAsync(data);
                    await _context.SaveChangesAsync();
                    return Ok(new RequestResponse {Message = $"Stored {data?.Count} rows."});
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("delete-translation")]
        public async Task<IActionResult> DeleteTranslation(int? id){
            try
            {
                var query = await _context.LlmTextTranslations.Where(w => w.Id == id).ToListAsync();
                if (query.Count == 0){
                    return NotFound(new RequestResponse {Message = $"N° rows: {query.Count}"});
                }
                _context.LlmTextTranslations.Remove(query[0]);
                await _context.SaveChangesAsync();
                return Ok(new RequestResponse {Message = "Deleted record."});
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("get-translation-by-id")]
        public async Task<IActionResult> GetTranslationById(int? id){
            try
            {
                var query = await _context.LlmTextTranslations.Where(w => w.Id == id).OrderByDescending(o => o.Id).ToListAsync();
                if (query.Count == 0){
                    return NotFound(new RequestResponse {Message = $"N° rows: {query.Count}"});
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("get-translation-by-search")]
        public async Task<IActionResult> GetTranslationBySearch(string? search){
            try
            {
                var query = await _context.LlmTextTranslations.Where(w => w.Title.Contains($"{search}")).OrderByDescending(o => o.Id).ToListAsync();
                if (query.Count == 0){
                    return NotFound(new RequestResponse {Message = $"N° rows: {query.Count}"});
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("get-translation")]
        public async Task<IActionResult> GetTranslation(){
            try
            {
                var query = await _context.LlmTextTranslations.Take(3).OrderByDescending(o => o.Id).ToListAsync();
                if (query.Count == 0){
                    return NotFound(new RequestResponse {Message = $"N° rows: {query.Count}"});
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("translation")]
        public async Task<IActionResult> Translation(string title, string text, string sector){
            try
            {
                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(text) || string.IsNullOrEmpty(sector))
                {
                    return NotFound(new RequestResponse {Message = "Possible null value(s)"});
                }
                List<LlmTextTranslation>? data = await _httpClient.GetFromJsonAsync<List<LlmTextTranslation>>($"http://127.0.0.1:5005/llms/translation?title={title}&text={text}&sector={sector}");
                if (data?.Count == 0)
                {
                    return Ok(new RequestResponse {Message = $"Count rows: {data?.Count}"});
                } else {
                    await _context.LlmTextTranslations.AddRangeAsync(data);
                    await _context.SaveChangesAsync();
                    return Ok(new RequestResponse {Message = $"Stored {data?.Count} rows."});
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("delete-text-generation")]
        public async Task<IActionResult> DeleteTextGeneration(int? id){
            try
            {
                var query = await _context.LlmTextGenerations.Where(w => w.Id == id).ToListAsync();
                if (query.Count == 0){
                    return NotFound(new RequestResponse {Message = $"N° rows: {query.Count}"});
                }
                _context.LlmTextGenerations.Remove(query[0]);
                await _context.SaveChangesAsync();
                return Ok(new RequestResponse {Message = "Deleted record."});
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("get-text-generation-by-id")]
        public async Task<IActionResult> GetTextGenerationById(int? id){
            try
            {
                var query = await _context.LlmTextGenerations.Where(w => w.Id == id).OrderByDescending(o => o.Id).ToListAsync();
                if (query.Count == 0){
                    return NotFound(new RequestResponse {Message = $"N° rows: {query.Count}"});
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("get-text-generation-by-search")]
        public async Task<IActionResult> GetTextGenerationBySearch(string? search){
            try
            {
                var query = await _context.LlmTextGenerations.Where(w => w.Title.Contains($"{search}")).OrderByDescending(o => o.Id).ToListAsync();
                if (query.Count == 0){
                    return NotFound(new RequestResponse {Message = $"N° rows: {query.Count}"});
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("get-text-generation")]
        public async Task<IActionResult> GetTextGeneration(){
            try
            {
                var query = await _context.LlmTextGenerations.Take(3).OrderByDescending(o => o.Id).ToListAsync();
                if (query.Count == 0){
                    return NotFound(new RequestResponse {Message = $"N° rows: {query.Count}"});
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("text-generation")]
        public async Task<IActionResult> TextGeneration(string title, string text, string actor, string response, string sector){
            try
            {
                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(text) || string.IsNullOrEmpty(actor) || string.IsNullOrEmpty(response) || string.IsNullOrEmpty(sector))
                {
                    return NotFound(new RequestResponse {Message = "Possible null value(s)"});
                }
                List<LlmTextGeneration>? data = await _httpClient.GetFromJsonAsync<List<LlmTextGeneration>>($"http://127.0.0.1:5005/llms/text_generation?title={title}&text={text}&actor={actor}&response={response}&sector={sector}");
                if (data?.Count == 0)
                {
                    return Ok(new RequestResponse {Message = $"Count rows: {data?.Count}"});
                } else {
                    await _context.LlmTextGenerations.AddRangeAsync(data);
                    await _context.SaveChangesAsync();
                    return Ok(new RequestResponse {Message = $"Stored {data?.Count} rows."});
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }

    }
}