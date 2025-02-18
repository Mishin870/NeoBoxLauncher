using Avalonia.Controls;
using NeoBoxLauncher.Interfaces;
using NeoBoxLauncher.ViewModels.Dialogs;

namespace NeoBoxLauncher.Dialogs;

public partial class QRCodeDialog : UserControl, IDialog {
    public QRCodeDialogViewModel ViewModel { get; }

    public QRCodeDialog(QRCodeDialogViewModel viewModel) {
        InitializeComponent();
        DataContext = viewModel;

        ViewModel = viewModel;
    }

    public void InitDialog(string data) {
        ViewModel.Init(data);
    }
}