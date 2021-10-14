using Backuper;
using NUnit.Framework;
using System.Globalization;
using System.Windows;

namespace UnitTests
{
    public class FalseToHiddenConverterTests
    {
        [Test]
        [TestCase(true, ExpectedResult = Visibility.Visible)]
        [TestCase(false, ExpectedResult = Visibility.Hidden)]
        [TestCase("wrongtype", ExpectedResult = Visibility.Hidden)]
        public object? Convert_GivenValue_ReturnExpectedResult(object value)
        {
            var test = new FalseToHiddenConverter();
            return test.Convert(value, typeof(Visibility), null, CultureInfo.CurrentCulture);
        }

        [Test]
        [TestCase(Visibility.Visible, ExpectedResult = true)]
        [TestCase(Visibility.Hidden, ExpectedResult = false)]
        [TestCase(Visibility.Collapsed, ExpectedResult = false)]
        [TestCase("wrongtype", ExpectedResult = false)]
        public object? ConvertBack_GivenValue_ReturnExpectedResult(object value)
        {
            var test = new FalseToHiddenConverter();
            return test.ConvertBack(value, typeof(bool), null, CultureInfo.CurrentCulture);
        }
    }
}