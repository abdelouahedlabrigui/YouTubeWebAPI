using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YouTubeWebAPI.Controllers.Trading.AlpacaModels;
using YouTubeWebAPI.Models.Responses;
using YouTubeWebAPI.Repository;

namespace YouTubeWebAPI.Controllers.Trading
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlpacaController : ControllerBase
    {
        private readonly AlpacaService _alpacaService;

        public AlpacaController(AlpacaService alpacaService)
        {
            _alpacaService = alpacaService;
        }

        [HttpGet("account")]
        public async Task<IActionResult> GetAccountInfo(){
            var account = await _alpacaService.GetAccountInfoAsync();
            return Ok(account);
        }

        [HttpGet("positions")]
        public async Task<IActionResult> GetOpenPositions(){
            var positions = await _alpacaService.GetOpenPositionsAsync();
            return Ok(positions);
        }

        [HttpGet("assets")]
        public async Task<IActionResult> GetAssets([FromQuery] string? symbol = null){
            try
            {
                if (string.IsNullOrEmpty(symbol))
                {
                    return NotFound();
                }

                var alpacaAssets = await _alpacaService.GetAssetsAsync(symbol);
                var assets = alpacaAssets.Select(a => new AssetModel
                {
                    AssetId = a.AssetId.ToString(),  // Ensure valid GUID
                    Class = (int)a.Class,
                    Exchange = (int)a.Exchange,
                    Symbol = a.Symbol,
                    Name = a.Name,
                    Status = (int)a.Status,
                    IsTradable = a.IsTradable,
                    Marginable = a.Marginable,
                    Shortable = a.Shortable,
                    EasyToBorrow = a.EasyToBorrow,
                    Fractionable = a.Fractionable,
                    MinOrderSize = a.MinOrderSize,
                    MinTradeIncrement = a.MinTradeIncrement,
                    PriceIncrement = a.PriceIncrement,
                    MaintenanceMarginRequirement = a.MaintenanceMarginRequirement,
                    Attributes = a.Attributes?.Select(attr => (int)attr).ToList() ?? new List<int>()
                }).ToList();
                return Ok(assets);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new RequestResponse {Message = ex.Message});
            }
        }

        [HttpGet("assets/{assetId}/trades")]
        public async Task<IActionResult> GetRecentTrades(string assetId, [FromQuery] string start, [FromQuery] string end){
            try
            {
                if (!DateTime.TryParse(start, out var startDate) || !DateTime.TryParse(end, out var endDate))
                {
                    return BadRequest(new RequestResponse {Message = "Invalid date format. Use 'yyyy-MM-ddTHH:mm:ssZ'." });
                }

                var alpacaTrades = await _alpacaService.GetRecentTradesAsync(assetId, startDate, endDate);

                var trades = alpacaTrades.Select(t => new TradeModel
                {
                    TimestampUtc = t.TimestampUtc,
                    Exchange = t.Exchange,
                    Price = t.Price,
                    Size = t.Size,
                    TradeId = t.TradeId,
                    Tape = t.Tape,
                    Update = t.Update,
                    // ConditionsList = t.ConditionsList ?? new List<string>(),
                    TakerSide = (int)t.TakerSide,
                    Symbol = t.Symbol,
                    Conditions = t.Conditions ?? new List<string>()
                }).ToList();
                return Ok(trades);  
            }
            catch (System.Exception ex)
            {
                return NotFound(new RequestResponse {Message = ex.Message});
            }
        }
    }
}