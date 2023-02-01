using CommunityToolkit.Mvvm.ComponentModel;
using Crypto_task.Core.Models;
using Crypto_task.Core.Services;
using Crypto_task.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Globalization;
using Windows.Web.Http;

namespace Crypto_task.ViewModels
{
    public class CurrencyExchangeViewModel : ObservableObject
    {
        public ObservableCollection<CurrencyModel> From { get; } = new ObservableCollection<CurrencyModel>();

        public ObservableCollection<CurrencyModel> To { get; } = new ObservableCollection<CurrencyModel>();

        private List<CurrencyModel> currencies;

        private string result;
        
        private decimal ammount = 10;

        public string Ammount
        {
            get
            {
                return ammount.ToString();
            }
            set
            {
                if (decimal.TryParse(value.Replace(',', '.'), out decimal limitNum))
                {
                    ammount = limitNum;
                    SetResultString();
                }
                
            }
        }
        
        public string Result
        {
            get
            {
                return result;
            }
            set
            {
                SetProperty(ref result, value);
            }
        }

        public async Task LoadDataAsync()
        {
            var responce = await HttpService.GetCurrencies(App.httpClient, null, limit: 2000);

            currencies = responce.Data;

            foreach (var currency in currencies)
            {
                From.Add(currency);
                To.Add(currency);
            }
            SetResultString();
        }

        public void UpdateFromCollection(string search)
        {
            From.Clear();
            var serchResult = currencies.Where(x => x.Id.ToLowerInvariant().Contains(search.ToLowerInvariant()) || x.Symbol.ToLowerInvariant().Contains(search.ToLowerInvariant()));
            foreach (CurrencyModel currency in serchResult)
            {
                From.Add(currency);
            }
            SetResultString();
        }

        public void UpdateToCollection(string search)
        {
            To.Clear();
            var serchResult = currencies.Where(x => x.Id.ToLowerInvariant().Contains(search.ToLowerInvariant()) || x.Symbol.ToLowerInvariant().Contains(search.ToLowerInvariant()));
            foreach (CurrencyModel currency in serchResult)
            {
                To.Add(currency);
            }
            SetResultString();
        }

        private void SetResultString()
        {
            var fromCurrency = From.FirstOrDefault();
            if (fromCurrency is null)
            {
                Result = "Traded currency is not selected";
                return;
            }
            if (!fromCurrency.PriceUsd.HasValue)
            {
                Result = "Traded currency has no price";
                return;
            }
            var toCurrency = To.FirstOrDefault();
            if (string.IsNullOrEmpty(toCurrency.Id))
            {
                Result = "Purchased currency is not selected";
                return;
            }
            if (!toCurrency.PriceUsd.HasValue)
            {
                Result = "Purchased currency has no price";
                return;
            }

            decimal result = fromCurrency.PriceUsd.Value * ammount / toCurrency.PriceUsd.Value;

            Result = $"{Ammount} {fromCurrency.Name} = {result} {toCurrency.Name}";
        }
    }
}
