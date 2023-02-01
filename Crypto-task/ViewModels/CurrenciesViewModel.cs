using Crypto_task.Core.Models;
using Crypto_task.Core.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace Crypto_task.ViewModels
{
    public class CurrenciesViewModel
    {
        public ObservableCollection<CurrencyModel> Source { get; } = new ObservableCollection<CurrencyModel>();

        public CurrenciesViewModel()
        {
        }

        public async Task LoadDataAsync(CancellationToken? token, string search = "", int limit = 10)
        {

            try
            {

                var responce = await HttpService.GetCurrencies(App.httpClient, token, limit, search);

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
