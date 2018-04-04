using System;
using System.Globalization;
using System.Windows.Data;

namespace ComicOrders.WPF.Helpers.Converters {
    public class PrettyOrderDateConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value is DateTime)
                return ((DateTime)value).ToString("MMMM yyyy");
            else
                throw new ArgumentException("value must be a DateTime.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
