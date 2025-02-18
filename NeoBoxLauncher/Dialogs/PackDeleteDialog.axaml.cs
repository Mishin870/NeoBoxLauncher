using Avalonia.Controls;
using Avalonia.Interactivity;
using NeoBoxLauncher.Data.Common;
using NeoBoxLauncher.Interfaces;
using NeoBoxLauncher.ViewModels;
using NeoBoxLauncher.ViewModels.Dialogs;

namespace NeoBoxLauncher.Dialogs;

public partial class PackDeleteDialog : UserControl, IDialog {
    private readonly ISettingsService SettingsService;
    private readonly IVisualService VisualService;
    private readonly MainViewModel MainViewModel;

    public PackDeleteDialogViewModel ViewModel { get; }

    public PackDeleteDialog(PackDeleteDialogViewModel viewModel, ISettingsService settingsService,
        IVisualService visualService, MainViewModel mainViewModel) {
        InitializeComponent();
        DataContext = viewModel;

        ViewModel = viewModel;

        SettingsService = settingsService;
        VisualService = visualService;
        MainViewModel = mainViewModel;
    }

    public void InitDialog(Pack pack) {
        ViewModel.Init(pack);
    }

    private void PackDeleteConfirm_OnClick(object? sender, RoutedEventArgs e) {
        SettingsService.RemovePack(ViewModel.Pack.PathFile);
        MainViewModel.RefreshTabs();
        VisualService.ShowSnackbar("Пак удалён");
        VisualService.CloseDialog();
    }
}