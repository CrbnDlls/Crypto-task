using CommunityToolkit.Mvvm.ComponentModel;
using Crypto_task.Core.Models;
using Crypto_task.Core.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace Crypto_task.ViewModels
{
    public class CurrencyExchangeViewModel : ObservableObject
    {
        public ObservableCollection<CurrencyModel> From { get; } = new ObservableCollection<CurrencyModel>();

        public ObservableCollection<CurrencyModel> To { get; } = new ObservableCollection<CurrencyModel>();

        private List<CurrencyModel> currencies;

        private CurrencyModel currencyFrom, currencyTo;

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
                    CalculateResult(currencyFrom, currencyTo);
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
        }

        public void UpdateFromCollection(string search)
        {
            From.Clear();
            var serchResult = currencies.Where(x => x.Id.ToLowerInvariant().Contains(search.ToLowerInvariant()) || x.Symbol.ToLowerInvariant().Contains(search.ToLowerInvariant()));
            foreach (CurrencyModel currency in serchResult)
            {
                From.Add(currency);
            }
        }

        public void UpdateToCollection(string search)
        {
            To.Clear();
            var serchResult = currencies.Where(x => x.Id.ToLowerInvariant().Contains(search.ToLowerInvariant()) || x.Symbol.ToLowerInvariant().Contains(search.ToLowerInvariant()));
            foreach (CurrencyModel currency in serchResult)
            {
                To.Add(currency);
            }
        }

        private void CalculateResult(CurrencyModel from, CurrencyModel to)
        {
            ResourceLoader rL = new ResourceLoader();
            if (from is null)
            {
                Result = rL.GetString("CurrencyExchange_NoTrade");
                return;
            }
            
            if (!from.PriceUsd.HasValue)
            {
                Result = rL.GetString("CurrencyExchange_NoTradePrice");
                return;
            }
            
            if (to is null)
            {
                Result = rL.GetString("CurrencyExchange_NoPurchase");
                return;
            }
            
            if (!to.PriceUsd.HasValue)
            {
                Result = rL.GetString("CurrencyExchange_NoPurchasePrice");
                return;
            }

            decimal result = from.PriceUsd.Value * ammount / to.PriceUsd.Value;

            Result = $"{Ammount} {from.Name} = {result} {to.Name}";
        }

        public void SetResultString(object fromCurrency, object toCurrency)
        {
            currencyFrom = fromCurrency as CurrencyModel;
            currencyTo = toCurrency as CurrencyModel;
            CalculateResult(currencyFrom, currencyTo);
        }
    }
}
