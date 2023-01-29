using CommunityToolkit.Mvvm.ComponentModel;
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
    public class CurrenciesViewModel
    {
        public ObservableCollection<CurrencyModel> Source { get; } = new ObservableCollection<CurrencyModel>();

        public CurrenciesViewModel()
        {
        }

        public async Task LoadDataAsync()
        {
            Source.Clear();

            var responce = await HttpService.GetCurrencies(App.httpClient);

            foreach (var item in responce.Data)
            {
                Source.Add(item);
            }
        }
    }
}
