using NeoBoxLauncher.Data.Common;
using NeoBoxLauncher.Extensions.Notify;

namespace NeoBoxLauncher.ViewModels.Dialogs;

public class PackDeleteDialogViewModel : NotifyPropertyChangedBase {
    private Pack _Pack = new();
    private string _Prompt = "";

    public Pack Pack {
        get => _Pack;
        set => SetField(ref _Pack, value);
    }

    public string Prompt {
        get => _Prompt;
        set => SetField(ref _Prompt, value);
    }

    public void Init(Pack pack) {
        Pack = pack;
        Prompt = $"Удалить пак \"{Pack.Title}\"?";
    }
}