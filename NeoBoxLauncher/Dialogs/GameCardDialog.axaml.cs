using Avalonia.Controls;
using NeoBoxLauncher.Data.Common;
using NeoBoxLauncher.Interfaces;
using NeoBoxLauncher.ViewModels.Dialogs;

namespace NeoBoxLauncher.Dialogs;

public partial class GameCardDialog : UserControl, IDialog {
    public GameCardDialogViewModel ViewModel { get; }

    public GameCardDialog(GameCardDialogViewModel viewModel) {
        InitializeComponent();
        DataContext = viewModel;

        ViewModel = viewModel;
    }

    public void InitDialog(Game game) {
        ViewModel.Init(game);
    }
}