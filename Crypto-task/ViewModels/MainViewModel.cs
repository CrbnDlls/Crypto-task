using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Crypto_task.Helpers;
using Crypto_task.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void Initialize(Frame frame, WinUI.NavigationView navigationView)
        {
            this.frame = frame;
            this.navigationView = navigationView;
            frame.Navigate(typeof(CurrenciesPage));
        }

        public ICommand ItemInvokedCommand => itemInvocedCommand ?? (itemInvocedCommand = new RelayCommand<WinUI.NavigationViewItemInvokedEventArgs>(OnItemInvoked));

        public void OnItemInvoked(WinUI.NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                //NavigationService.Navigate(typeof(SettingsPage), null, args.RecommendedNavigationTransitionInfo);
                frame.Navigate(typeof(SettingsPage), null, args.RecommendedNavigationTransitionInfo);
            }
            else
            {
                var selectedItem = args.InvokedItemContainer as WinUI.NavigationViewItem;
                var pageType = selectedItem?.GetValue(NavHelper.NavigateToProperty) as Type;

                if (pageType != null)
                {
                    //NavigationService.Navigate(pageType, null, args.RecommendedNavigationTransitionInfo);
                    frame.Navigate(pageType, null, args.RecommendedNavigationTransitionInfo);
                }
            }
        }
    }
}
