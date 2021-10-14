using NUnit.Framework;
using FluentAssertions;
using Backuper;
using System.ComponentModel;

namespace UnitTests
{
    public class NotifyPropertyChangedTests
    {
        [Test]
        public void OnPropertyChanged_WithArgument_EventRisedWithArgument()
        {
            var test = new NotifyPropertyChanged();
            using var monitoredTest = test.Monitor<INotifyPropertyChanged>();
            test.OnPropertyChanged("bla");
            monitoredTest.Should().Raise(nameof(test.PropertyChanged)).WithSender(test).
                WithArgs<PropertyChangedEventArgs>(o => o.PropertyName == "bla");
        }

        [Test]
        public void OnPropertyChanged_WithoutArgument_EventRisedWithMethodName()
        {
            var test = new NotifyPropertyChanged();
            using var monitoredTest = test.Monitor<INotifyPropertyChanged>();
            test.OnPropertyChanged();
            monitoredTest.Should().Raise(nameof(test.PropertyChanged)).WithSender(test).
                WithArgs<PropertyChangedEventArgs>(o => o.PropertyName == nameof(OnPropertyChanged_WithoutArgument_EventRisedWithMethodName));
        }

    }
}