using Crypto_task.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
            await ViewModel.LoadDataAsync(Id, null, null);
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

            ViewModel.LoadDataAsync(Id, token, limitNum);
        }
    }
}
