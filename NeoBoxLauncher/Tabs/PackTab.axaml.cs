using System.Diagnostics;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using NeoBoxLauncher.Data.Common;
using NeoBoxLauncher.Dialogs;
using NeoBoxLauncher.Interfaces;
using NeoBoxLauncher.ViewModels.Tabs;

namespace NeoBoxLauncher.Tabs;

public partial class PackTab : UserControl, ITab {
    private readonly IPacksService PacksService;
    private readonly IDialogService DialogService;

    public PackTabViewModel ViewModel { get; }

    public PackTab(PackTabViewModel viewModel, IPacksService packsService, IDialogService dialogService) {
        InitializeComponent();
        DataContext = viewModel;

        PacksService = packsService;
        DialogService = dialogService;
        ViewModel = viewModel;
    }

    public void SetContext(Tab tab) {
        ViewModel.Init(tab);
    }

    private void Control_OnLoaded(object? sender, RoutedEventArgs e) {
        ViewModel.QueryView.Bind();
    }

    private void Control_OnUnloaded(object? sender, RoutedEventArgs e) {
        ViewModel.QueryView.UnBind();
    }

    private void PackDelete_OnClick(object? sender, RoutedEventArgs e) {
        DialogService.Show<PackDeleteDialog>(dialog => {
            dialog.InitDialog(ViewModel.Pack);
        });
    }

    private void PackShowDirectory_OnClick(object? sender, RoutedEventArgs e) {
        var pathDirectory = ViewModel.Pack.PathDirectory;
        if (pathDirectory == null) {
            return;
        }

        pathDirectory = Path.GetFullPath(pathDirectory);

        Process.Start(new ProcessStartInfo {
            FileName = pathDirectory,
            UseShellExecute = true,
            Verb = "open",
        });
    }

    private void PackLaunch_OnClick(object? sender, RoutedEventArgs e) {
        PacksService.LaunchPack(ViewModel.Pack);
    }

    private void Filter_OnClick(object? sender, RoutedEventArgs e) {
        DialogService.Show<GameQueryDialog>(dialog => {
            dialog.InitDialog();
        });
    }
}