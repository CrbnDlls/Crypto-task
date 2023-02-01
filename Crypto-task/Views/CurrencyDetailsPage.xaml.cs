using Crypto_task.Services;
using Crypto_task.ViewModels;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Crypto_task.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CurrencyDetailsPage : Page
    {
        public CurrencyDetailsViewModel ViewModel { get; } = new CurrencyDetailsViewModel();
        public CurrencyDetailsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ViewModel.LoadData(e.Parameter);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            NavigationService.Navigate(Frame, typeof(MarketsPage), button.Name);
        }

        private async void Button_Click_Explorer(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            var uri = new Uri(button.Content.ToString());

            await Windows.System.Launcher.LaunchUriAsync(uri);
        }
    }
}
