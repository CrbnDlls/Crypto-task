using Crypto_task.Services;
using Crypto_task.ViewModels;
using System;
using System.Threading.Tasks;
using System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Microsoft.Toolkit.Uwp.UI.Controls;
using Crypto_task.Core.Models;
using System.Net.Http;
using Windows.UI.Popups;
using Newtonsoft.Json;

namespace Crypto_task.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CurrenciesPage : Page
    {
        public CurrenciesViewModel ViewModel { get; } = new CurrenciesViewModel();
        private CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        public CurrenciesPage()
        {
            this.InitializeComponent();

        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            await LoadData(null);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            NavigateToDetailsPage(new object[] { button.Content, ViewModel.Source });
        }
        private void dataGrid_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            CurrencyModel model = grid.SelectedItem as CurrencyModel;
            NavigateToDetailsPage(new object[] { model.Id, ViewModel.Source });
        }

        private void NavigateToDetailsPage(object[] parameters)
        {
            NavigationService.Navigate(Frame, typeof(CurrencyDetailsPage), parameters);
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cancelTokenSource != null)
            {
                cancelTokenSource.Cancel();
            }
            cancelTokenSource = new CancellationTokenSource();

           PerformSearch(cancelTokenSource.Token);
        }

        private void PerformSearch(CancellationToken token)
        {
            if (!int.TryParse(LimitTextBox.Text, out int limitNum))
            {
                limitNum = 10;
            }
            
            LoadData(token, SearchTextBox.Text, limitNum);
        }

        private async Task LoadData(CancellationToken? token, string text = "", int limitNum = 10)
        {
            try 
            { 
                await ViewModel.LoadDataAsync(token, text, limitNum);
            }
            catch (HttpRequestException ex)
            {
                var messageDialog = new MessageDialog(ex.Message, "Http Error");

                await messageDialog.ShowAsync();
            }
            catch (JsonException ex)
            {
                var messageDialog = new MessageDialog(ex.Message, "Responce cannot be deserialized");

                await messageDialog.ShowAsync();
            }
        }
    }
}
