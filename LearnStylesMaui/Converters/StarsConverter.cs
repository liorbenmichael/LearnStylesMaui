
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LearnStylesMaui.Converters
{
    internal class StarsConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            int stars = value is int r ? r : 0;
            if (stars <= 0) return "☆☆☆☆☆";
            if (stars == 1) return "★☆☆☆☆";
            if (stars == 2) return "★★☆☆☆";
            if (stars == 3) return "★★★☆☆";
            if (stars == 4) return "★★★★☆";
            return "★★★★★";
        }
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
