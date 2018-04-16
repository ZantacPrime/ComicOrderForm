using System;
using System.Globalization;
using System.Windows.Data;

namespace ComicOrders.WPF.Helpers.Converters {
    public class PrettyCostConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(!(value is decimal) || value is null)
                return "";
            //throw new ArgumentException("This converter only works with decimal types.", nameof(value));

            return ((decimal)value).ToString("C");

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            var val = value as string;
            if(val == null)
                return "";

            var numVal = System.Text.RegularExpressions.Regex.Replace(val, @"[^\d\.]", "");
            if(decimal.TryParse(numVal, out var cost)) return cost;
            return new decimal(0);
        }
    }
}
