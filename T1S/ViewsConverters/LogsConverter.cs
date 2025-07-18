using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace T1S.ViewsConverters
{
    public class LogsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Dictionary<object, SolidColorBrush> keyValuePairs = new Dictionary<object, SolidColorBrush>
            {
                { Models.LogLevel.INFO, Brushes.LimeGreen },
                { Models.LogLevel.WARN, Brushes.Orange },
                { Models.LogLevel.ERRO, Brushes.Red }
            };

            return keyValuePairs[value];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
