using Crypto_task.ViewModels;
using System;
using Windows.ApplicationModel.Resources;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Crypto_task.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsViewModel ViewModel { get; } = new SettingsViewModel();

        public SettingsPage()
        {
            this.InitializeComponent();
            ViewModel.LanguageChanged += ViewModel_LanguageChanged;
        }

        private async void ViewModel_LanguageChanged(object sender, EventArgs e)
        {
            ResourceLoader rL = new ResourceLoader();
            
            var messageDialog = new MessageDialog(rL.GetString("Settings_LanguageChangedMessage"), rL.GetString("Settings_LanguageChangedHeader"));

            

            await messageDialog.ShowAsync();
        }
    }
}
