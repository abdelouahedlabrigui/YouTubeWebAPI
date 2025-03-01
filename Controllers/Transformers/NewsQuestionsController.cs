using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YouTubeWebAPI.Models;
using YouTubeWebAPI.Models.NLP.Llms.Questions;
using YouTubeWebAPI.Models.Responses;

namespace YouTubeWebAPI.Controllers.Transformers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsQuestionsController : ControllerBase
    {
        private readonly AppDbContext? _context;
        public NewsQuestionsController(AppDbContext? context)
        {
            _context = context;
        }
        [HttpGet("delete-question")]
        public async Task<IActionResult> DeleteQuestion(int? id){
            try
            {
                var query = await _context.QuestionsStores.Where(w => w.Id == id).ToListAsync();
                if (query.Count == 0){
                    return NotFound(new RequestResponse {Message = $"N° rows: {query.Count}"});
                }
                _context.QuestionsStores.Remove(query[0]);
                await _context.SaveChangesAsync();
                return Ok(new RequestResponse {Message = "Deleted record."});
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("get-question-by-id")]
        public async Task<IActionResult> GetQuestionById(int? id){
            try
            {
                var query = await _context.QuestionsStores.Where(w => w.Id == id).OrderByDescending(o => o.Id).ToListAsync();
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
        [HttpGet("get-questions-by-search")]
        public async Task<IActionResult> GetQuestionsBySearch(string? search){
            try
            {
                var query = await _context.QuestionsStores.Where(w => w.Title.Contains($"{search}")).OrderByDescending(o => o.Id).ToListAsync();
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
        [HttpGet("get-questions")]
        public async Task<IActionResult> GetQuestions(){
            try
            {
                var query = await _context.QuestionsStores.Take(3).OrderByDescending(o => o.Id).ToListAsync();
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

        [HttpGet("post-question")]
        public async Task<IActionResult> PostQuestion(string? textPath, string? title, string? searchString, string? question){
            try
            {
                if (string.IsNullOrWhiteSpace(textPath) || string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(searchString) || 
                    string.IsNullOrWhiteSpace(question))
                {
                    return NotFound(new RequestResponse {Message = "Possible null value(s)"});
                }
                QuestionsStore questionsStore = new QuestionsStore{
                    TextPath = textPath,
                    Title = title,
                    SearchString = searchString,
                    Question = question,
                    CreatedAT = DateTime.Now.ToString()
                };
                await _context.QuestionsStores.AddAsync(questionsStore);
                await _context.SaveChangesAsync();
                return Ok(new RequestResponse {Message = "Added record."}); 
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
    }
}