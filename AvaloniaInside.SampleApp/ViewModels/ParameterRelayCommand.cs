using System;
using System.Windows.Input;

namespace AvaloniaInside.SampleApp.ViewModels;

public class RelayCommand<T> : ICommand
{
    private readonly Action<T> _execute;
    // Fields

    public RelayCommand(Action<T> execute) {
        _execute = execute;
    }

    public bool CanExecute(object? parameter) {
        return parameter != null;
    }

    public void Execute(object? parameter) {
        _execute.Invoke((T)parameter!);
    }

    public event EventHandler? CanExecuteChanged;
}