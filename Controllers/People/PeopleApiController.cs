using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YouTubeWebAPI.Models;
using YouTubeWebAPI.Models.Responses;
using YouTubeWebAPI.Models.Search.NewsAPI;

namespace YouTubeWebAPI.Controllers.People
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PeopleApiController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("get-person")]
        public async Task<IActionResult> GetPerson(int? id){
            try
            {
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    return NotFound();
                }

                var query = await _context.People
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
        [HttpGet("search-person")]
        public async Task<IActionResult> SearchPeople(string search){
            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    return NotFound();
                }

                var query = await _context.People
                    .Where(w => w.FullName.Contains($"{search}"))
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
        [HttpGet("get-people")]
        public async Task<IActionResult> GetPeople(){
            try
            {
                var query = await _context.People
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
        [HttpGet("DeletePerson")]
        public async Task<IActionResult> DeletePerson(int id){
            try
            {
                var query = await _context.People.Where(w => w.Id == id).ToListAsync();
                _context.People?.Remove(query[0]);
                await _context.SaveChangesAsync();
                return Ok(new RequestResponse {Message = $"Person deleted successfully."});
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("post-person")]
        public async Task<IActionResult> PostPerson(string fullName, string  role, string birthdate, string state){
            try
            {
                if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(role) || string.IsNullOrEmpty(birthdate) || string.IsNullOrEmpty(state))
                {
                    return NotFound();
                }
                var person = new Person{
                    FullName = fullName,
                    Role = role,
                    Birthdate = birthdate,
                    State = state,
                    CreatedAT = DateTime.Now.ToString()
                };
                await _context.People.AddAsync(person);
                await _context.SaveChangesAsync();
                return Ok(new RequestResponse {Message = $"Person created successfully."});
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}