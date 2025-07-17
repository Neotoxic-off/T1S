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
    public class EntropyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double entropy)
            {
                if (entropy >= 7.5)
                    return Brushes.IndianRed;
                if (entropy >= 6.0)
                    return Brushes.Gold;
                return Brushes.LightGreen;
            }

            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
