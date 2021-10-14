using System;
using System.Windows.Input;

namespace Backuper
{
    public class DelegateCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        readonly Action<object?> _execute;
        readonly Func<object?, bool>? _canExecute;

        public DelegateCommand(Action<object?> executeDelegate) : this(executeDelegate, null) { }

        public DelegateCommand(Action<object?> executeDelegate, Func<object?, bool>? canExecute)
        {
            _execute = executeDelegate;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;

        public void Execute(object? parameter) => _execute?.Invoke(parameter);

        public void Update() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
