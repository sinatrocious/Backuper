using Backuper;
using FluentAssertions;
using NUnit.Framework;
using System.Globalization;
using System.Windows;

namespace UnitTests
{
    public class DoubleToGridLengthConverterTests
    {
        [Test]
        public void Convert_Double123_ReturnGridLength123()
        {
            var test = new DoubleToGridLengthConverter();
            var result = test.Convert((double)123, typeof(GridLength), null, CultureInfo.CurrentCulture);
            result.Should().Be(new GridLength(123));
        }

        [Test]
        public void Convert_Int123_ReturnGridLengthAuto()
        {
            var test = new DoubleToGridLengthConverter();
            var result = test.Convert(123, typeof(GridLength), null, CultureInfo.CurrentCulture);
            result.Should().Be(GridLength.Auto);
        }

        [Test]
        public void Convert_Null_ReturnGridLengthAuto()
        {
            var test = new DoubleToGridLengthConverter();
            var result = test.Convert(null, typeof(GridLength), null, CultureInfo.CurrentCulture);
            result.Should().Be(GridLength.Auto);
        }

        [Test]
        public void ConvertBack_GridLength123_ReturnDouble123()
        {
            var test = new DoubleToGridLengthConverter();
            var result = test.ConvertBack(new GridLength(123), typeof(double), null, CultureInfo.CurrentCulture);
            result.Should().Be((double)123);
        }

        [Test]
        public void ConvertBack_GridLengthAuto_ReturnDouble1()
        {
            var test = new DoubleToGridLengthConverter();
            var result = test.ConvertBack(GridLength.Auto, typeof(double), null, CultureInfo.CurrentCulture);
            result.Should().Be((double)1);
        }

        [Test]
        public void ConvertBack_String_ReturnDouble0()
        {
            var test = new DoubleToGridLengthConverter();
            var result = test.ConvertBack("bla", typeof(double), null, CultureInfo.CurrentCulture);
            result.Should().Be((double)0);
        }

        [Test]
        public void ConvertBack_Null_ReturnDouble0()
        {
            var test = new DoubleToGridLengthConverter();
            var result = test.ConvertBack(null, typeof(double), null, CultureInfo.CurrentCulture);
            result.Should().Be((double)0);
        }
    }
}