using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input.Platform;
using Avalonia.Platform.Storage;
using Material.Icons;
using Material.Styles.Themes;
using NeoBoxLauncher.Commands;
using NeoBoxLauncher.Data.Common;
using NeoBoxLauncher.Extensions.Notify;
using NeoBoxLauncher.Interfaces;
using NeoBoxLauncher.Tabs;

namespace NeoBoxLauncher.ViewModels;

public class MainViewModel(
    IPacksService packsService,
    ITabService tabService,
    ISettingsService settingsService,
    CloseDialogCommand closeDialogCommand) : NotifyPropertyChangedBase, IClipboardProvider, ILauncherProvider {
    public ISettingsService SettingsService => settingsService;
    public CloseDialogCommand CloseDialogCommand => closeDialogCommand;

    public ObservableCollection<Tab> Tabs { get; } = [];

    private TopLevel? _TopLevel;
    private Tab? _CurrentTab;
    private object? _CurrentTabContent;

    public TopLevel? TopLevel {
        get => _TopLevel;
        set => SetField(ref _TopLevel, value);
    }

    public Tab? CurrentTab {
        get => _CurrentTab;
        set {
            if (value != _CurrentTab) {
                OnCurrentTabUpdated(value);
            }

            SetField(ref _CurrentTab, value);
        }
    }

    public object? CurrentTabContent {
        get => _CurrentTabContent;
        set => SetField(ref _CurrentTabContent, value);
    }

    public void RefreshTabs() {
        Tabs.Clear();
        packsService.Reload();

        Tabs.Add(new Tab {
            Type = TabType.AllPacks,
            Title = "Все игры",
            Icon = MaterialIconKind.PackageVariant,
        });
        Tabs.Add(new Tab {
            Type = TabType.FavoriteGames,
            Title = "Избранное",
            Icon = MaterialIconKind.CardsHeart,
        });

        foreach (var pack in packsService.GetPacks()) {
            Tabs.Add(new Tab {
                Type = TabType.Pack,
                Title = pack.Title,
                Icon = MaterialIconKind.Apps,
                Pack = pack,
            });
        }

        Tabs.Add(new Tab {
            Type = TabType.Sites,
            Title = "Сайты",
            Icon = MaterialIconKind.Controller,
        });

        CurrentTab = Tabs[0];
    }

    private void OnCurrentTabUpdated(Tab? tab) {
        if (tab == null) {
            tab = null;
            return;
        }

        ITab tabInstance = tab.Type switch {
            TabType.Pack => tabService.Create<PackTab>(),
            TabType.AllPacks => tabService.Create<AllPacksTab>(),
            TabType.FavoriteGames => tabService.Create<FavoriteGamesTab>(),
            TabType.Sites => tabService.Create<SitesTab>(),
            _ => throw new ArgumentOutOfRangeException(),
        };

        tabInstance.SetContext(tab);
        CurrentTabContent = tabInstance;
    }

    public IClipboard? GetClipboard() {
        return _TopLevel?.Clipboard;
    }

    public ILauncher? GetLauncher() {
        return _TopLevel?.Launcher;
    }
}