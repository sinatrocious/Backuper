using Backuper;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Windows.Input;

namespace UnitTests
{
    public class DelegateCommandTests
    {
        [Test]
        public void Update_EventRised()
        {
            var test = new DelegateCommand(o => { });
            using var monitoredTest = test.Monitor<ICommand>();
            test.Update();
            monitoredTest.Should().Raise(nameof(test.CanExecuteChanged)).WithSender(test).WithArgs<EventArgs>(o => o == EventArgs.Empty);
        }

        [Test]
        public void Execute_WithArgument_DelegateCalledWithArgument()
        {
            object? delegateArgument = null;
            var test = new DelegateCommand(o => delegateArgument = o);
            test.Execute("bla");
            delegateArgument.Should().Be("bla");
        }

        [Test]
        public void CanExecute_WithoutDelegate_ReturnTrue()
        {
            var test = new DelegateCommand(o => { });
            test.CanExecute(null).Should().BeTrue();
        }

        [Test]
        public void CanExecute_WithDelegateTrue_ReturnTrue()
        {
            var test = new DelegateCommand(o => { }, o => true);
            test.CanExecute(null).Should().BeTrue();
        }

        [Test]
        public void CanExecute_WithDelegateFalse_ReturnFalse()
        {
            var test = new DelegateCommand(o => { }, o => false);
            test.CanExecute(null).Should().BeFalse();
        }
    }
}