using System;
using System.IO;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NeoBoxLauncher.Commands;
using NeoBoxLauncher.Data.Query;
using NeoBoxLauncher.Dialogs;
using NeoBoxLauncher.Extensions;
using NeoBoxLauncher.Interfaces;
using NeoBoxLauncher.Services;
using NeoBoxLauncher.Tabs;
using NeoBoxLauncher.ViewModels;
using NeoBoxLauncher.ViewModels.Dialogs;
using NeoBoxLauncher.ViewModels.Tabs;

namespace NeoBoxLauncher;

public partial class App : Application {
    public static readonly string AssemblyName = typeof(ResourceService).Assembly.GetName().Name!;

    public IServiceProvider ServiceProvider { get; private set; } = null!;

    public IConfiguration Configuration { get; private set; } = null!;

    public override void Initialize() {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted() {
        //BindingPlugins.DataValidators.RemoveAt(0);

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, false);
        Configuration = builder.Build();

        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        ServiceProvider = serviceCollection.BuildServiceProvider();

        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
            desktop.MainWindow = mainWindow;
        } else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform) {
            singleViewPlatform.MainView = mainWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void ConfigureServices(IServiceCollection services) {
        services.AddLogging(builder => {
            builder.AddFile(Configuration.GetSection("FileLogging"));
        });

        // Config and settings
        services.AddConfig(Configuration.GetSection("App"));
        services.AddSingleton<ISettingsService, SettingsService>();
        services.AddSingleton<IAttributeService, AttributeService>();

        // GameQueryView
        services.AddSingleton<GameQuery>();
        services.AddTransient<GameQueryView>();

        // MainWindow
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MainWindow>();
        services.AddSingleton<IClipboardProvider>(x => x.GetRequiredService<MainViewModel>());
        services.AddSingleton<ILauncherProvider>(x => x.GetRequiredService<MainViewModel>());

        // Tabs
        services.AddTransient<SitesTab>();
        services.AddTransient<SitesTabViewModel>();
        services.AddTransient<PackTab>();
        services.AddTransient<PackTabViewModel>();
        services.AddTransient<FavoriteGamesTab>();
        services.AddTransient<FavoriteGamesTabViewModel>();
        services.AddTransient<AllPacksTab>();
        services.AddTransient<AllPacksTabViewModel>();

        // Dialogs
        services.AddTransient<AboutDialog>();
        services.AddTransient<AboutDialogViewModel>();
        services.AddTransient<ErrorDialog>();
        services.AddTransient<ErrorDialogViewModel>();
        services.AddTransient<GameCardDialog>();
        services.AddTransient<GameCardDialogViewModel>();
        services.AddTransient<GameQueryDialog>();
        services.AddTransient<GameQueryDialogViewModel>();
        services.AddTransient<PackDeleteDialog>();
        services.AddTransient<PackDeleteDialogViewModel>();
        services.AddTransient<QRCodeDialog>();
        services.AddTransient<QRCodeDialogViewModel>();
        services.AddTransient<SettingsDialog>();
        services.AddTransient<SettingsDialogViewModel>();

        // Services
        services.AddSingleton<IPacksService, PacksService>();
        services.AddSingleton<IResourceService, ResourceService>();
        services.AddTransient<ITabService, TabService>();
        services.AddTransient<IDialogService, DialogService>();
        services.AddTransient<IVisualService, VisualService>();

        // Commands
        services.AddTransient<CloseDialogCommand>();
        services.AddTransient<CopySiteCommand>();
        services.AddTransient<LaunchGameCommand>();
        services.AddTransient<LaunchSiteCommand>();
        services.AddTransient<LikeGameCommand>();
        services.AddTransient<OpenGameCardCommand>();
        services.AddTransient<QRSiteCommand>();
    }
}