using CommunityToolkit.Mvvm.ComponentModel;
using Crypto_task.Models;
using Crypto_task.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.Globalization;
using Windows.UI.Xaml;

namespace Crypto_task.ViewModels
{
    public class SettingsViewModel : ObservableObject
    {
        private ElementTheme elementTheme = ThemeSelectorService.Theme;

        private ObservableCollection<AppLanguage> languages;

        private AppLanguage language;

        public event EventHandler LanguageChanged;

        public ObservableCollection<AppLanguage> Languages
        {
            get => languages;
            set { languages = value; }
        }

        public AppLanguage SelectedLanguage
        {
            get => language;
            set
            {
                if (language != value)
                {
                    SetProperty(ref language, value);
                    ApplicationLanguages.PrimaryLanguageOverride = value.Locale;
                    OnLanguageChanged();
                }
            }
        }

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
        private void OnLanguageChanged()
        {
            LanguageChanged?.Invoke(this, new EventArgs());
        }

        public SettingsViewModel()
        {
            languages = new ObservableCollection<AppLanguage> 
            {
                new AppLanguage { Name = "English", Locale = "en-US" },
                new AppLanguage { Name = "Русский", Locale = "ru-RU" }
            };

            language = languages.FirstOrDefault(x => x.Locale.Contains(ApplicationLanguages.PrimaryLanguageOverride));
        }

        public string[] Themes => GetThemes();

        public static string[] GetThemes()
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
