using Avalonia.Media;
using NeoBoxLauncher.Extensions.Notify;
using NeoBoxLauncher.Interfaces;

namespace NeoBoxLauncher.ViewModels.Dialogs;

public class AboutDialogViewModel(ISettingsService settingsService) : NotifyPropertyChangedBase {
    public DrawingImage AppIcon { get; private set; }

    private DrawingImage AppIconLight = null!;
    private DrawingImage AppIconDark = null!;

    public void Init(DrawingImage appIconLight, DrawingImage appIconDark) {
        AppIconLight = appIconLight;
        AppIconDark = appIconDark;

        ReloadTheme();
    }

    private void ReloadTheme() {
        AppIcon = settingsService.ThemeDark
            ? AppIconDark
            : AppIconLight;
        OnPropertyChanged(nameof(AppIcon));
    }
}