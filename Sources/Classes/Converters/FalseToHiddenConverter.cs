using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Backuper
{
    public class FalseToHiddenConverter : MarkupExtension, IValueConverter
    {
        public override FalseToHiddenConverter ProvideValue(IServiceProvider serviceProvider) => this;

        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
            value is bool boolean && boolean ? Visibility.Visible : Visibility.Hidden;

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
            value is Visibility visibility && visibility == Visibility.Visible;
    }
}
