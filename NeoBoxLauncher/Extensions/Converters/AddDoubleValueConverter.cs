using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace NeoBoxLauncher.Extensions.Converters;

public class AddDoubleValueConverter : IValueConverter {
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
        if (value is double doubleValue && parameter is string doubleParameter) {
            return doubleValue + double.Parse(doubleParameter);
        }

        return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        throw new NotSupportedException();
    }
}