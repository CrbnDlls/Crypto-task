using Crypto_task.Core.Models;
using Crypto_task.Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_task.ViewModels
{
    public class MarketsViewModel
    {
        public ObservableCollection<MarketModel> Source { get; } = new ObservableCollection<MarketModel>();

        public MarketsViewModel()
        {
        }

        public async Task LoadDataAsync(object currency)
        {
            Source.Clear();

            var responce = await HttpService.GetMarkets(App.httpClient, currency.ToString());

            foreach (var item in responce.Data)
            {
                Source.Add(item);
            }
        }
    }
}
