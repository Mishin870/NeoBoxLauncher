using NeoBoxLauncher.Data.Query;
using NeoBoxLauncher.Extensions.Notify;
using NeoBoxLauncher.Interfaces;

namespace NeoBoxLauncher.ViewModels.Tabs;

public class AllPacksTabViewModel(GameQueryView queryView) : NotifyPropertyChangedBase, ITabViewModel {
    private string _Title = "";

    public string Title {
        get => _Title;
        set => SetField(ref _Title, value);
    }

    public GameQueryView QueryView => queryView;
}