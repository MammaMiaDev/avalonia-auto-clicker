using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace AutoClicker.Desktop.Converters;

public class RunningStatusConverter : IValueConverter
{
    public static RunningStatusConverter Instance { get; } = new();

    public object Convert(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture
    ) => bool.Parse(value?.ToString() ?? "false") ? "ON" : "OFF";

    public object ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture
    ) => throw new NotSupportedException();
}
