using System;
using System.Windows.Input;

namespace NeoBoxLauncher.Commands;

public abstract class TypedCommand<T> : ICommand {
    public bool CanExecute(object? parameter) {
        return parameter is T;
    }

    public void Execute(object? parameter) {
        if (parameter is T item) {
            Execute(item);
        }
    }

    protected abstract void Execute(T parameter);

    public event EventHandler? CanExecuteChanged;
}