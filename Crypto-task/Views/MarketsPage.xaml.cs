using Crypto_task.ViewModels;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Crypto_task.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MarketsPage : Page
    {
        public MarketsViewModel ViewModel { get; } = new MarketsViewModel();
        private CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        private object Id;
        public MarketsPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Id = e.Parameter;
            await LoadData(null, null);
        }

        private void LimitTextBox_TextChanged(object sender, TextChangedEventArgs e)
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

            LoadData(token, limitNum);
        }

        private async Task LoadData(CancellationToken? token, int? limit)
        {
            try
            {
                await ViewModel.LoadDataAsync(Id, token, limit);
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
