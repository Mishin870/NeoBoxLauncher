using NeoBoxLauncher.Data.Common;
using NeoBoxLauncher.Data.Query;
using NeoBoxLauncher.Extensions.Notify;
using NeoBoxLauncher.Interfaces;

namespace NeoBoxLauncher.ViewModels.Tabs;

public class PackTabViewModel(
    IResourceService resourceService,
    IPacksService packsService,
    ISettingsService settingsService,
    GameQueryView queryView) : NotifyPropertyChangedBase, ITabViewModel {
    public IResourceService ResourceService => resourceService;
    public GameQueryView QueryView => queryView;
    public int LogoHeight => Pack.ImageCached.Exist ? settingsService.PackLogoHeight : 0;
    public bool CanLaunch => packsService.CanLaunchPack(Pack);

    private Pack _Pack = new();

    public Pack Pack {
        get => _Pack;
        set {
            SetField(ref _Pack, value);
            OnPropertyChanged(nameof(CanLaunch));
            OnPropertyChanged(nameof(LogoHeight));
        }
    }

    public void Init(Tab tab) {
        Pack = tab.Pack;
        QueryView.SetAllGames(Pack.Games);
        QueryView.SetCustomFilter();
    }
}