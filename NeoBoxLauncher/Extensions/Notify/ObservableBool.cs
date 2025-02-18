using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NeoBoxLauncher.Extensions.Notify;

public class ObservableBool(bool _Value = false) : INotifyPropertyChanged {
    public bool Value {
        get => _Value;
        set {
            if (value == _Value) {
                return;
            }

            _Value = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}