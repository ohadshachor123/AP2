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
        // The converter between the log-status to its background color.
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Brush)) {
                throw new InvalidOperationException("Must convert to a color!");
            }

            LogEnum type = (LogEnum)value;
            if (type == LogEnum.ERROR) { //RED
                return new SolidColorBrush(Color.FromRgb(255,92,92));
            } else if (type == LogEnum.WARNING) { // YELLOW
                return new SolidColorBrush(Color.FromRgb(255, 251, 117));
            } else if (type == LogEnum.INFO) { // GREEN
                return new SolidColorBrush(Color.FromRgb(81, 232, 136));
            }
            throw new InvalidOperationException("The parameter given for the log enum does not exist!");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
