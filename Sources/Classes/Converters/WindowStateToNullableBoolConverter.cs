using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Backuper
{
    public class WindowStateToNullableBoolConverter : MarkupExtension, IValueConverter
    {
        public override WindowStateToNullableBoolConverter ProvideValue(IServiceProvider serviceProvider) => this;

        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) => value switch
        {
            true => WindowState.Maximized,
            false => WindowState.Normal,
            _ => WindowState.Minimized, // null or anything unexpected
        };

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => value switch
        {
            WindowState.Maximized => true,
            WindowState.Normal => false,
            _ => null // minimized or anything unexpected
        };
    }
}
