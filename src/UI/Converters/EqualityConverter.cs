// This EqualityConverter class comes from Tyrrrz's YoutubeDownloader project
// Find more info here => https://github.com/Tyrrrz/YoutubeDownloader

using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;

namespace AutoClicker.Desktop.Converters;

public class EqualityConverter(bool isInverted) : IValueConverter
{
    public static EqualityConverter Equality { get; } = new(false);

    public object? Convert(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture
    ) => EqualityComparer<object>.Default.Equals(value, parameter) != isInverted;

    public object ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture
    ) => throw new NotSupportedException();
}
