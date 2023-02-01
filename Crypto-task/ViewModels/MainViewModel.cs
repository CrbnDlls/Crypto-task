using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Crypto_task.Helpers;
using Crypto_task.Services;
using Crypto_task.Views;
using System;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using WinUI = Microsoft.UI.Xaml.Controls;

namespace Crypto_task.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private Frame frame;
        private WinUI.NavigationView navigationView;
        private ICommand itemInvocedCommand;
        private bool isBackEnabled;
        public bool IsBackEnabled
        {
            get { return isBackEnabled; }
            set { SetProperty(ref isBackEnabled, value); }
        }
        public void Initialize(Frame frame, WinUI.NavigationView navigationView)
        {
            this.frame = frame;
            this.navigationView = navigationView;
            frame.Navigate(typeof(CurrenciesPage));
            navigationView.BackRequested += NavigationView_BackRequested;
            frame.Navigated += Frame_Navigated;
        }

        public ICommand ItemInvokedCommand => itemInvocedCommand ?? (itemInvocedCommand = new RelayCommand<WinUI.NavigationViewItemInvokedEventArgs>(OnItemInvoked));

        public void OnItemInvoked(WinUI.NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                NavigationService.Navigate(frame, typeof(SettingsPage), null, args.RecommendedNavigationTransitionInfo);
            }
            else
            {
                var selectedItem = args.InvokedItemContainer as WinUI.NavigationViewItem;
                var pageType = selectedItem?.GetValue(NavHelper.NavigateToProperty) as Type;

                if (pageType != null)
                {
                    NavigationService.Navigate(frame, pageType, null, args.RecommendedNavigationTransitionInfo);
                }
            }
        }

        private void NavigationView_BackRequested(WinUI.NavigationView sender, WinUI.NavigationViewBackRequestedEventArgs args)
        {
            NavigationService.GoBack(frame);
        }

        private void Frame_Navigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            IsBackEnabled = frame.CanGoBack;
        }
    }
}
