using System;
using System.Windows.Input;

namespace AvaloniaInside.SampleApp.ViewModels;

public class RelayCommand : ICommand
{
    private readonly Action? _execute;
    // Fields

    public RelayCommand(Action? execute)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
    }

    // Constructors

    public bool CanExecute(object? parameter)
    {
        var val = _execute != null;
        //CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        return val;
    }

    public event EventHandler? CanExecuteChanged;

    public void Execute(object? parameter)
    {
        _execute?.Invoke();
    }
    // ICommand Members

    public static RelayCommand Create(Action? execute)
    {
        return new RelayCommand(execute);
    }
    public static RelayCommand<T> Create<T>(Action<T>? execute)
    {
        return new RelayCommand<T>(execute);
    }
}