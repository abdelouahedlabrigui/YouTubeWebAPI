using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest;
using YouTubeWebAPI.Models;
using YouTubeWebAPI.Models.Documents;
using YouTubeWebAPI.Models.Documents.PdfDocuments;
using YouTubeWebAPI.Models.Responses;
using YouTubeWebAPI.Models.Trading.StockPrice;
using YouTubeWebAPI.Models.Trading.StockPrice.Indicators;

namespace YouTubeWebAPI.Controllers.Trading
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockPricesApiController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;

        public StockPricesApiController(AppDbContext context)
        {
            _context = context;
            _httpClient = new HttpClient(); 
        }

        private PdfRequestModel EncodePdf(string filePath){
            if (!System.IO.File.Exists(filePath))
            {
                throw new FileNotFoundException("PDF file not found.", filePath);
            }

            byte[] pdfBytes = System.IO.File.ReadAllBytes(filePath);
            string base64String = Convert.ToBase64String(pdfBytes);

            return new PdfRequestModel{
                FileName = Path.GetFileName(filePath),
                Base64Content = base64String
            };
        }
        [HttpPost("decode-pdf")]
        public IActionResult DecodePdf([FromBody] PdfRequestModel pdfRequest)
        {
            if (pdfRequest == null || string.IsNullOrEmpty(pdfRequest.FileName) || string.IsNullOrEmpty(pdfRequest.Base64Content))
            {
                return BadRequest(new RequestResponse { Message = "Invalid PDF content." });
            }

            try
            {
                byte[] pdfBytes = Convert.FromBase64String(pdfRequest.Base64Content);
                return File(pdfBytes, "application/pdf", pdfRequest.FileName);
            }
            catch (Exception ex)
            {
                return BadRequest(new RequestResponse { Message = $"Failed to decode PDF. {ex.Message}" });
            }
        }

        
        [HttpGet("get-volume-based-features")]
        public async Task<IActionResult> GetVolumeBasedFeatures(){
            try
            {
                var query = await _context.StockPricePdfDocuments.Take(7).OrderByDescending(o => o.Id).ToListAsync();
                if (query.Count == 0)
                {
                    return NoContent();
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("search-volume-based-features")]
        public async Task<IActionResult> SearchVolumeBasedFeatures(string search){
            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    return NotFound();
                }
                var query = await _context.StockPricePdfDocuments
                    .Where(w => w.Company.Contains($"{search}"))
                    .OrderByDescending(o => o.Id).ToListAsync();
                if (query.Count == 0)
                {
                    return NoContent();
                }
                return Ok(query);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("DeleteVolumeBasedFeatures")]
        public async Task<IActionResult> DeleteVolumeBasedFeatures(int? id){
            try
            {
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    return NotFound();
                }
                var query = await _context.StockPricePdfDocuments.FindAsync(id);                
                _context.StockPricePdfDocuments.Remove(query);
                await _context.SaveChangesAsync();
                return Ok(new RequestResponse {Message = "Deleted record."});
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }
        [HttpGet("get-volume-based-feature")]
        public async Task<IActionResult> GetVolumeBasedFeature(int? id){
            try
            {
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    return NotFound();
                }
                var query = await _context.StockPricePdfDocuments.Where(w => w.Id == id).ToListAsync();
                if (query.Count == 0)
                {
                    return NoContent();
                }
                return Ok(query);               
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }

        [HttpGet("generate-volume-based-features-pdf")]
        public async Task<IActionResult> GenerateVolumeBasedFeaturesPDF(string tickers, string start_date, string end_date, string filename, string options){
            try
            {
                if (!tickers.Contains(",") || !start_date.Contains("-") || !end_date.Contains("-") || !filename.Contains(".pdf"))
                {
                    return NotFound();
                }
                RequestResponse? requestResponse = new RequestResponse(); 
                string url = "";
                PdfRequestModel? pdfResponse = new PdfRequestModel();
                StockPricePdfDocument? pdfDocument;
                switch (options)
                {
                    case "Stock Volume vs. Moving Average":
                        url = $"http://127.0.0.1:5005/trading/plot_stock_volume_vs_moving_average?tickers={tickers}&start_date={start_date}&end_date={end_date}&filename={filename}";
                        requestResponse = await _httpClient.GetFromJsonAsync<RequestResponse>(url); 
                        if (requestResponse != null)
                        {
                            pdfResponse = EncodePdf(requestResponse.Message ?? string.Empty);
                        }  
                        if (!string.IsNullOrEmpty(pdfResponse.Base64Content))
                        {
                            pdfDocument =  new StockPricePdfDocument{
                                Company = tickers,
                                StartDate = start_date,
                                EndDate = end_date,
                                FileName = requestResponse?.Message,
                                Base64Content = pdfResponse.Base64Content,
                                CreatedAT = DateTime.Now.ToString()
                            };
                            await _context.StockPricePdfDocuments.AddAsync(pdfDocument);
                            await _context.SaveChangesAsync();

                            return Ok(new RequestResponse {Message = "Saved stock price pdf document to database."});
                        } else {
                            return Ok(new RequestResponse {Message = "Couldn't process request."});
                        }                     
                    case "Daily Volume Change (%)":
                        url = $"http://127.0.0.1:5005/trading/plot_daily_volume_change?tickers={tickers}&start_date={start_date}&end_date={end_date}&filename={filename}";
                        requestResponse = await _httpClient.GetFromJsonAsync<RequestResponse>(url);
                        if (requestResponse != null)
                        {
                            pdfResponse = EncodePdf(requestResponse.Message ?? string.Empty);
                        }  
                        if (!string.IsNullOrEmpty(pdfResponse.Base64Content))
                        {
                            pdfDocument =  new StockPricePdfDocument{
                                Company = tickers,
                                StartDate = start_date,
                                EndDate = end_date,
                                FileName = requestResponse?.Message,
                                Base64Content = pdfResponse.Base64Content,
                                CreatedAT = DateTime.Now.ToString()
                            };
                            await _context.StockPricePdfDocuments.AddAsync(pdfDocument);
                            await _context.SaveChangesAsync();

                            return Ok(new RequestResponse {Message = "Saved stock price pdf document to database."});
                        } else {
                            return Ok(new RequestResponse {Message = "Couldn't process request."});
                        } 
                    case "Stock Price vs. Volume Surges":
                        url = $"http://127.0.0.1:5005/trading/plot_volume_surges_vs_price?tickers={tickers}&start_date={start_date}&end_date={end_date}&filename={filename}";
                        requestResponse = await _httpClient.GetFromJsonAsync<RequestResponse>(url);
                        if (requestResponse != null)
                        {
                            pdfResponse = EncodePdf(requestResponse.Message ?? string.Empty);
                        }  
                        if (!string.IsNullOrEmpty(pdfResponse.Base64Content))
                        {
                            pdfDocument =  new StockPricePdfDocument{
                                Company = tickers,
                                StartDate = start_date,
                                EndDate = end_date,
                                FileName = requestResponse?.Message,
                                Base64Content = pdfResponse.Base64Content,
                                CreatedAT = DateTime.Now.ToString()
                            };
                            await _context.StockPricePdfDocuments.AddAsync(pdfDocument);
                            await _context.SaveChangesAsync();

                            return Ok(new RequestResponse {Message = "Saved stock price pdf document to database."});
                        } else {
                            return Ok(new RequestResponse {Message = "Couldn't process request."});
                        } 
                    case "RSI for Stocks":
                        url = $"http://127.0.0.1:5005/trading/plot_rsi?tickers={tickers}&start_date={start_date}&end_date={end_date}&filename={filename}";
                        requestResponse = await _httpClient.GetFromJsonAsync<RequestResponse>(url);
                        if (requestResponse != null)
                        {
                            pdfResponse = EncodePdf(requestResponse.Message ?? string.Empty);
                        }  
                        if (!string.IsNullOrEmpty(pdfResponse.Base64Content))
                        {
                            pdfDocument =  new StockPricePdfDocument{
                                Company = tickers,
                                StartDate = start_date,
                                EndDate = end_date,
                                FileName = requestResponse?.Message,
                                Base64Content = pdfResponse.Base64Content,
                                CreatedAT = DateTime.Now.ToString()
                            };
                            await _context.StockPricePdfDocuments.AddAsync(pdfDocument);
                            await _context.SaveChangesAsync();

                            return Ok(new RequestResponse {Message = "Saved stock price pdf document to database."});
                        } else {
                            return Ok(new RequestResponse {Message = "Couldn't process request."});
                        } 
                    case "MACD for Stocks":
                        url = $"http://127.0.0.1:5005/trading/plot_macd?tickers={tickers}&start_date={start_date}&end_date={end_date}&filename={filename}";
                        requestResponse = await _httpClient.GetFromJsonAsync<RequestResponse>(url);
                        if (requestResponse != null)
                        {
                            pdfResponse = EncodePdf(requestResponse.Message ?? string.Empty);
                        }  
                        if (!string.IsNullOrEmpty(pdfResponse.Base64Content))
                        {
                            pdfDocument =  new StockPricePdfDocument{
                                Company = tickers,
                                StartDate = start_date,
                                EndDate = end_date,
                                FileName = requestResponse?.Message,
                                Base64Content = pdfResponse.Base64Content,
                                CreatedAT = DateTime.Now.ToString()
                            };
                            await _context.StockPricePdfDocuments.AddAsync(pdfDocument);
                            await _context.SaveChangesAsync();

                            return Ok(new RequestResponse {Message = "Saved stock price pdf document to database."});
                        } else {
                            return Ok(new RequestResponse {Message = "Couldn't process request."});
                        } 
                    case "Volume Prediction":
                        url = $"http://127.0.0.1:5005/trading/plot_volume_prediction?tickers={tickers}&start_date={start_date}&end_date={end_date}&filename={filename}";
                        requestResponse = await _httpClient.GetFromJsonAsync<RequestResponse>(url);
                        if (requestResponse != null)
                        {
                            pdfResponse = EncodePdf(requestResponse.Message ?? string.Empty);
                        }  
                        if (!string.IsNullOrEmpty(pdfResponse.Base64Content))
                        {
                            pdfDocument =  new StockPricePdfDocument{
                                Company = tickers,
                                StartDate = start_date,
                                EndDate = end_date,
                                FileName = requestResponse?.Message,
                                Base64Content = pdfResponse.Base64Content,
                                CreatedAT = DateTime.Now.ToString()
                            };
                            await _context.StockPricePdfDocuments.AddAsync(pdfDocument);
                            await _context.SaveChangesAsync();

                            return Ok(new RequestResponse {Message = "Saved stock price pdf document to database."});
                        } else {
                            return Ok(new RequestResponse {Message = "Couldn't process request."});
                        } 
                    case "Prophet Volume Predictions for Multiple Stocks":
                        url = $"http://127.0.0.1:5005/trading/plot_prophet_volume_prediction_for_multiple_stocks?tickers={tickers}&start_date={start_date}&end_date={end_date}&filename={filename}";
                        requestResponse = await _httpClient.GetFromJsonAsync<RequestResponse>(url);
                        if (requestResponse != null)
                        {
                            pdfResponse = EncodePdf(requestResponse.Message ?? string.Empty);
                        }  
                        if (!string.IsNullOrEmpty(pdfResponse.Base64Content))
                        {
                            pdfDocument =  new StockPricePdfDocument{
                                Company = tickers,
                                StartDate = start_date,
                                EndDate = end_date,
                                FileName = requestResponse?.Message,
                                Base64Content = pdfResponse.Base64Content,
                                CreatedAT = DateTime.Now.ToString()
                            };
                            await _context.StockPricePdfDocuments.AddAsync(pdfDocument);
                            await _context.SaveChangesAsync();

                            return Ok(new RequestResponse {Message = "Saved stock price pdf document to database."});
                        } else {
                            return Ok(new RequestResponse {Message = "Couldn't process request."});
                        } 
                }
                return Ok(new RequestResponse {Message = "Couldn't process request."});
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse { Message = ex.Message });
            }
        }

        [HttpGet("DeleteStockPriceVisualization")]
        public async Task<IActionResult> DeleteStockPriceVisualization(int? id){
            try
            {
                var query = await _context.TechnicalIndicatorsVisualizations.FindAsync(id);
                _context.TechnicalIndicatorsVisualizations.Remove(query);
                await _context.SaveChangesAsync();
                return Ok(new RequestResponse {Message = "Deleted record."});
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse { Message = ex.Message });
            }
        }
        [HttpGet("search-stock-price-by-tickers-and-year")]
        public async Task<IActionResult> SearchStockPriceByTickers(string tickers, string year, string priceType){
            try
            {
                if (string.IsNullOrEmpty(tickers) || string.IsNullOrEmpty(year) || string.IsNullOrEmpty(priceType))
                {
                    return NotFound();                    
                }
                var result = await _context.TechnicalIndicatorsVisualizations
                    .Where(w => 
                        w.Ticker.Contains($"{tickers} | for comparison {priceType}") &&
                        w.StartDate.Contains($"{year}") &&
                        w.EndDate.Contains($"{year}"))
                        .ToListAsync();
                if (result.Count > 0)
                {
                    return Ok(result);
                } else {
                    return Ok(new RequestResponse {Message = "Requested resource not found."});
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse { Message = ex.Message });
            }
        }
        [HttpGet("get-stock-price-visualizations")]
        public async Task<IActionResult> GetVisualizedStockPrices(){
            try
            {
                var result = await _context.TechnicalIndicatorsVisualizations
                        .ToListAsync();
                if (result.Count > 0)
                {
                    return Ok(result);
                } else {
                    return Ok(new RequestResponse {Message = "Requested resource not found."});
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse { Message = ex.Message });
            }
        }

        [HttpGet("visualize-price-by-tickers")]
        public async Task<IActionResult> VisualizePriceByTickers(string tickers, string start_date, string end_date, string options){
            try
            {
                if (string.IsNullOrEmpty(tickers) || string.IsNullOrEmpty(start_date) || string.IsNullOrEmpty(end_date) || string.IsNullOrEmpty(options))
                {
                    return NotFound();
                }
                DbTechnicalIndicatorsVisualization visualization = new();
                var query = await _context.TechnicalIndicatorsVisualizations
                    .Where(w => w.Ticker.Contains($"{tickers} | for comparison") && w.StartDate == start_date && w.EndDate == end_date)
                    .ToListAsync();
                
                switch (options)
                {
                    case "Close":
                        visualization = await _httpClient.GetFromJsonAsync<DbTechnicalIndicatorsVisualization>(
                            $"http://127.0.0.1:5005/yahoo_finance/price_comparison?tickers={tickers}&start_date={start_date}&end_date={end_date}&options=Close");
                        if (query.Count == 0)
                        {
                            await _context.TechnicalIndicatorsVisualizations.AddAsync(visualization);
                            await _context.SaveChangesAsync();
                        }
                        return Ok(visualization);
                    case "High":
                        visualization = await _httpClient.GetFromJsonAsync<DbTechnicalIndicatorsVisualization>(
                            $"http://127.0.0.1:5005/yahoo_finance/price_comparison?tickers={tickers}&start_date={start_date}&end_date={end_date}&options=High");
                        if (query.Count == 0)
                        {
                            await _context.TechnicalIndicatorsVisualizations.AddAsync(visualization);
                            await _context.SaveChangesAsync();
                        }
                        return Ok(visualization);
                    case "Low":
                        visualization = await _httpClient.GetFromJsonAsync<DbTechnicalIndicatorsVisualization>(
                            $"http://127.0.0.1:5005/yahoo_finance/price_comparison?tickers={tickers}&start_date={start_date}&end_date={end_date}&options=Low");
                        if (query.Count == 0)
                        {
                            await _context.TechnicalIndicatorsVisualizations.AddAsync(visualization);
                            await _context.SaveChangesAsync();
                        }
                        return Ok(visualization);
                    case "Open":
                        visualization = await _httpClient.GetFromJsonAsync<DbTechnicalIndicatorsVisualization>(
                            $"http://127.0.0.1:5005/yahoo_finance/price_comparison?tickers={tickers}&start_date={start_date}&end_date={end_date}&options=Open");
                        if (query.Count == 0)
                        {
                            await _context.TechnicalIndicatorsVisualizations.AddAsync(visualization);
                            await _context.SaveChangesAsync();
                        }
                        return Ok(visualization);
                    case "Volume":
                        visualization = await _httpClient.GetFromJsonAsync<DbTechnicalIndicatorsVisualization>(
                            $"http://127.0.0.1:5005/yahoo_finance/price_comparison?tickers={tickers}&start_date={start_date}&end_date={end_date}&options=Volume");
                        if (query.Count == 0)
                        {
                            await _context.TechnicalIndicatorsVisualizations.AddAsync(visualization);
                            await _context.SaveChangesAsync();
                        }
                        return Ok(visualization);
                    default:
                        return Ok(new RequestResponse {Message = "Cannot Process Request."});
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }

        [HttpGet("summary-stats-using-sql-client")]
        public async Task<IActionResult> GetSummaryStatsUsingSqlClient(string ticker, string year){
            if (string.IsNullOrEmpty(ticker) || string.IsNullOrEmpty(year)) return NotFound();
            string? connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=youtube_db;Integrated Security=True;";
            string query = @$"
                WITH RankedData AS (
                    SELECT 
                        Ticker, Date, [Close], High, Low, [Open], Volume, SMA_20, EMA_20, RSI_14, MACD, MACD_Signal, Upper_BB,
                        Lower_BB, Stochastic_K, 
                        ROW_NUMBER() OVER (PARTITION BY Ticker ORDER BY Date) as rn
                    FROM TechnicalIndicators
                    WHERE Ticker = '{ticker}' AND Date LIKE '%{year}%'
                ),
                SummaryStats AS (
                    SELECT 
                        Ticker, CAST(AVG([Close])  AS DECIMAL(18, 2)) AS AvgClose, 
                        CAST(MIN([Close])  AS DECIMAL(18, 2)) AS MinClose, 
                        CAST(MAX([Close])  AS DECIMAL(18, 2)) AS MaxClose,
                        CAST(PERCENTILE_CONT(0.5) WITHIN GROUP (ORDER BY [Close]) OVER (PARTITION BY Ticker)  AS DECIMAL(18, 2)) AS MedianClose,
                        CAST(PERCENTILE_CONT(0.25) WITHIN GROUP (ORDER BY [Close]) OVER (PARTITION BY Ticker)  AS DECIMAL(18, 2)) AS Percentile25Close,
                        CAST(PERCENTILE_CONT(0.75) WITHIN GROUP (ORDER BY [Close]) OVER (PARTITION BY Ticker)  AS DECIMAL(18, 2)) AS Percentile75Close,
                        CAST(MIN(Volume)  AS DECIMAL(18, 2)) AS MinVolume, CAST(MAX(Volume)  AS DECIMAL(18, 2)) AS MaxVolume,
                        CAST(PERCENTILE_CONT(0.5) WITHIN GROUP (ORDER BY Volume) OVER (PARTITION BY Ticker)  AS DECIMAL(18, 2)) AS MedianVolume,
                        CAST(PERCENTILE_CONT(0.25) WITHIN GROUP (ORDER BY Volume) OVER (PARTITION BY Ticker)  AS DECIMAL(18, 2)) AS Percentile25Volume,
                        CAST(PERCENTILE_CONT(0.75) WITHIN GROUP (ORDER BY Volume) OVER (PARTITION BY Ticker)  AS DECIMAL(18, 2)) AS Percentile75Volume 
                    FROM RankedData
                    GROUP BY Ticker, [Close], Volume
                )
                SELECT * FROM SummaryStats ORDER BY Ticker;
            ";

            try
            {
                var results = new List<SummaryStatsModel>();
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                results.Add(new SummaryStatsModel
                                {
                                    Ticker = reader["Ticker"].ToString(),
                                    AvgClose = Convert.ToDecimal(reader["AvgClose"]),
                                    MinClose = Convert.ToDecimal(reader["MinClose"]),
                                    MaxClose = Convert.ToDecimal(reader["MaxClose"]),
                                    MedianClose = Convert.ToDecimal(reader["MedianClose"]),
                                    Percentile25Close = Convert.ToDecimal(reader["Percentile25Close"]),
                                    Percentile75Close = Convert.ToDecimal(reader["Percentile75Close"]),
                                    MinVolume = Convert.ToDecimal(reader["MinVolume"]),
                                    MaxVolume = Convert.ToDecimal(reader["MaxVolume"]),
                                    MedianVolume = Convert.ToDecimal(reader["MedianVolume"]),
                                    Percentile25Volume = Convert.ToDecimal(reader["Percentile25Volume"]),
                                    Percentile75Volume = Convert.ToDecimal(reader["Percentile75Volume"])
                                });
                            }
                        }
                    }
                }
                return Ok(results);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }

        [HttpGet("summary-stats-using-ef")]
        public IActionResult GetSummaryStats(){
            var rankedData = _context.TechnicalIndicators
                .AsEnumerable()
                .GroupBy(t => t.Ticker)
                .SelectMany(g => g
                    .OrderBy(t => t.Date)
                    .Select((item, index) => new {
                        item.Ticker, item.Date, item.Close, item.High, item.Low, item.Volume, item.SMA_20, item.EMA_20, item.RSI_14,
                        item.MACD, item.MACD_Signal, item.Upper_BB, item.Lower_BB, item.Stochastic_K, RowNumber = index + 1
                    }));
            var summaryStats = rankedData
                .GroupBy(r => r.Ticker)
                .Select(g => new {
                    Ticker = g.Key, AvgClose = g.Average(x => x.Close), MinClose = g.Min(x => x.Close), MaxClose = g.Max(x => x.Close),
                    MedianClose = GetPercentile(g.Select(x => x?.Close).OrderBy(x => x), 0.5),
                    Percentile25Close = GetPercentile(g.Select(x => x?.Close).OrderBy(x => x), 0.25),
                    Percentile75Close = GetPercentile(g.Select(x => x?.Close).OrderBy(x => x), 0.75),
                    MinVolume = g.Min(x => x.Volume), MaxVolume = g.Max(x => x.Volume),
                    MedianVolume = GetPercentile(g.Select(x => x?.Volume).OrderBy(x => x), 0.5),
                    Percentile25Volume = GetPercentile(g.Select(x => x?.Volume).OrderBy(x => x), 0.25),
                    Percentile75Volume = GetPercentile(g.Select(x => x?.Volume).OrderBy(x => x), 0.75),                    
                })
                .OrderBy(stats => stats.Ticker)
                .ToList();
            return Ok(summaryStats);
        }

        private static double GetPercentile(IOrderedEnumerable<double?> orderedData, double percentile){
            var data = orderedData.Where(x => x.HasValue).Select(x => x.Value).ToList();
            if (!data.Any()) return 0;

            var index = (data.Count - 1) * percentile;  
            var lowerIndex = (int)Math.Floor(index);
            var upperIndex = (int)Math.Ceiling(index);

            if(lowerIndex == percentile)
                return data[lowerIndex];

            return data[lowerIndex] + (data[upperIndex] - data[lowerIndex]) * (index - lowerIndex);
        }
        [HttpGet("get-saved-stock-price-technical-indicators")]
        public async Task<IActionResult> GetSavedStockPricesTechnicalIndicators(){
            try
            {
                var query = await _context.TechnicalIndicators
                    // .Where(w => w.Id == id)
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
                return BadRequest(new RequestResponse { Message = ex.Message });
            }
        }

        [HttpGet("get-stock-price-technical-indicators")]
        public async Task<IActionResult> GetStockPricesTechnicalIndicators(string tickers, string start_date, string end_date){
            try
            {
                if (string.IsNullOrEmpty(tickers) || string.IsNullOrEmpty(start_date) || string.IsNullOrEmpty(end_date))
                {
                    return BadRequest(new RequestResponse { Message = "One or many inputs are missing"});
                }
                if (!tickers.Contains(",") && !start_date.Contains("-") && !end_date.Contains("-"))
                {
                    return BadRequest(new RequestResponse {Message = "tickers param must have a comma and dates must have a dash."}); 
                } else {
                    var stockPricesTechnicalIndicatorsVisualization = await _httpClient.GetFromJsonAsync<List<DbTechnicalIndicatorsVisualization>>(
$"http://127.0.0.1:5005/yahoo_finance/technical_indicators?tickers={tickers}&start_date={start_date}&end_date={end_date}&options=visualization");
                    var stockPricesTechnicalIndicators = await _httpClient.GetFromJsonAsync<List<DbTechnicalIndicator>>(
$"http://127.0.0.1:5005/yahoo_finance/technical_indicators?tickers={tickers}&start_date={start_date}&end_date={end_date}&options=data");

                    if (stockPricesTechnicalIndicatorsVisualization == null || stockPricesTechnicalIndicators == null)
                    {
                        return BadRequest(new RequestResponse {Message = "An error occured while fetching data."});
                    } else {

                        await _context.TechnicalIndicatorsVisualizations.AddRangeAsync(stockPricesTechnicalIndicatorsVisualization);
                        await _context.SaveChangesAsync();
                        await _context.TechnicalIndicators.AddRangeAsync(stockPricesTechnicalIndicators);   
                        await _context.SaveChangesAsync();
                        return Ok(new RequestResponse { Message = "Data added successfully."});
                    }
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse { Message = ex.Message });
            }
        }

        [HttpGet("get-stock-price")]
        public async Task<IActionResult> GetStockPrice(int? id){
            try
            {
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    return NotFound();
                }

                var query = await _context.StockPrices
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
        [HttpGet("search-stock-prices")]
        public async Task<IActionResult> SearchStockPrice(string search, string startDate, string endDate)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    return NotFound();
                }

                var query = await _context.StockPrices
                    .Where(w => w.Company.Contains($"{search}"))
                    .Where(w => w.StartDate == startDate && w.EndDate == endDate)                    
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
        [HttpGet("get-stock-prices")]
        public async Task<IActionResult> GetStockPrice(){
            try
            {
                var query = await _context.StockPrices
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
        [HttpGet("DeleteStockPrice")]
        public async Task<IActionResult> DeleteStockPrice(int id){
            try
            {
                var query = await _context.StockPrices.Where(w => w.Id == id).ToListAsync();
                _context.StockPrices?.Remove(query[0]);
                await _context.SaveChangesAsync();
                return Ok(new RequestResponse {Message = $"Weather deleted successfully."});
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("delete-by-range-stock-price-arima-time-series-forecasting")]
        public async Task<IActionResult> DeleteTimeSeriesForecastingUsingArima(string company, string start_date, string end_date){
            try
            {
                if (string.IsNullOrEmpty(company) || string.IsNullOrEmpty(start_date) || string.IsNullOrEmpty(end_date))
                {
                    return BadRequest(new RequestResponse {Message = "Invalid parameters"}); 
                }
                var critical_value = await _context.StockPriceAdfTestCriticalValues
                    .Where(w => w.Ticker == company)
                    .Where(w => w.StartDate == start_date)
                    .Where(w => w.EndDate == end_date)
                    .ToListAsync();
                var adf_test = await _context.StockPriceAdfTestResults
                    .Where(w => w.Ticker == company)
                    .Where(w => w.StartDate == start_date)
                    .Where(w => w.EndDate == end_date)
                    .ToListAsync();
                var arima_coefficients = await _context.StockPriceArimaCoefficients
                    .Where(w => w.Ticker == company)
                    .Where(w => w.StartDate == start_date)
                    .Where(w => w.EndDate == end_date)
                    .ToListAsync();
                var arima_metrics = await _context.StockPriceArimaMetrics
                    .Where(w => w.Ticker == company)
                    .Where(w => w.StartDate == start_date)
                    .Where(w => w.EndDate == end_date)
                    .ToListAsync();
                var visualization = await _context.ArimaVisualizations
                    .Where(w => w.Ticker == company)
                    .Where(w => w.StartDate == start_date)
                    .Where(w => w.EndDate == end_date)
                    .ToListAsync();
                if (critical_value.Count > 0 && adf_test.Count > 0 && arima_coefficients.Count > 0 && arima_metrics.Count > 0 && visualization.Count > 0)
                {
                    _context.StockPriceAdfTestCriticalValues.RemoveRange(critical_value);
                    await _context.SaveChangesAsync();
                    _context.StockPriceAdfTestResults.RemoveRange(adf_test);
                    await _context.SaveChangesAsync();
                    _context.StockPriceArimaCoefficients.RemoveRange(arima_coefficients);
                    await _context.SaveChangesAsync();
                    _context.StockPriceArimaMetrics.RemoveRange(arima_metrics);
                    await _context.SaveChangesAsync();
                    _context.ArimaVisualizations.RemoveRange(visualization);
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
        [HttpGet("get-stock-price-arima-time-series-forecasting")]
        public async Task<IActionResult> GetTimeSeriesForecastingUsingArima(string company, string start_date, string end_date){
            try
            {
                if (string.IsNullOrEmpty(company) || string.IsNullOrEmpty(start_date) || string.IsNullOrEmpty(end_date))
                {
                    return BadRequest(new RequestResponse {Message = "Invalid parameters"}); 
                }
                var critical_value = await _context.StockPriceAdfTestCriticalValues
                    .Where(w => w.Ticker == company)
                    .Where(w => w.StartDate == start_date)
                    .Where(w => w.EndDate == end_date)
                    .ToListAsync();
                var adf_test = await _context.StockPriceAdfTestResults
                    .Where(w => w.Ticker == company)
                    .Where(w => w.StartDate == start_date)
                    .Where(w => w.EndDate == end_date)
                    .ToListAsync();
                var arima_coefficients = await _context.StockPriceArimaCoefficients
                    .Where(w => w.Ticker == company)
                    .Where(w => w.StartDate == start_date)
                    .Where(w => w.EndDate == end_date)
                    .ToListAsync();
                var arima_metrics = await _context.StockPriceArimaMetrics
                    .Where(w => w.Ticker == company)
                    .Where(w => w.StartDate == start_date)
                    .Where(w => w.EndDate == end_date)
                    .ToListAsync();
                var visualization = await _context.ArimaVisualizations
                    .Where(w => w.Ticker == company)
                    .Where(w => w.StartDate == start_date)
                    .Where(w => w.EndDate == end_date)
                    .ToListAsync();
                if (critical_value.Count > 0 && adf_test.Count > 0 && arima_coefficients.Count > 0 && arima_metrics.Count > 0 && visualization.Count > 0)
                {
                    StockPriceArimaTimeSeriesForcasting forcasting = new StockPriceArimaTimeSeriesForcasting{
                        ArimaVisualizationList = visualization,
                        StockPriceAdfTestCriticalValueList = critical_value,
                        StockPriceAdfTestResultList = adf_test,
                        StockPriceArimaCoefficientList = arima_coefficients,
                        StockPriceArimaMetricList = arima_metrics,
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

        [HttpGet("stock-price-time-series-forecasting-using-arima")]
        public async Task<IActionResult> TimeSeriesForecastingUsingArima(string company, string start_date, string end_date, string filename)
        {
            try
            {
                if (string.IsNullOrEmpty(company) || string.IsNullOrEmpty(start_date) || string.IsNullOrEmpty(end_date) || string.IsNullOrEmpty(filename))
                {
                    return BadRequest(new RequestResponse {Message = "Invalid parameters"});
                }
                
                List<StockPriceAdfTestCriticalValue> critical_value_1 = await _httpClient.GetFromJsonAsync<List<StockPriceAdfTestCriticalValue>>($"http://127.0.0.1:5005/yahoo_finance/adf_test?company={company}&start_date={start_date}&end_date={end_date}&filename={filename}&option=critical_value_1");
                List<StockPriceAdfTestCriticalValue> critical_value_2 = await _httpClient.GetFromJsonAsync<List<StockPriceAdfTestCriticalValue>>($"http://127.0.0.1:5005/yahoo_finance/adf_test?company={company}&start_date={start_date}&end_date={end_date}&filename={filename}&option=critical_value_2");
                
                StockPriceAdfTestResult adfTestResult_1 = await _httpClient.GetFromJsonAsync<StockPriceAdfTestResult>($"http://127.0.0.1:5005/yahoo_finance/adf_test?company={company}&start_date={start_date}&end_date={end_date}&filename={filename}&option=adfTestResult_1");
                StockPriceAdfTestResult adfTestResult_2 = await _httpClient.GetFromJsonAsync<StockPriceAdfTestResult>($"http://127.0.0.1:5005/yahoo_finance/adf_test?company={company}&start_date={start_date}&end_date={end_date}&filename={filename}&option=adfTestResult_2");

                List<StockPriceArimaCoefficient> coefficients = await _httpClient.GetFromJsonAsync<List<StockPriceArimaCoefficient>>($"http://127.0.0.1:5005/yahoo_finance/arima_model_summary?company={company}&start_date={start_date}&end_date={end_date}&filename={filename}&option=coef_df");

                StockPriceArimaMetric metrics = await _httpClient.GetFromJsonAsync<StockPriceArimaMetric>($"http://127.0.0.1:5005/yahoo_finance/arima_model_summary?company={company}&start_date={start_date}&end_date={end_date}&filename={filename}&option=metrics");   

                ArimaVisualization visualization = await _httpClient.GetFromJsonAsync<ArimaVisualization>($"http://127.0.0.1:5005/yahoo_finance/arima_visualization?company={company}&start_date={start_date}&end_date={end_date}&filename={filename}");

                if (critical_value_1.Count > 0 && critical_value_2.Count > 0 && adfTestResult_1 != null && adfTestResult_2 != null && visualization != null)
                {
                    await _context.StockPriceAdfTestCriticalValues.AddRangeAsync(critical_value_1);
                    await _context.SaveChangesAsync();
                    await _context.StockPriceAdfTestCriticalValues.AddRangeAsync(critical_value_2);
                    await _context.SaveChangesAsync();
                    await _context.StockPriceAdfTestResults.AddAsync(adfTestResult_1);
                    await _context.SaveChangesAsync();
                    await _context.StockPriceAdfTestResults.AddAsync(adfTestResult_2);
                    await _context.SaveChangesAsync();
                    await _context.StockPriceArimaCoefficients.AddRangeAsync(coefficients);
                    await _context.SaveChangesAsync();
                    await _context.StockPriceArimaMetrics.AddAsync(metrics);
                    await _context.SaveChangesAsync();
                    await _context.ArimaVisualizations.AddAsync(visualization);
                    await _context.SaveChangesAsync();
                    await _context.SaveChangesAsync();

                    return Ok(new RequestResponse {Message = $"Arima Time Series Forecasting completed for {new FileInfo(filename).Name}."});
                } else {
                    return Ok(new RequestResponse {Message = $"Couldn't proceed with file: {new FileInfo(filename).Name}."});
                }                            
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = $"Error: {ex.Message}"});
            }
        }

        [HttpGet("download-stock-price")]
        public async Task<IActionResult> DownloadStockPrice(string company, string start_date, string end_date, string filename)
        {
            try
            {
                if (string.IsNullOrEmpty(company) || string.IsNullOrEmpty(start_date) || string.IsNullOrEmpty(end_date) || string.IsNullOrEmpty(filename))
                {
                    return BadRequest(new RequestResponse {Message = "Invalid parameters"});
                }
                var check = await _context.StockPrices
                    .Where(w => w.Company == company)
                    .Where(w => w.StartDate == start_date)
                    .Where(w => w.EndDate == end_date)
                    .Where(w => w.Filename == filename)
                    .ToListAsync();
                
                if(!System.IO.File.Exists($"{filename}") && check.Count == 0){
                    RequestResponse response = await _httpClient.GetFromJsonAsync<RequestResponse>($"http://127.0.0.1:5005/yahoo_finance/download_dataset?company={company}&start_date={start_date}&end_date={end_date}&filename={filename}");
                    if (System.IO.File.Exists($"{filename}"))
                    {
                        StockPrice stockPrice = new StockPrice{
                            Company = company,
                            StartDate = start_date,
                            EndDate = end_date,
                            Filename = filename,
                            CreatedAT = DateTime.Now.ToString(),
                        };

                        await _context.StockPrices.AddAsync(stockPrice);
                        await _context.SaveChangesAsync();  
                        return Ok(new RequestResponse {Message = response.Message});
                    } else {
                        return BadRequest(new RequestResponse {Message = response.Message});
                    }
                } else {
                    return BadRequest(new RequestResponse {Message = "File or data already exists"});
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = $"Error: {ex.Message}"});
            }
        }
    }
}