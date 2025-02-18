using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using DialogHostAvalonia;
using NeoBoxLauncher.Dialogs;
using NeoBoxLauncher.Interfaces;
using NeoBoxLauncher.ViewModels;
using SettingsDialog=NeoBoxLauncher.Dialogs.SettingsDialog;

namespace NeoBoxLauncher;

public partial class MainWindow : Window {
    private readonly MainViewModel ViewModel;
    private readonly IPacksService PacksService;
    private readonly IDialogService DialogService;

    public MainWindow(MainViewModel viewModel, IPacksService packsService, IDialogService dialogService) {
        InitializeComponent();

        viewModel.TopLevel = GetTopLevel(this);
        DataContext = viewModel;

        ViewModel = viewModel;
        PacksService = packsService;
        DialogService = dialogService;
    }

    private void Window_OnOpened(object? sender, EventArgs e) {
        ViewModel.RefreshTabs();
    }

    private void About_OnClick(object? sender, RoutedEventArgs e) {
        DialogService.Show<AboutDialog>(dialog => {
            dialog.InitDialog();
        });
    }

    private void Settings_OnClick(object? sender, RoutedEventArgs e) {
        DialogService.Show<SettingsDialog>();
    }

    private async void AddPack_OnTapped(object? sender, TappedEventArgs e) {
        var topLevel = GetTopLevel(this);
        if (topLevel == null) {
            return;
        }

        await PacksService.ShowAddPackDialogAsync(topLevel);
        ViewModel.RefreshTabs();
    }

    private void DialogHost_OnDialogClosing(object? sender, DialogClosingEventArgs e) {
        if (MainDialogHost.DialogContent is IClosingListener closingListener) {
            closingListener.OnClosing();
        }
    }
}