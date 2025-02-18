using NeoBoxLauncher.Extensions.Notify;

namespace NeoBoxLauncher.ViewModels.Dialogs;

public class ErrorDialogViewModel : NotifyPropertyChangedBase {
    private string _Text = "";

    public string Text {
        get => _Text;
        set => SetField(ref _Text, value);
    }
}