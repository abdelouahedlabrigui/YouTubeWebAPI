using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YouTubeWebAPI.Models;
using YouTubeWebAPI.Models.Responses;
using YouTubeWebAPI.Models.Trading.StockPrice.SARIMA;

namespace YouTubeWebAPI.Controllers.Trading.SARIMA
{
    [ApiController]
    [Route("api/[controller]")]
    public class SarimaApiController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly AppDbContext _context;

        public SarimaApiController(AppDbContext context)
        {
            _httpClient = new HttpClient();
            _context = context;
        }
        [HttpGet("delete-by-range-stock-price-sarima-time-series-forecasting")]
        public async Task<IActionResult> DeleteTimeSeriesForecastingUsingArima(string company, string start_date, string end_date){
            try
            {
                if (string.IsNullOrEmpty(company) || string.IsNullOrEmpty(start_date) || string.IsNullOrEmpty(end_date))
                {
                    return BadRequest(new RequestResponse {Message = "Invalid parameters"}); 
                }
                var coef_df = await _context.StockPriceSarimaCoefficients
                    .Where(w => w.Ticker == company)
                    .Where(w => w.StartDate == start_date)
                    .Where(w => w.EndDate == end_date)
                    .ToListAsync();
                var metrics = await _context.SarimaMetrics
                    .Where(w => w.Ticker == company)
                    .Where(w => w.StartDate == start_date)
                    .Where(w => w.EndDate == end_date)
                    .ToListAsync();
                var visualization = await _context.SarimaVisualizations
                    .Where(w => w.Ticker == company)
                    .Where(w => w.StartDate == start_date)
                    .Where(w => w.EndDate == end_date)
                    .ToListAsync();
                if (coef_df.Count > 0 && metrics.Count > 0 && visualization.Count > 0)
                {
                    _context.StockPriceSarimaCoefficients.RemoveRange(coef_df);
                    await _context.SaveChangesAsync();
                    _context.SarimaMetrics.RemoveRange(metrics);
                    await _context.SaveChangesAsync();
                    _context.SarimaVisualizations.RemoveRange(visualization);
                    await _context.SaveChangesAsync();
                    await _context.SaveChangesAsync();

                    return Ok(new RequestResponse {Message = $"Data deleted successfully."});
                } else {
                    return Ok(new RequestResponse {Message = "Data isn't available."});
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = $"Error: {ex.Message}"});
            }
        }
        [HttpGet("get-stock-price-sarima-time-series-forecasting")]
        public async Task<IActionResult> GetTimeSeriesForecastingUsingSarima(string company, string start_date, string end_date){
            try
            {
                if (string.IsNullOrEmpty(company) || string.IsNullOrEmpty(start_date) || string.IsNullOrEmpty(end_date))
                {
                    return BadRequest(new RequestResponse {Message = "Invalid parameters"}); 
                }
                var coef_df = await _context.StockPriceSarimaCoefficients
                    .Where(w => w.Ticker == company)
                    .Where(w => w.StartDate == start_date)
                    .Where(w => w.EndDate == end_date)
                    .ToListAsync();
                var metrics = await _context.SarimaMetrics
                    .Where(w => w.Ticker == company)
                    .Where(w => w.StartDate == start_date)
                    .Where(w => w.EndDate == end_date)
                    .ToListAsync();
                var visualization = await _context.SarimaVisualizations
                    .Where(w => w.Ticker == company)
                    .Where(w => w.StartDate == start_date)
                    .Where(w => w.EndDate == end_date)
                    .ToListAsync();
                if (coef_df.Count > 0 && metrics.Count > 0 && visualization.Count > 0)
                {
                    SarimaTimeSeriesForcasting forcasting = new SarimaTimeSeriesForcasting{
                        StockPriceSarimaCoefficientsList = coef_df,
                        SarimaMetricsList = metrics,
                        SarimaVisualizationsList = visualization,
                    };
                    return Ok(forcasting);
                } else {
                    return Ok(new RequestResponse {Message = "Data isn't available."});
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = $"Error: {ex.Message}"});
            }
        }

        [HttpGet("stock-price-time-series-forecasting-using-sarima")]
        public async Task<IActionResult> TimeSeriesForecastingUsingSarima(string company, string start_date, string end_date, string filename)
        {
            try
            {
                if (string.IsNullOrEmpty(company) || string.IsNullOrEmpty(start_date) || string.IsNullOrEmpty(end_date) || string.IsNullOrEmpty(filename))
                {
                    return BadRequest(new RequestResponse {Message = "Invalid parameters"});
                }
                
                SarimaMetric metrics = await _httpClient.GetFromJsonAsync<SarimaMetric>($"http://127.0.0.1:5005/yahoo_finance/sarima_model_summary?company={company}&start_date={start_date}&end_date={end_date}&filename={filename}&option=metrics");                

                List<StockPriceSarimaCoefficient> coef_df = await _httpClient.GetFromJsonAsync<List<StockPriceSarimaCoefficient>>($"http://127.0.0.1:5005/yahoo_finance/sarima_model_summary?company={company}&start_date={start_date}&end_date={end_date}&filename={filename}&option=coef_df");

                SarimaVisualization visualization = await _httpClient.GetFromJsonAsync<SarimaVisualization>($"http://127.0.0.1:5005/yahoo_finance/sarima_visualization?company={company}&start_date={start_date}&end_date={end_date}&filename={filename}");

                if (coef_df.Count > 0 && metrics != null && visualization != null)
                {
                    await _context.StockPriceSarimaCoefficients.AddRangeAsync(coef_df);
                    await _context.SaveChangesAsync();
                    await _context.SarimaMetrics.AddRangeAsync(metrics);
                    await _context.SaveChangesAsync();
                    await _context.SarimaVisualizations.AddAsync(visualization);
                    await _context.SaveChangesAsync();
                    await _context.SaveChangesAsync();

                    return Ok(new RequestResponse {Message = $"Sarima Time Series Seasonality Forecasting completed for {new FileInfo(filename).Name}."});
                } else {
                    return Ok(new RequestResponse {Message = $"Couldn't proceed with file: {new FileInfo(filename).Name}."});
                }                            
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = $"Error: {ex.Message}"});
            }
        }
    }
}