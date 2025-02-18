using System;
using System.ComponentModel;
using System.IO;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Material.Styles.Themes;
using Material.Styles.Themes.Base;
using NeoBoxLauncher.Extensions;
using NeoBoxLauncher.Extensions.Notify;
using NeoBoxLauncher.Interfaces;

namespace NeoBoxLauncher.Services;

public class ResourceService : NotifyPropertyChangedBase, IResourceService {
    private static MaterialTheme CurrentTheme => Application.Current!.LocateMaterialTheme<MaterialTheme>();
    private readonly ISettingsService SettingsService;

    private readonly Bitmap BitmapNotFound;
    private readonly Bitmap BitmapBackDark;
    private readonly Bitmap BitmapBackLight;
    private readonly Bitmap BitmapFadeDark;
    private readonly Bitmap BitmapFadeLight;

    public Bitmap BitmapFade { get; private set; } = null!;
    public Bitmap BitmapBack { get; private set; } = null!;

    public ResourceService(ISettingsService settingsService) {
        SettingsService = settingsService;

        BitmapNotFound = LoadBitmap("Images/not_found.png");
        BitmapBackDark = LoadBitmap("Images/back_dark.png");
        BitmapBackLight = LoadBitmap("Images/back_light.png");
        BitmapFadeDark = LoadBitmap("Images/fade_dark.png");
        BitmapFadeLight = LoadBitmap("Images/fade_light.png");

        SettingsService.PropertyChanged += OnSettingsPropertyChanged;
        ReloadTheme();
    }

    private void OnSettingsPropertyChanged(object? sender, PropertyChangedEventArgs e) {
        if (e.PropertyName == nameof(ISettingsService.ThemeDark)) {
            ReloadTheme();
        }
    }

    private void ReloadTheme() {
        var isDark = SettingsService.ThemeDark;

        CurrentTheme.BaseTheme = isDark ? BaseThemeMode.Dark : BaseThemeMode.Light;
        BitmapBack = isDark ? BitmapBackDark : BitmapBackLight;
        BitmapFade = isDark ? BitmapFadeDark : BitmapFadeLight;

        OnPropertyChanged(nameof(BitmapBack));
        OnPropertyChanged(nameof(BitmapFade));
    }

    public Stream Load(string path) {
        return AssetLoader.Open(new Uri($"avares://{App.AssemblyName}/{path}"));
    }

    public Bitmap LoadBitmap(string path) {
        return new Bitmap(Load(path));
    }

    public LazyBitmap LoadLazyBitmap(string? path = null) {
        return new LazyBitmap(BitmapNotFound, path);
    }
}