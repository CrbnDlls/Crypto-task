using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Crypto_task.Helpers;
using Crypto_task.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel;
using Windows.UI;
using Windows.UI.Xaml;

namespace Crypto_task.ViewModels
{
    public class SettingsViewModel : ObservableObject
    {
        private ElementTheme elementTheme = ThemeSelectorService.Theme;
        
        
        public ElementTheme ElementTheme
        {
            get { return elementTheme; }

            set
            {
                if (elementTheme != value)
                {
                    SetProperty(ref elementTheme, value);
                    ThemeSelectorService.SetThemeAsync(elementTheme);
                }
            }
        }

        public SettingsViewModel()
        {
        }

        public string[] Strings => GetStrings();

        public static string[] GetStrings()
        {
            List<string> list = new List<string>();

            foreach (ElementTheme theme in Enum.GetValues(typeof(ElementTheme)))
            {
                list.Add(theme.ToString());
            }


            return list.ToArray();
        }
    }
}
