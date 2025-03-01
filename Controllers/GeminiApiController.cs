using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UglyToad.PdfPig.Content;
using YouTubeWebAPI.Models;
using YouTubeWebAPI.Models.Prompts;
using YouTubeWebAPI.Models.Responses;
using Word = Microsoft.Office.Interop.Word;

namespace YouTubeWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeminiApiController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;

        public GeminiApiController(IConfiguration configuration, AppDbContext context)
        {
            _configuration = configuration;
            _context = context;
            _httpClient = new HttpClient();
        }
        [HttpGet("get-document")]
        public async Task<IActionResult> GetDocument(int? id){
            try
            {
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    return NotFound();
                }

                var query = await _context.Prompts
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
        [HttpGet("search-prompt")]
        public async Task<IActionResult> SearchPrompts(string search){
            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    return NotFound();
                }

                var query = await _context.Prompts
                    .Where(w => w.PromptString.Contains($"{search}"))
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
        [HttpGet("get-documents")]
        public async Task<IActionResult> GetDocuments(){
            try
            {
                var query = await _context.Prompts
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
        [HttpGet("DeleteDocument")]
        public async Task<IActionResult> DeleteDocument(int id){
            try
            {
                var query = await _context.Prompts.Where(w => w.Id == id).ToListAsync();
                _context.Prompts?.Remove(query[0]);
                await _context.SaveChangesAsync();
                return Ok(new RequestResponse {Message = $"PDF deleted successfully."});
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("get-raw-document")]
        public async Task<IActionResult> GetRawDocument(int? id){
            try
            {
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    return NotFound();
                }

                var query = await _context.PromptRawResults
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
        [HttpGet("search-raw-prompt")]
        public async Task<IActionResult> SearchRawPrompts(string search){
            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    return NotFound();
                }

                var query = await _context.PromptRawResults
                    .Where(w => w.PromptString.Contains($"{search}"))
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
        [HttpGet("get-raw-documents")]
        public async Task<IActionResult> GetRawDocuments(){
            try
            {
                var query = await _context.PromptRawResults
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
        [HttpGet("post-raw-document")]
        public async Task<IActionResult> PostRawDocument(string promptString, string txtPath){
            try
            {
                if (string.IsNullOrWhiteSpace(promptString) || string.IsNullOrWhiteSpace(txtPath))
                {
                    return NotFound();
                }
                if (System.IO.File.Exists(txtPath))
                {
                    PromptRawResult result = new PromptRawResult{
                        PromptString = promptString,
                        TextPath = txtPath,
                        CreatedAT = DateTime.Now.ToString(),
                    };
                    _context.PromptRawResults.Add(result);  
                    await _context.SaveChangesAsync();
                    return Ok(new RequestResponse {Message = $"Prompt saved successfully."});
                }
                else {
                    return BadRequest(new RequestResponse {Message = "Text file does not exist."});
                }
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("DeleteRawDocument")]
        public async Task<IActionResult> DeleteRawDocument(int id){
            try
            {
                var query = await _context.PromptRawResults.Where(w => w.Id == id).ToListAsync();
                _context.PromptRawResults?.Remove(query[0]);
                await _context.SaveChangesAsync();
                return Ok(new RequestResponse {Message = $"PDF deleted successfully."});
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("load-file")]
        public IActionResult LoadFile(string filePath)
        {
            try
            {
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound($"File not found: {filePath}");
                }
                string fileContent = System.IO.File.ReadAllText(filePath);
                return Content(fileContent, "text/plain");
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Error loading file: {ex.Message}");
            }
        }
        [HttpGet("generate-prompt-using-mistral-ai")]
        public async Task<IActionResult> GeneratePromptUsingMistral(string prompt_string, string output_doc){
            try
            {
                if(string.IsNullOrWhiteSpace(prompt_string) || string.IsNullOrWhiteSpace(output_doc)){
                    return NotFound();
                }
                RequestResponse response = await _httpClient.GetFromJsonAsync<RequestResponse>($"http://127.0.0.1:5005/gen/prompt_mistral_ai?prompt_string={prompt_string}&output_doc={output_doc}");
                if (response.Message == "operation ended")
                {
                    return Ok(new RequestResponse {Message = $"Prompt generated successfully."});
                } else {
                    return BadRequest(new RequestResponse {Message = response.Message});
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = $"Error: {ex.Message}"});
            }
        }
        [HttpGet("generate-prompt")]
        // [Authorize]
        public async Task<IActionResult> GeneratePrompt(string promptString, string txtPath){
            try
            {
                if (string.IsNullOrWhiteSpace(promptString) || string.IsNullOrWhiteSpace(txtPath))
                {
                    return NotFound();
                }
                if (!System.IO.File.Exists(txtPath))
                {        
                    string apiKey = _configuration["Gemini:ApiKey"];            
                    PromptRawResult rawResult = new PromptRawResult{
                        PromptString = promptString,
                        TextPath = txtPath,
                        CreatedAT = DateTime.Now.ToString(),
                    };
                    var check = await _context.PromptRawResults
                        .Where(w => w.TextPath == txtPath)
                        .ToListAsync();
                    if (check.Count == 0)
                    {
                        string responseText = await CallGeminiApiAsync(apiKey, rawResult.PromptString);
                        await SaveApiResponseToFileAsync(responseText, rawResult.TextPath);
                        _context.PromptRawResults.Add(rawResult);
                        await _context.SaveChangesAsync();
                        return Ok(new RequestResponse {Message = $"Output generated into {rawResult.TextPath}"});
                    } else {
                        return BadRequest(new RequestResponse {Message = "Prompt already in database."});
                    }
                } else {
                    return Ok(new RequestResponse {Message = $"Text file already exists."});
                }                
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = $"{ex.Message}" });
            }
        }

        [HttpGet("generate-prompt-pdf")]
        public async Task<IActionResult> GeneratePdf(string agent, string promptString, string pdfPath, string txtPath){
            try
            {
                if (string.IsNullOrWhiteSpace(agent) || string.IsNullOrWhiteSpace(promptString) || string.IsNullOrWhiteSpace(pdfPath) || string.IsNullOrWhiteSpace(txtPath))
                {
                    return NotFound();
                }
                var check = await _context.Prompts
                    .Where(w => w.PromptString == promptString)
                    .ToListAsync();
                
                    if (check.Count == 0)
                    {                   
                        string apiKey = _configuration["Gemini:ApiKey"];
                    if (string.IsNullOrEmpty(apiKey))
                    {
                        return BadRequest("Gemini API Key isn't configured.");
                    }

                    string responseText = await CallGeminiApiAsync(apiKey, promptString);
                    await SaveApiResponseToFileAsync(responseText, txtPath);
                    if (string.IsNullOrWhiteSpace(responseText))
                    {
                        return BadRequest("Gemini API response is empty.");
                    }
                    HttpClient _httpClient = new HttpClient();
                    if (System.IO.File.Exists(txtPath))
                    {
                        RequestResponse requestResponse = await _httpClient.GetFromJsonAsync<RequestResponse>($"http://127.0.0.1:5005/gen/generate_prompt_output_pdf_document?agent={agent}&textFile={txtPath}&pdfFile={pdfPath}");

                        if (requestResponse.Message.Contains("Operation ended."))
                        {
                            

                            Prompt prompt = new Prompt(){
                                PromptString = promptString,
                                PdfPath = pdfPath,
                                TextPath = txtPath,
                                CreatedAT = DateTime.Now.ToString(),
                            }; 
                            _context.Prompts.Add(prompt);
                            await _context.SaveChangesAsync();
                            return Ok(new RequestResponse {Message = requestResponse.Message});                        
                        } else {
                            return BadRequest(new RequestResponse {Message = requestResponse.Message});
                        }                        
                    } else {
                        return Ok(new RequestResponse {Message = "Text file not found."});
                    }             
                } else {
                    return Ok(new RequestResponse {Message = "Prompt already exists."});
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = $"{ex.Message}" });
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

        private async Task SaveApiResponseToFileAsync(string response, string textPath){
            try
            {
                var directory = Path.GetDirectoryName(textPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                await System.IO.File.WriteAllTextAsync(textPath, response);                   
            }
            catch (System.Exception ex)
            {    
                Console.WriteLine(ex.Message);            
                throw;
            }
        }

        private void GenerateWordDocument(string content, string wordFilePath){
            var wordApp = new Word.Application();
            var document = wordApp.Documents.Add();

            try
            {
                var paragraph = document.Content.Paragraphs.Add();
                paragraph.Range.Text = content;
                document.SaveAs2(wordFilePath);
            }
            finally
            {
                document.Close(false);
                wordApp.Quit();
            }
        }

        private void SaveAsPdf(string wordFilePath, string pdfFilePath){
            var wordApp = new Word.Application();
            var document = wordApp.Documents.Open(wordFilePath);
            try
            {
                document.ExportAsFixedFormat(pdfFilePath, Word.WdExportFormat.wdExportFormatPDF);
            } finally {
                document.Close(false);
                wordApp.Quit();
            }
        }
    }
}