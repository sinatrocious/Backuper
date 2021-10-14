using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Backuper
{
    public class DoubleToGridLengthConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => this;

        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
            value is double pixels ? new GridLength(pixels) : new GridLength();

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
            value is GridLength grid ? grid.Value : 0;
    }
}
