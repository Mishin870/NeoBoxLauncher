using Avalonia.Controls;
using Avalonia.Interactivity;
using NeoBoxLauncher.Interfaces;
using NeoBoxLauncher.ViewModels.Dialogs;

namespace NeoBoxLauncher.Dialogs;

public partial class GameQueryDialog : UserControl, IClosingListener, IDialog {
    private readonly IVisualService VisualService;

    public GameQueryDialogViewModel ViewModel { get; }

    public GameQueryDialog(GameQueryDialogViewModel viewModel, IVisualService visualService) {
        VisualService = visualService;
        InitializeComponent();
        DataContext = viewModel;

        ViewModel = viewModel;
    }

    public void InitDialog() {
        ViewModel.Init();
    }

    public void OnClosing() {
        ViewModel.Query.Apply();
    }

    private void Reset_OnClick(object? sender, RoutedEventArgs e) {
        ViewModel.Query.Reset();
        VisualService.CloseDialog();
    }

    private void TagsAll_OnClick(object? sender, RoutedEventArgs e) {
        ViewModel.Query.TagsFilter.SetAll(true);
    }

    private void TagsNothing_OnClick(object? sender, RoutedEventArgs e) {
        ViewModel.Query.TagsFilter.SetAll(false);
    }

    private void TranslatorsAll_OnClick(object? sender, RoutedEventArgs e) {
        ViewModel.Query.TranslatorsFilter.SetAll(true);
    }

    private void TranslatorsNothing_OnClick(object? sender, RoutedEventArgs e) {
        ViewModel.Query.TranslatorsFilter.SetAll(false);
    }

    private void GameTypesAll_OnClick(object? sender, RoutedEventArgs e) {
        ViewModel.Query.LaunchTypeFilter.SetAll(true);
    }

    private void GameTypesNothing_OnClick(object? sender, RoutedEventArgs e) {
        ViewModel.Query.LaunchTypeFilter.SetAll(false);
    }
}