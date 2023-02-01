using Crypto_task.Core.Models;
using Crypto_task.Core.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace Crypto_task.ViewModels
{
    public class MarketsViewModel
    {
        public ObservableCollection<MarketModel> Source { get; } = new ObservableCollection<MarketModel>();

        public MarketsViewModel()
        {
        }

        public async Task LoadDataAsync(object currency, CancellationToken? token, int? limit)
        {
            try 
            {
                var responce = await HttpService.GetMarkets(App.httpClient, token, currency.ToString(), limit.HasValue ? limit.Value : 10);

                Source.Clear();
                foreach (var item in responce.Data)
                {
                    Source.Add(item);
                }
            }
            catch (OperationCanceledException)
            {
                
            }
        }
    }
}
