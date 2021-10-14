using Backuper;
using NUnit.Framework;
using System.Globalization;
using System.Windows;

namespace UnitTests
{
    public class WindowStateToNullableBoolConverterTests
    {
        [Test]
        [TestCase(true, ExpectedResult = WindowState.Maximized)]
        [TestCase(false, ExpectedResult = WindowState.Normal)]
        [TestCase(null, ExpectedResult = WindowState.Minimized)]
        [TestCase("wrongtype", ExpectedResult = WindowState.Minimized)]
        public object Convert_GivenValue_ReturnExpectedResult(object value)
        {
            var test = new WindowStateToNullableBoolConverter();
            return test.Convert(value, typeof(WindowState), null, CultureInfo.CurrentCulture);
        }

        [Test]
        [TestCase(WindowState.Maximized, ExpectedResult = true)]
        [TestCase(WindowState.Normal, ExpectedResult = false)]
        [TestCase(WindowState.Minimized, ExpectedResult = null)]
        [TestCase("wrongtype", ExpectedResult = null)]
        public object? ConvertBack_GivenValue_ReturnExpectedResult(object value)
        {
            var test = new WindowStateToNullableBoolConverter();
            return test.ConvertBack(value, typeof(bool?), null, CultureInfo.CurrentCulture);
        }
    }
}