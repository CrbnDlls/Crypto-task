using Crypto_task.Services;
using Crypto_task.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json.Linq;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

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

            await ViewModel.LoadDataAsync(null);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            NavigationService.Navigate(Frame, typeof(CurrencyDetailsPage), new object[] { button.Content, ViewModel.Source });
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
            
            ViewModel.LoadDataAsync(token, SearchTextBox.Text, limitNum);
        }


    }
}
