﻿using Crypto_task.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Crypto_task.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CurrencyExchange : Page
    {
        CurrencyExchangeViewModel ViewModel = new CurrencyExchangeViewModel();
        public CurrencyExchange()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            await LoadData();
            dataGridFrom.SelectedIndex = 0;
            dataGridTo.SelectedIndex = 0;
        }

        private async Task LoadData()
        {
            try
            {
                await ViewModel.LoadDataAsync();
            }
            catch (HttpRequestException ex)
            {
                var messageDialog = new MessageDialog(ex.Message, "Http Error");

                await messageDialog.ShowAsync();
            }
            catch (JsonException ex)
            {
                var messageDialog = new MessageDialog(ex.Message, "Response cannot be deserialized");

                await messageDialog.ShowAsync();
            }
        }

        private void FromTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModel.UpdateFromCollection(FromTextBox.Text);
            dataGridFrom.SelectedIndex = 0;
        }

        private void ToTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModel.UpdateToCollection(ToTextBox.Text);
            dataGridTo.SelectedIndex = 0;
        }
    }
}
