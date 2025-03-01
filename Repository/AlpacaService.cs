using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alpaca.Markets;

namespace YouTubeWebAPI.Repository
{
    public class AlpacaService
    {
        private readonly IAlpacaTradingClient _tradingClient;
        private readonly IAlpacaDataClient _dataClient;

        public AlpacaService(IAlpacaTradingClient tradingClient, IAlpacaDataClient dataClient)
        {
            _tradingClient = tradingClient;
            _dataClient = dataClient;
        }

        public async Task<IReadOnlyList<IAsset>> GetAssetsAsync(string? symbol = null){
            var assets = await _tradingClient.ListAssetsAsync(new AssetsRequest());
            if (!string.IsNullOrEmpty(symbol))
            {
                assets = assets.Where(a => a.Symbol.Equals(symbol, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            return assets;
        }

        public async Task<IEnumerable<ITrade>> GetRecentTradesAsync(string assetId, DateTime startDate, DateTime endDate){
            try
            {
                var dateRange = new Interval<DateTime>(startDate, endDate); 
                var request = new HistoricalTradesRequest(assetId, dateRange);
                var trades = await _dataClient.ListHistoricalTradesAsync(request);
                return trades.Items;
            }
            catch (RestClientErrorException ex)
            {
                throw new Exception($"Error fetching trades for asset {assetId}: {ex.Message}");
            }
        }

        public async Task<IAccount> GetAccountInfoAsync(){
            return await _tradingClient.GetAccountAsync();
        }
        public async Task<IReadOnlyList<IPosition>> GetOpenPositionsAsync(){
            return await _tradingClient.ListPositionsAsync();
        }
    }
}