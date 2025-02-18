using Avalonia.Controls;
using Avalonia.Interactivity;
using NeoBoxLauncher.Data.Common;
using NeoBoxLauncher.Dialogs;
using NeoBoxLauncher.Interfaces;
using NeoBoxLauncher.ViewModels.Tabs;

namespace NeoBoxLauncher.Tabs;

public partial class AllPacksTab : UserControl, ITab {
    private readonly IPacksService PacksService;
    private readonly IDialogService DialogService;

    public AllPacksTabViewModel ViewModel { get; }

    public AllPacksTab(AllPacksTabViewModel viewModel, IPacksService packsService, IDialogService dialogService) {
        PacksService = packsService;
        DialogService = dialogService;
        InitializeComponent();
        DataContext = viewModel;

        ViewModel = viewModel;
    }

    public void SetContext(Tab tab) {
        ViewModel.Title = "Все игры";
        ViewModel.QueryView.SetAllGames(PacksService.GetGames());
        ViewModel.QueryView.SetCustomFilter();
    }

    private void Control_OnLoaded(object? sender, RoutedEventArgs e) {
        ViewModel.QueryView.Bind();
    }

    private void Control_OnUnloaded(object? sender, RoutedEventArgs e) {
        ViewModel.QueryView.UnBind();
    }

    private void Filter_OnClick(object? sender, RoutedEventArgs e) {
        DialogService.Show<GameQueryDialog>(dialog => {
            dialog.InitDialog();
        });
    }
}