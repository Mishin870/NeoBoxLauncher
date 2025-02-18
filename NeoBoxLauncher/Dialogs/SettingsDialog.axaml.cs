using Avalonia.Controls;
using NeoBoxLauncher.Interfaces;
using NeoBoxLauncher.ViewModels.Dialogs;

namespace NeoBoxLauncher.Dialogs;

public partial class SettingsDialog : UserControl, IDialog {
    public SettingsDialogViewModel ViewModel { get; }

    public SettingsDialog(SettingsDialogViewModel viewModel) {
        InitializeComponent();
        DataContext = viewModel;

        ViewModel = viewModel;
    }
}