using Avalonia.Controls;
using Avalonia.Media;
using NeoBoxLauncher.Interfaces;
using NeoBoxLauncher.ViewModels.Dialogs;

namespace NeoBoxLauncher.Dialogs;

public partial class AboutDialog : UserControl, IDialog {
    public AboutDialogViewModel ViewModel { get; }

    public AboutDialog(AboutDialogViewModel viewModel) {
        InitializeComponent();
        DataContext = viewModel;

        ViewModel = viewModel;
    }

    public void InitDialog() {
        ViewModel.Init((DrawingImage)Resources["AppIconDrawingImageLight"]!,
            (DrawingImage)Resources["AppIconDrawingImageDark"]!);
    }
}