using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Crypto_task.Helpers
{
    public class ThemeValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is ElementTheme theme)
            {
                return theme.ToString();
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is string s)
            {
                return Enum.Parse(typeof(ElementTheme), s);
            }

            return null;
        }
    }
}
