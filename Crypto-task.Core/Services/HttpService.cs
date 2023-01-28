using Crypto_task.Core.Helpers;
using Crypto_task.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_task.Core.Services
{
    public static class HttpService
    {
        public static async Task<CoinCapResponce<CurrencyModel>> GetCurrencies(HttpClient client, int limit = 10, string search = "", string ids = "", int offset = 0)
        {
            var currencies = await GetData<CoinCapResponce<CurrencyModel>>(client, $@"https://api.coincap.io/v2/assets?limit={((limit <= 2000 && limit > 0) ? limit : 10)}&search={search}&ids={ids}&offset={offset}");

            return currencies;
        }
        private static async Task<T> GetData<T>(HttpClient client, string url)
        {
            var response = await client.GetStringAsync(url);

            T result = await JsonHelper.ToObjectAsync<T>(response);

            return result;
        }

    }
}
