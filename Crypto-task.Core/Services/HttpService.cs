using Crypto_task.Core.Helpers;
using Crypto_task.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Networking.Vpn;

namespace Crypto_task.Core.Services
{
    public static class HttpService
    {
        public static async Task<CoinCapResponce<MarketModel>> GetMarkets(HttpClient client, CancellationToken? token, string id, int limit = 10, int offset = 0)
        {
            var markets = await GetData<CoinCapResponce<MarketModel>>(client, token, $@"https://api.coincap.io/v2/assets/{id}/markets?limit={((limit <= 2000 && limit > 0) ? limit : 10)}&offset={offset}");

            return markets;
        }
        public static async Task<CoinCapResponce<CurrencyModel>> GetCurrencies(HttpClient client, CancellationToken? token, int limit = 10, string search = "", string ids = "", int offset = 0)
        {
            var currencies = await GetData<CoinCapResponce<CurrencyModel>>(client, token, $@"https://api.coincap.io/v2/assets?limit={((limit <= 2000 && limit > 0) ? limit : 10)}&search={search}&ids={ids}&offset={offset}");

            return currencies;
        }
        private static async Task<T> GetData<T>(HttpClient client, CancellationToken? token, string url)
        {
            if (token.HasValue)
            {
                await Task.Delay(500);
                if (token.Value.IsCancellationRequested)
                {
                    token.Value.ThrowIfCancellationRequested();
                }
            }
           
            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Request returned error code {response.StatusCode}.");
            }

            T result = await JsonHelper.ToObjectAsync<T>(await response.Content.ReadAsStringAsync());

            return result;
        }

    }
}
