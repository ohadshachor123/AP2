using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ServiceGUI.Logging
{
    public class LogEnumToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Brush)) {
                throw new InvalidOperationException("Must convert to a color!");
            }

            LogEnum type = (LogEnum)value;
            if (type == LogEnum.ERROR) {
                return Brushes.Red;
            } else if (type == LogEnum.WARNING) {
                return Brushes.Yellow;
            } else if (type == LogEnum.INFO) {
                return Brushes.Green;
            }
            throw new InvalidOperationException("The parameter given for the log enum does not exist!");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
