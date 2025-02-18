using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace NeoBoxLauncher.Extensions.Converters;

public class ServerValueConverter : IValueConverter {
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
        if (value is string valueString && string.IsNullOrWhiteSpace(valueString)) {
            return "Оригинальный";
        }

        return value;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        throw new NotSupportedException();
    }
}