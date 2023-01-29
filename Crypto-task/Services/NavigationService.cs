using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Crypto_task.Services
{
    public static class NavigationService
    {

        public static void Navigate(Frame currentFrame, Type pageType, object parameter = null, NavigationTransitionInfo infoOverride = null)
        {
            //if (pageType == null || !pageType.IsSubclassOf(typeof(Page)))
            //{
            //    throw new ArgumentException($"Invalid pageType '{pageType}', please provide a valid pageType.", nameof(pageType));
            //}

            // Don't open the same page multiple times
            if (currentFrame.Content?.GetType() != pageType)
            {
                currentFrame.Navigate(pageType, parameter, infoOverride);
            } 
        }

        public static bool GoBack(Frame frame)
        {
            if (frame.CanGoBack)
            {
                frame.GoBack();
                return true;
            }

            return false;
        }
    }
}
