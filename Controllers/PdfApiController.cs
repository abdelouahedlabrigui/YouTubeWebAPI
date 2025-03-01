using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UglyToad.PdfPig;
using YouTubeWebAPI.Models;
using YouTubeWebAPI.Models.Documents;
using YouTubeWebAPI.Models.Responses;

namespace YouTubeWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PdfApiController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AppDbContext _context;

        public PdfApiController(IHttpClientFactory httpClientFactory, AppDbContext context)
        {
            _httpClientFactory = httpClientFactory;
            _context = context;
        }

        [HttpGet("get-text-document")]
        public async Task<IActionResult> GetTextDocument(int? id){
            try
            {
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    return NotFound();
                }

                var query = await _context.TextDocuments
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
        [HttpGet("search-text-document")]
        public async Task<IActionResult> SearchTextDocument(string search){
            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    return NotFound();
                }

                var query = await _context.TextDocuments
                    .Where(w => w.Title.Contains($"{search}"))
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
        [HttpGet("get-text-documents")]
        public async Task<IActionResult> GetTextDocuments(){
            try
            {
                var query = await _context.TextDocuments
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
        [HttpGet("DeleteTextDocument")]
        public async Task<IActionResult> DeleteTextDocument(int id){
            try
            {
                var query = await _context.TextDocuments.Where(w => w.Id == id).ToListAsync();
                _context.TextDocuments?.Remove(query[0]);
                await _context.SaveChangesAsync();
                return Ok(new RequestResponse {Message = $"Text document deleted successfully."});
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("get-pdf")]
        public async Task<IActionResult> GetPdf(int? id){
            try
            {
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    return NotFound();
                }

                var query = await _context.SearchedDocuments
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
        [HttpGet("search-pdfs")]
        public async Task<IActionResult> SearchPdfs(string search){
            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    return NotFound();
                }

                var query = await _context.SearchedDocuments
                    .Where(w => w.Title.Contains($"{search}"))
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
        [HttpGet("get-pdfs")]
        public async Task<IActionResult> GetPdfs(){
            try
            {
                var query = await _context.SearchedDocuments
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
        [HttpGet("DeletePdf")]
        public async Task<IActionResult> DeletePdf(int id){
            try
            {
                var query = await _context.SearchedDocuments.Where(w => w.Id == id).ToListAsync();
                _context.SearchedDocuments?.Remove(query[0]);
                await _context.SaveChangesAsync();
                return Ok(new RequestResponse {Message = $"PDF deleted successfully."});
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("extract-text")]
        public async Task<IActionResult> ExtractTextFromPdf(string pdfFilePath, string title){
            if (string.IsNullOrWhiteSpace(pdfFilePath) || string.IsNullOrWhiteSpace(title))
            {
                return BadRequest(new {Message = "PDF file path is required."});
            }
            if (!System.IO.File.Exists(pdfFilePath))
            {
                return NotFound(new {Message = "PDF file not found."});
            }
            try
            {
                string extractedText;
                using (var pdfDocument = PdfDocument.Open(pdfFilePath))
                {
                    var textBuilder = new System.Text.StringBuilder();
                    foreach (var page in pdfDocument.GetPages())
                    {
                        var text = page.Text;
                        textBuilder.AppendLine(text);
                    }
                    extractedText = textBuilder.ToString();
                }
                string textFilePath = Path.ChangeExtension(pdfFilePath, ".txt");
                await System.IO.File.WriteAllTextAsync(textFilePath, extractedText);
                if (!System.IO.File.Exists(textFilePath))
                {
                    return BadRequest(new {Message = "Text file not found."});
                }
                FileInfo fileInfo = null;
                fileInfo = new FileInfo(textFilePath);
                TextDocument textDocument = new TextDocument
                {
                    Title = title,
                    OfflineUrl = textFilePath,
                    Name = fileInfo.Name.ToString(),
                    LastWriteTime = fileInfo.LastWriteTime.ToString(),
                    LastAccessTime = fileInfo.LastAccessTime.ToString(),
                    Length = fileInfo.Length.ToString(),
                    CreatedAT = DateTime.Now.ToString()
                };
                await _context.TextDocuments.AddAsync(textDocument);
                await _context.SaveChangesAsync();
                return Ok(new RequestResponse {
                    Message = $"Text extracted and saved successfully to {textFilePath}."
                });                
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");                
            }
        }

        [HttpGet("save-pdf")]
        public async Task<IActionResult> SavePdfAsync(string title, string url)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(url))
            {
                return BadRequest(new {Message = "Title, URL, and Local Path are required."});
            }

            string fileName = Regex.Replace(title, @"[^\w\d-]", "_") + ".pdf";
            string localPath = @"C:\Users\dell\Entrepreneurship\Books\NewsPapers";
            string filePath = Path.Combine(localPath, fileName);
            try
            {
                Directory.CreateDirectory(localPath);

                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync(url);
                // if (!response.IsSuccessStatusCode)
                // {
                //     return BadRequest(new {Message = $"Failed to download PDF from url/ {url}"}); 
                // }

                var pdfBytes = await response.Content.ReadAsByteArrayAsync();

                await System.IO.File.WriteAllBytesAsync(filePath, pdfBytes);
                if (System.IO.File.Exists(filePath))
                {
                    FileInfo fileInfo = null;
                    fileInfo = new FileInfo(filePath);
                    SearchedDocument document = new SearchedDocument
                    {
                        Title = title,
                        OnlineUrl = url,
                        OfflineUrl = filePath,
                        Name = fileInfo.Name.ToString(),
                        LastWriteTime = fileInfo.LastWriteTime.ToString(),
                        LastAccessTime = fileInfo.LastAccessTime.ToString(),
                        Length = fileInfo.Length.ToString(),
                        CreatedAT = DateTime.Now.ToString()
                    };
                    _context.SearchedDocuments?.Add(document);
                    await _context.SaveChangesAsync();
                    return Ok(new RequestResponse {Message = $"PDF saved successfully. Path: {filePath}"});
                } else {
                    return Ok(new RequestResponse {Message = $"PDF not saved."});
                }             
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }            
        }   
    }
}