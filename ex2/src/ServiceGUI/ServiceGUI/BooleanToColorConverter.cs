using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ServiceGUI
{
    class BooleanToColorConverter : IValueConverter
    {
        private static Color DISCONNECTED_COLOR = Color.FromRgb(96, 96, 96);
        private static Color CONNECTED_COLOR = Color.FromRgb(153, 255, 255);
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Color))
            {
                Console.WriteLine(targetType.Name);
                throw new InvalidOperationException("Must convert to a color!");
            }

            bool isConnected = (bool)value;
            if (isConnected)
                return CONNECTED_COLOR;
            return DISCONNECTED_COLOR;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
