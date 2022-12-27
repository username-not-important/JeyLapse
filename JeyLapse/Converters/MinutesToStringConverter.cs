using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace JeyLapse.Converters
{
    public class MinutesToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double min = (double)value;

            if (min == 0)
                return "Unlimited";
            else if (min < 60)
                return (int)min + " Minutes";
            else
            {
                int h = (int)(min/60);
                int m = (int)(min)%60;

                return h + " Hours" + ((m != 0) ? " & " + m + " Minutes" : "");
            }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
