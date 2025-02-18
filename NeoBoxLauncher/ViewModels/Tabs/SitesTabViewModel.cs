using System.Collections.ObjectModel;
using NeoBoxLauncher.Commands;
using NeoBoxLauncher.Data.Common;
using NeoBoxLauncher.Extensions.Notify;
using NeoBoxLauncher.Interfaces;

namespace NeoBoxLauncher.ViewModels.Tabs;

public class SitesTabViewModel(LaunchGameCommand launchGameCommand) : NotifyPropertyChangedBase, ITabViewModel {
    public LaunchGameCommand LaunchGameCommand => launchGameCommand;

    private ObservableCollection<Site> _Sites = [];

    public ObservableCollection<Site> Sites {
        get => _Sites;
        set => SetField(ref _Sites, value);
    }
}