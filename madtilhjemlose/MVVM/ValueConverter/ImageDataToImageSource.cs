using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace madtilhjemlose.MVVM.ValueConverter
{
    public class ImageDataToImageSource : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            byte[]? data = value is null ? null : (byte[]) value;
            return data is null ? null : ImageSource.FromStream(() => new BinaryData(data).ToStream());
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
