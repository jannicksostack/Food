using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace madtilhjemlose.MVVM.ValueConverter
{
    class DateTimeToDateOnly : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is null)
            {
                return null;
            }

            DateTime date = (DateTime) value;
            return DateOnly.FromDateTime(date);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
