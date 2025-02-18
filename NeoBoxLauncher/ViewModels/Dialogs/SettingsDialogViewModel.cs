using NeoBoxLauncher.Data.Common;
using NeoBoxLauncher.Extensions.Notify;
using NeoBoxLauncher.Interfaces;

namespace NeoBoxLauncher.ViewModels.Dialogs;

public class SettingsDialogViewModel(ISettingsService settingsService, Config config) : NotifyPropertyChangedBase {
    public ISettingsService SettingsService => settingsService;
    public Config Config => config;
}