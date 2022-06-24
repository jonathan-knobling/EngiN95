using System;
using System.Windows.Input;

namespace Frontend.Core;

public class RelayCommand : ICommand
{
    private Action<object> _execute;
    private Func<object, bool>? _canExecute;
    
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public RelayCommand(Action<object> execute, Func<object, bool>? canExecute)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public bool CanExecute(object? parameter)
    {
        return parameter != null && (_canExecute == null || _canExecute(parameter));
    }

    public void Execute(object? parameter)
    {
        if (parameter != null) _execute(parameter);
    }
}