﻿using Crypto_task.Core.Models;
using Crypto_task.Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_task.ViewModels
{
    public class CurrencyDetailsViewModel
    {
        public ObservableCollection<CurrencyModel> Source { get; } = new ObservableCollection<CurrencyModel>();

        public CurrencyDetailsViewModel()
        {
            
        }

        public void LoadData(object parameter)
        {
            object[] objects= parameter as object[];
            Source.Clear();
            foreach (var item in (ObservableCollection<CurrencyModel>)objects[1]) 
            {
                if (item.Id == objects[0].ToString())
                {
                    Source.Add(item);
                }
            }

        }
    }
}
