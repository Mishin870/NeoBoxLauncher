using Avalonia.Controls;
using Avalonia.Interactivity;
using NeoBoxLauncher.Data.Common;
using NeoBoxLauncher.Data.Query;
using NeoBoxLauncher.Dialogs;
using NeoBoxLauncher.Interfaces;
using NeoBoxLauncher.ViewModels.Tabs;

namespace NeoBoxLauncher.Tabs;

public partial class FavoriteGamesTab : UserControl, ITab {
    private readonly IPacksService PacksService;
    private readonly IDialogService DialogService;

    public FavoriteGamesTabViewModel ViewModel { get; }

    public FavoriteGamesTab(FavoriteGamesTabViewModel viewModel, IPacksService packsService, IDialogService dialogService) {
        PacksService = packsService;
        DialogService = dialogService;
        InitializeComponent();
        DataContext = viewModel;

        ViewModel = viewModel;
    }

    public void SetContext(Tab tab) {
        ViewModel.Title = "Избранное";
        ViewModel.QueryView.SetAllGames(PacksService.GetGames());
        ViewModel.QueryView.SetCustomFilter(new FavoriteFilter());
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