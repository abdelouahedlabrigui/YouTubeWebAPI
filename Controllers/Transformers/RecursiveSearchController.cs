using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YouTubeWebAPI.Models;
using YouTubeWebAPI.Models.NLP.RecursiveSearch;
using YouTubeWebAPI.Models.NLP.RecursiveSearch.ContextSearch;
using YouTubeWebAPI.Models.NLP.RecursiveSearch.MatchSearch;
using YouTubeWebAPI.Models.Responses;

namespace YouTubeWebAPI.Controllers.Transformers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecursiveSearchController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;

        public RecursiveSearchController(AppDbContext context)
        {
            _context = context;
            _httpClient = new HttpClient(); 
        }
        [HttpGet("search-building")]
        public async Task<IActionResult> SearchBuildingContexts(string search){
            if (string.IsNullOrEmpty(search))
            {
                return NotFound(new RequestResponse {Message = "Parameter empty."});
            }
            var query = await _context.BuildingContexts.Where(w => w.Buildings.Contains($"{search}")).ToListAsync();
            if (query.Count == 0)
            {
                return BadRequest(new RequestResponse {Message = $"Query count: {query.Count}" });
            } else {
                return Ok(query);
            }
        }
        [HttpGet("search-company")]
        public async Task<IActionResult> SearchCompanyContexts(string search){
            if (string.IsNullOrEmpty(search))
            {
                return NotFound(new RequestResponse {Message = "Parameter empty."});
            }
            var query = await _context.CompanyContexts.Where(w => w.Companies.Contains($"{search}")).ToListAsync();
            if (query.Count == 0)
            {
                return BadRequest(new RequestResponse {Message = $"Query count: {query.Count}" });
            } else {
                return Ok(query);
            }
        }
        [HttpGet("search-country")]
        public async Task<IActionResult> SearchCountryContexts(string search){
            if (string.IsNullOrEmpty(search))
            {
                return NotFound(new RequestResponse {Message = "Parameter empty."});
            }
            var query = await _context.CountryContexts.Where(w => w.Countries.Contains($"{search}")).ToListAsync();
            if (query.Count == 0)
            {
                return BadRequest(new RequestResponse {Message = $"Query count: {query.Count}" });
            } else {
                return Ok(query);
            }
        }
        [HttpGet("search-nationality")]
        public async Task<IActionResult> SearchNationalityContexts(string search){
            if (string.IsNullOrEmpty(search))
            {
                return NotFound(new RequestResponse {Message = "Parameter empty."});
            }
            var query = await _context.NationalityContexts.Where(w => w.Nationalities.Contains($"{search}")).ToListAsync();
            if (query.Count == 0)
            {
                return BadRequest(new RequestResponse {Message = $"Query count: {query.Count}" });
            } else {
                return Ok(query);
            }
        }
        [HttpGet("search-non-gpe")]
        public async Task<IActionResult> SearchNonGpeContexts(string search){
            if (string.IsNullOrEmpty(search))
            {
                return NotFound(new RequestResponse {Message = "Parameter empty."});
            }
            var query = await _context.NonGpeContexts.Where(w => w.NonGpe.Contains($"{search}")).ToListAsync();
            if (query.Count == 0)
            {
                return BadRequest(new RequestResponse {Message = $"Query count: {query.Count}" });
            } else {
                return Ok(query);
            }
        }
        [HttpGet("search-person")]
        public async Task<IActionResult> SearchPersonContexts(string search){
            if (string.IsNullOrEmpty(search))
            {
                return NotFound(new RequestResponse {Message = "Parameter empty."});
            }
            var query = await _context.PersonContexts.Where(w => w.Person.Contains($"{search}")).ToListAsync();
            if (query.Count == 0)
            {
                return BadRequest(new RequestResponse {Message = $"Query count: {query.Count}" });
            } else {
                return Ok(query);
            }
        }
        [HttpGet("search-match-building")]
        public async Task<IActionResult> SearchMatchBuildingContexts(string search){
            if (string.IsNullOrEmpty(search))
            {
                return NotFound(new RequestResponse {Message = "Parameter empty."});
            }
            var query = await _context.MatchBuildingContexts.Where(w => w.Buildings.Contains($"{search}")).ToListAsync();
            if (query.Count == 0)
            {
                return BadRequest(new RequestResponse {Message = $"Query count: {query.Count}" });
            } else {
                return Ok(query);
            }
        }
        [HttpGet("search-match-company")]
        public async Task<IActionResult> SearchMatchCompanyContexts(string search){
            if (string.IsNullOrEmpty(search))
            {
                return NotFound(new RequestResponse {Message = "Parameter empty."});
            }
            var query = await _context.MatchCompanyContexts.Where(w => w.Companies.Contains($"{search}")).ToListAsync();
            if (query.Count == 0)
            {
                return BadRequest(new RequestResponse {Message = $"Query count: {query.Count}" });
            } else {
                return Ok(query);
            }
        }
        [HttpGet("search-match-country")]
        public async Task<IActionResult> SearchMatchCountryContexts(string search){
            if (string.IsNullOrEmpty(search))
            {
                return NotFound(new RequestResponse {Message = "Parameter empty."});
            }
            var query = await _context.MatchCountryContexts.Where(w => w.Countries.Contains($"{search}")).ToListAsync();
            if (query.Count == 0)
            {
                return BadRequest(new RequestResponse {Message = $"Query count: {query.Count}" });
            } else {
                return Ok(query);
            }
        }
        [HttpGet("search-match-nationality")]
        public async Task<IActionResult> SearchMatchNationalityContexts(string search){
            if (string.IsNullOrEmpty(search))
            {
                return NotFound(new RequestResponse {Message = "Parameter empty."});
            }
            var query = await _context.MatchNationalityContexts.Where(w => w.Nationalities.Contains($"{search}")).ToListAsync();
            if (query.Count == 0)
            {
                return BadRequest(new RequestResponse {Message = $"Query count: {query.Count}" });
            } else {
                return Ok(query);
            }
        }
        [HttpGet("search-match-non-gpe")]
        public async Task<IActionResult> SearchMatchNonGpeContexts(string search){
            if (string.IsNullOrEmpty(search))
            {
                return NotFound(new RequestResponse {Message = "Parameter empty."});
            }
            var query = await _context.MatchNonGpeContexts.Where(w => w.NonGpe.Contains($"{search}")).ToListAsync();
            if (query.Count == 0)
            {
                return BadRequest(new RequestResponse {Message = $"Query count: {query.Count}" });
            } else {
                return Ok(query);
            }
        }
        [HttpGet("search-match-person")]
        public async Task<IActionResult> SearchMatchPersonContexts(string search){
            if (string.IsNullOrEmpty(search))
            {
                return NotFound(new RequestResponse {Message = "Parameter empty."});
            }
            var query = await _context.MatchPersonContexts.Where(w => w.Person.Contains($"{search}")).ToListAsync();
            if (query.Count == 0)
            {
                return BadRequest(new RequestResponse {Message = $"Query count: {query.Count}" });
            } else {
                return Ok(query);
            }
        }
        [HttpGet("search-entities")]
        public async Task<IActionResult> SearchExtractEntities(string search){
            if (string.IsNullOrEmpty(search))
            {
                return NotFound(new RequestResponse {Message = "Parameter empty."});
            }
            var query = await _context.ExtractEntities.Where(w => w.Text.Contains($"{search}")).ToListAsync();
            if (query.Count == 0)
            {
                return BadRequest(new RequestResponse {Message = $"Query count: {query.Count}" });
            } else {
                return Ok(query);
            }
        }
        [HttpGet("search-features")]
        public async Task<IActionResult> SearchFeatureExtractions(string search){
            if (string.IsNullOrEmpty(search))
            {
                return NotFound(new RequestResponse {Message = "Parameter empty."});
            }
            var query = await _context.FeatureExtractions.Where(w => w.ExtractedAnswer.Contains($"{search}")).ToListAsync();
            if (query.Count == 0)
            {
                return BadRequest(new RequestResponse {Message = $"Query count: {query.Count}" });
            } else {
                return Ok(query);
            }
        }

        [HttpGet("search-wiki-search")]
        public async Task<IActionResult> SearchWikiSearchs(string search){
            if (string.IsNullOrEmpty(search))
            {
                return NotFound(new RequestResponse {Message = "Parameter empty."});
            }
            var query = await _context.WikiSearchs.Where(w => w.Title.Contains($"{search}")).ToListAsync();
            if (query.Count == 0)
            {
                return BadRequest(new RequestResponse {Message = $"Query count: {query.Count}" });
            } else {
                return Ok(query);
            }
        }


        [HttpGet("wiki-search")]
        public async Task<IActionResult> WikipediaDocumentSearch(string search_string){
            try
            {
                if (string.IsNullOrWhiteSpace(search_string))
                {
                    return NotFound(new RequestResponse {Message = "Not found."});
                } else {
                    List<WikiSearch> wikiSearch = await _httpClient.GetFromJsonAsync<List<WikiSearch>>($"http://127.0.0.1:5005/nlp/wikipedia_document_search?search_string={search_string}");
                    if (wikiSearch?.Count == 0)
                    {
                        return BadRequest(new RequestResponse {Message = $"Query count: {wikiSearch.Count}"});
                    } else {
                        return Ok(wikiSearch);
                    }
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse{Message = ex.Message});
            }
        }

        [HttpGet("extract-entities")]
        public async Task<IActionResult> ExtractEntities(string text_file, string label){
            try
            {
                if (string.IsNullOrWhiteSpace(text_file) || string.IsNullOrWhiteSpace(label))
                {
                    return NotFound(new RequestResponse {Message = "Not found."});
                } else {
                    List<ExtractEntity> query = await _httpClient.GetFromJsonAsync<List<ExtractEntity>>($"http://127.0.0.1:5005/nlp/extract_entities?text_file={text_file}&label={label}");
                    if (query?.Count == 0)
                    {
                        return BadRequest(new RequestResponse {Message = $"Query count: {query.Count}"});
                    } else {
                        return Ok(query);
                    }
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse{Message = ex.Message});
            }
        }
        

        [HttpGet("extract-context")]
        public async Task<IActionResult> ExtractContext(string text_file, string label){
            try
            {
                if (string.IsNullOrWhiteSpace(text_file) || string.IsNullOrWhiteSpace(label))
                {
                    return NotFound(new RequestResponse {Message = "Not found."});
                } else {
                    switch (label)
                    {
                        case "person":
                            List<PersonContext> person = await _httpClient.GetFromJsonAsync<List<PersonContext>>($"http://127.0.0.1:5005/nlp/extract_label_context?text_file={text_file}&label={label}");
                            if (person?.Count == 0)
                            {
                                return BadRequest(new RequestResponse {Message = $"Query count: {person.Count}"});
                            } else {
                                return Ok(person);
                            }
                        case "non_gpe":
                            List<NonGpeContext> nonGpe = await _httpClient.GetFromJsonAsync<List<NonGpeContext>>($"http://127.0.0.1:5005/nlp/extract_label_context?text_file={text_file}&label={label}");
                            if (nonGpe?.Count == 0)
                            {
                                return BadRequest(new RequestResponse {Message = $"Query count: {nonGpe.Count}"});
                            } else {
                                return Ok(nonGpe);
                            }
                        case "nationalities":
                            List<NationalityContext> nationalities = await _httpClient.GetFromJsonAsync<List<NationalityContext>>($"http://127.0.0.1:5005/nlp/extract_label_context?text_file={text_file}&label={label}");
                            if (nationalities?.Count == 0)
                            {
                                return BadRequest(new RequestResponse {Message = $"Query count: {nationalities.Count}"});
                            } else {
                                return Ok(nationalities);
                            }
                        case "buildings":
                            List<BuildingContext> buildings = await _httpClient.GetFromJsonAsync<List<BuildingContext>>($"http://127.0.0.1:5005/nlp/extract_label_context?text_file={text_file}&label={label}");
                            if (buildings?.Count == 0)
                            {
                                return BadRequest(new RequestResponse {Message = $"Query count: {buildings.Count}"});
                            } else {
                                return Ok(buildings);
                            }
                        case "companies":
                            List<CompanyContext> companies = await _httpClient.GetFromJsonAsync<List<CompanyContext>>($"http://127.0.0.1:5005/nlp/extract_label_context?text_file={text_file}&label={label}");
                            if (companies?.Count == 0)
                            {
                                return BadRequest(new RequestResponse {Message = $"Query count: {companies.Count}"});
                            } else {
                                return Ok(companies);
                            }
                        case "countries":
                            List<CountryContext> countries = await _httpClient.GetFromJsonAsync<List<CountryContext>>($"http://127.0.0.1:5005/nlp/extract_label_context?text_file={text_file}&label={label}");
                            if (countries?.Count == 0)
                            {
                                return BadRequest(new RequestResponse {Message = $"Query count: {countries.Count}"});
                            } else {
                                return Ok(countries);
                            }
                        default:
                            return Ok(new RequestResponse {Message = "Parameter doesn't match."});
                    }                    
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse{Message = ex.Message});
            }
        }

        [HttpGet("match-context")]
        public async Task<IActionResult> MatchContext(string text_file, string label, string entity_search_string, string search_string){
            try
            {
                if (string.IsNullOrWhiteSpace(text_file) || string.IsNullOrWhiteSpace(label) || string.IsNullOrWhiteSpace(entity_search_string) || string.IsNullOrWhiteSpace(search_string))
                {
                    return NotFound(new RequestResponse {Message = "Not found."});
                } else {
                    switch (label)
                        {
                        case "person":
                            List<MatchPersonContext> person = await _httpClient.GetFromJsonAsync<List<MatchPersonContext>>($"http://127.0.0.1:5005/nlp/match_label_context?text_file={text_file}&label={label}&entity_search_string={entity_search_string}&search_string={search_string}");
                            if (person?.Count == 0)
                            {
                                return BadRequest(new RequestResponse {Message = $"Query count: {person.Count}"});
                            } else {
                                return Ok(person);
                            }
                        case "non_gpe":
                            List<MatchNonGpeContext> nonGpe = await _httpClient.GetFromJsonAsync<List<MatchNonGpeContext>>($"http://127.0.0.1:5005/nlp/match_label_context?text_file={text_file}&label={label}&entity_search_string={entity_search_string}&search_string={search_string}");
                            if (nonGpe?.Count == 0)
                            {
                                return BadRequest(new RequestResponse {Message = $"Query count: {nonGpe.Count}"});
                            } else {
                                return Ok(nonGpe);
                            }
                        case "nationalities":
                            List<MatchNationalityContext> nationalities = await _httpClient.GetFromJsonAsync<List<MatchNationalityContext>>($"http://127.0.0.1:5005/nlp/match_label_context?text_file={text_file}&label={label}&entity_search_string={entity_search_string}&search_string={search_string}");
                            if (nationalities?.Count == 0)
                            {
                                return BadRequest(new RequestResponse {Message = $"Query count: {nationalities.Count}"});
                            } else {
                                return Ok(nationalities);
                            }
                        case "buildings":
                            List<MatchBuildingContext> buildings = await _httpClient.GetFromJsonAsync<List<MatchBuildingContext>>($"http://127.0.0.1:5005/nlp/match_label_context?text_file={text_file}&label={label}&entity_search_string={entity_search_string}&search_string={search_string}");
                            if (buildings?.Count == 0)
                            {
                                return BadRequest(new RequestResponse {Message = $"Query count: {buildings.Count}"});
                            } else {
                                return Ok(buildings);
                            }
                        case "companies":
                            List<MatchCompanyContext> companies = await _httpClient.GetFromJsonAsync<List<MatchCompanyContext>>($"http://127.0.0.1:5005/nlp/match_label_context?text_file={text_file}&label={label}&entity_search_string={entity_search_string}&search_string={search_string}");
                            if (companies?.Count == 0)
                            {
                                return BadRequest(new RequestResponse {Message = $"Query count: {companies.Count}"});
                            } else {
                                return Ok(companies);
                            }
                        case "countries":
                            List<MatchCountryContext> countries = await _httpClient.GetFromJsonAsync<List<MatchCountryContext>>($"http://127.0.0.1:5005/nlp/match_label_context?text_file={text_file}&label={label}&entity_search_string={entity_search_string}&search_string={search_string}");
                            if (countries?.Count == 0)
                            {
                                return BadRequest(new RequestResponse {Message = $"Query count: {countries.Count}"});
                            } else {
                                return Ok(countries);
                            }
                        default:
                            return Ok(new RequestResponse {Message = "Parameter doesn't match."});
                    }
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse{Message = ex.Message});
            }
        }

        [HttpGet("features-extraction")]
        public async Task<IActionResult> FeaturesExtraction(string search_string){
            try
            {
                if (string.IsNullOrWhiteSpace(search_string))
                {
                    return NotFound(new RequestResponse {Message = "Not found."});
                } else {
                    List<FeatureExtraction> features = await _httpClient.GetFromJsonAsync<List<FeatureExtraction>>($"http://127.0.0.1:5005/nlp/features_extraction?context={search_string}");
                    if (features?.Count == 0)
                    {
                        return BadRequest(new RequestResponse {Message = $"Query count: {features.Count}"});
                    } else {
                        return Ok(features);
                    }
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse{Message = ex.Message});
            }
        }


        [HttpGet("post-wiki-document")]
        public async Task<IActionResult> PostWikiDocument(string searchString, string title, string summary, string text, string createdAT){
            try
            {
                if (string.IsNullOrWhiteSpace(searchString) || string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(summary) || string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(createdAT))
                {
                    return NotFound(new RequestResponse {Message = "Input(s) not valid."});
                } else {
                    WikiSearch wikiSearch = new WikiSearch{
                        SearchString = searchString,
                        Title = title,
                        Summary = summary,
                        Text = text,
                        CreatedAT = createdAT,
                    };
                    await _context.WikiSearchs.AddAsync(wikiSearch);
                    await _context.SaveChangesAsync();

                    return Ok(new RequestResponse {Message = "Added record."});
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }

        [HttpGet("post-features-extraction")]
        public async Task<IActionResult> PostFeatureExtraction(string context, string sentiment, string sentimentScore, string extractedAnswer, string extractedScore, string events, string eventScore, string createdAT){
            try
            {
                if (string.IsNullOrWhiteSpace(context) || string.IsNullOrWhiteSpace(sentiment) || string.IsNullOrWhiteSpace(sentimentScore) || string.IsNullOrWhiteSpace(extractedAnswer) || string.IsNullOrWhiteSpace(extractedScore) || string.IsNullOrWhiteSpace(events) || string.IsNullOrWhiteSpace(eventScore) || string.IsNullOrWhiteSpace(createdAT))
                {
                    return NotFound(new RequestResponse {Message = "Input(s) not valid."});
                } else {
                    FeatureExtraction featuresExtractions = new FeatureExtraction{
                        Context = context,
                        Sentiment = sentiment,
                        SentimentScore = sentimentScore,
                        ExtractedAnswer = extractedAnswer,
                        ExtractedScore = extractedScore,
                        Events = events,
                        EventScore = eventScore,
                        CreatedAT = createdAT,
                    };
                    await _context.FeatureExtractions.AddAsync(featuresExtractions);
                    await _context.SaveChangesAsync();

                    return Ok(new RequestResponse {Message = "Added record."});
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }

        [HttpGet("post-extracted-entity")]
        public async Task<IActionResult> PostExtractEntity(string text, string label, string fileName, string createdAT){
            try
            {
                if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(label) || string.IsNullOrWhiteSpace(fileName) || string.IsNullOrWhiteSpace(createdAT))
                {
                    return NotFound(new RequestResponse {Message = "Input(s) not valid."});
                } else {
                    ExtractEntity entities = new ExtractEntity{
                        Text = text,
                        Label = label,
                        FileName = fileName,
                        CreatedAT = createdAT,
                    };
                    await _context.ExtractEntities.AddAsync(entities);
                    await _context.SaveChangesAsync();

                    return Ok(new RequestResponse {Message = "Added record."});
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }

        [HttpGet("post-building-match")]
        public async Task<IActionResult> PostMatchBuildingContext(string buildings, string context, string searchTerm, string selectedChunks, string fileName, string createdAT){
            try
            {
                if (string.IsNullOrWhiteSpace(buildings) || string.IsNullOrWhiteSpace(context) || string.IsNullOrWhiteSpace(searchTerm) || string.IsNullOrWhiteSpace(selectedChunks) || string.IsNullOrWhiteSpace(fileName) || string.IsNullOrWhiteSpace(createdAT))
                {
                    return NotFound(new RequestResponse {Message = "Input(s) not valid."});
                } else {
                    MatchBuildingContext match = new MatchBuildingContext{
                        Buildings = buildings,
                        Context = context,
                        SearchTerm = searchTerm,
                        SelectedChunks = selectedChunks,
                        FileName = fileName,
                        CreatedAT = createdAT
                    };
                    await _context.MatchBuildingContexts.AddAsync(match);
                    await _context.SaveChangesAsync();

                    return Ok(new RequestResponse {Message = "Added record."});
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("post-company-match")]
        public async Task<IActionResult> PostMatchCompanyContext(string companies, string context, string searchTerm, string selectedChunks, string fileName, string createdAT){
            try
            {
                if (string.IsNullOrWhiteSpace(companies) || string.IsNullOrWhiteSpace(context) || string.IsNullOrWhiteSpace(searchTerm) || string.IsNullOrWhiteSpace(selectedChunks) || string.IsNullOrWhiteSpace(fileName) || string.IsNullOrWhiteSpace(createdAT))
                {
                    return NotFound(new RequestResponse {Message = "Input(s) not valid."});
                } else {
                    MatchCompanyContext match = new MatchCompanyContext{
                        Companies = companies,
                        Context = context,
                        SearchTerm = searchTerm,
                        SelectedChunks = selectedChunks,
                        FileName = fileName,
                        CreatedAT = createdAT
                    };
                    await _context.MatchCompanyContexts.AddAsync(match);
                    await _context.SaveChangesAsync();

                    return Ok(new RequestResponse {Message = "Added record."});
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("post-countries-match")]
        public async Task<IActionResult> PostMatchCountryContext(string countries, string context, string searchTerm, string selectedChunks, string fileName, string createdAT){
            try
            {
                if (string.IsNullOrWhiteSpace(countries) || string.IsNullOrWhiteSpace(context) || string.IsNullOrWhiteSpace(searchTerm) || string.IsNullOrWhiteSpace(selectedChunks) || string.IsNullOrWhiteSpace(fileName) || string.IsNullOrWhiteSpace(createdAT))
                {
                    return NotFound(new RequestResponse {Message = "Input(s) not valid."});
                } else {
                    MatchCountryContext match = new MatchCountryContext{
                        Countries = countries,
                        Context = context,
                        SearchTerm = searchTerm,
                        SelectedChunks = selectedChunks,
                        FileName = fileName,
                        CreatedAT = createdAT
                    };
                    await _context.MatchCountryContexts.AddAsync(match);
                    await _context.SaveChangesAsync();

                    return Ok(new RequestResponse {Message = "Added record."});
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("post-nationalities-match")]
        public async Task<IActionResult> PostMatchNationalityContext(string nationalities, string context, string searchTerm, string selectedChunks, string fileName, string createdAT){
            try
            {
                if (string.IsNullOrWhiteSpace(nationalities) || string.IsNullOrWhiteSpace(context) || string.IsNullOrWhiteSpace(searchTerm) || string.IsNullOrWhiteSpace(selectedChunks) || string.IsNullOrWhiteSpace(fileName) || string.IsNullOrWhiteSpace(createdAT))
                {
                    return NotFound(new RequestResponse {Message = "Input(s) not valid."});
                } else {
                    MatchNationalityContext match = new MatchNationalityContext{
                        Nationalities = nationalities,
                        Context = context,
                        SearchTerm = searchTerm,
                        SelectedChunks = selectedChunks,
                        FileName = fileName,
                        CreatedAT = createdAT
                    };
                    await _context.MatchNationalityContexts.AddAsync(match);
                    await _context.SaveChangesAsync();

                    return Ok(new RequestResponse {Message = "Added record."});
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("post-nonGpe-match")]
        public async Task<IActionResult> PostMatchNonGpeContext(string nonGpe, string context, string searchTerm, string selectedChunks, string fileName, string createdAT){
            try
            {
                if (string.IsNullOrWhiteSpace(nonGpe) || string.IsNullOrWhiteSpace(context) || string.IsNullOrWhiteSpace(searchTerm) || string.IsNullOrWhiteSpace(selectedChunks) || string.IsNullOrWhiteSpace(fileName) || string.IsNullOrWhiteSpace(createdAT))
                {
                    return NotFound(new RequestResponse {Message = "Input(s) not valid."});
                } else {
                    MatchNonGpeContext match = new MatchNonGpeContext{
                        NonGpe = nonGpe,
                        Context = context,
                        SearchTerm = searchTerm,
                        SelectedChunks = selectedChunks,
                        FileName = fileName,
                        CreatedAT = createdAT
                    };
                    await _context.MatchNonGpeContexts.AddAsync(match);
                    await _context.SaveChangesAsync();

                    return Ok(new RequestResponse {Message = "Added record."});
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("post-person-match")]
        public async Task<IActionResult> PostMatchPersonContext(string person, string context, string searchTerm, string selectedChunks, string fileName, string createdAT){
            try
            {
                if (string.IsNullOrWhiteSpace(person) || string.IsNullOrWhiteSpace(context) || string.IsNullOrWhiteSpace(searchTerm) || string.IsNullOrWhiteSpace(selectedChunks) || string.IsNullOrWhiteSpace(fileName) || string.IsNullOrWhiteSpace(createdAT))
                {
                    return NotFound(new RequestResponse {Message = "Input(s) not valid."});
                } else {
                    MatchPersonContext match = new MatchPersonContext{
                        Person = person,
                        Context = context,
                        SearchTerm = searchTerm,
                        SelectedChunks = selectedChunks,
                        FileName = fileName,
                        CreatedAT = createdAT
                    };
                    await _context.MatchPersonContexts.AddAsync(match);
                    await _context.SaveChangesAsync();

                    return Ok(new RequestResponse {Message = "Added record."});
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        
        
        [HttpGet("post-building")]
        public async Task<IActionResult> PostBuildingContext(string buildings, string context, string fileName, string createdAT){
            try
            {
                if (string.IsNullOrWhiteSpace(buildings) || string.IsNullOrWhiteSpace(context) || string.IsNullOrWhiteSpace(fileName) || string.IsNullOrWhiteSpace(createdAT))
                {
                    return NotFound(new RequestResponse {Message = "Input(s) not valid."});
                } else {
                    BuildingContext match = new BuildingContext{
                        Buildings = buildings,
                        Context = context,
                        FileName = fileName,
                        CreatedAT = createdAT
                    };
                    await _context.BuildingContexts.AddAsync(match);
                    await _context.SaveChangesAsync();

                    return Ok(new RequestResponse {Message = "Added record."});
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("post-company")]
        public async Task<IActionResult> PostCompanyContext(string companies, string context, string fileName, string createdAT){
            try
            {
                if (string.IsNullOrWhiteSpace(companies) || string.IsNullOrWhiteSpace(context) || string.IsNullOrWhiteSpace(fileName) || string.IsNullOrWhiteSpace(createdAT))
                {
                    return NotFound(new RequestResponse {Message = "Input(s) not valid."});
                } else {
                    CompanyContext match = new CompanyContext{
                        Companies = companies,
                        Context = context,
                        FileName = fileName,
                        CreatedAT = createdAT
                    };
                    await _context.CompanyContexts.AddAsync(match);
                    await _context.SaveChangesAsync();

                    return Ok(new RequestResponse {Message = "Added record."});
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("post-countries")]
        public async Task<IActionResult> PostCountryContext(string countries, string context, string fileName, string createdAT){
            try
            {
                if (string.IsNullOrWhiteSpace(countries) || string.IsNullOrWhiteSpace(context) || string.IsNullOrWhiteSpace(fileName) || string.IsNullOrWhiteSpace(createdAT))
                {
                    return NotFound(new RequestResponse {Message = "Input(s) not valid."});
                } else {
                    CountryContext match = new CountryContext{
                        Countries = countries,
                        Context = context,
                        FileName = fileName,
                        CreatedAT = createdAT
                    };
                    await _context.CountryContexts.AddAsync(match);
                    await _context.SaveChangesAsync();

                    return Ok(new RequestResponse {Message = "Added record."});
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("post-nationalities")]
        public async Task<IActionResult> PostNationalityContext(string nationalities, string context, string fileName, string createdAT){
            try
            {
                if (string.IsNullOrWhiteSpace(nationalities) || string.IsNullOrWhiteSpace(context) || string.IsNullOrWhiteSpace(fileName) || string.IsNullOrWhiteSpace(createdAT))
                {
                    return NotFound(new RequestResponse {Message = "Input(s) not valid."});
                } else {
                    NationalityContext match = new NationalityContext{
                        Nationalities = nationalities,
                        Context = context,
                        FileName = fileName,
                        CreatedAT = createdAT
                    };
                    await _context.NationalityContexts.AddAsync(match);
                    await _context.SaveChangesAsync();

                    return Ok(new RequestResponse {Message = "Added record."});
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("post-nonGpe")]
        public async Task<IActionResult> PostNonGpeContext(string nonGpe, string context, string fileName, string createdAT){
            try
            {
                if (string.IsNullOrWhiteSpace(nonGpe) || string.IsNullOrWhiteSpace(context) || string.IsNullOrWhiteSpace(fileName) || string.IsNullOrWhiteSpace(createdAT))
                {
                    return NotFound(new RequestResponse {Message = "Input(s) not valid."});
                } else {
                    NonGpeContext match = new NonGpeContext{
                        NonGpe = nonGpe,
                        Context = context,
                        FileName = fileName,
                        CreatedAT = createdAT
                    };
                    await _context.NonGpeContexts.AddAsync(match);
                    await _context.SaveChangesAsync();

                    return Ok(new RequestResponse {Message = "Added record."});
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("post-person")]
        public async Task<IActionResult> PostPersonContext(string person, string context, string fileName, string createdAT){
            try
            {
                if (string.IsNullOrWhiteSpace(person) || string.IsNullOrWhiteSpace(context) || string.IsNullOrWhiteSpace(fileName) || string.IsNullOrWhiteSpace(createdAT))
                {
                    return NotFound(new RequestResponse {Message = "Input(s) not valid."});
                } else {
                    PersonContext match = new PersonContext{
                        Person = person,
                        Context = context,
                        FileName = fileName,
                        CreatedAT = createdAT
                    };
                    await _context.PersonContexts.AddAsync(match);
                    await _context.SaveChangesAsync();

                    return Ok(new RequestResponse {Message = "Added record."});
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
    }
}