using Avalonia.Controls;
using Avalonia.Interactivity;
using NeoBoxLauncher.Interfaces;
using NeoBoxLauncher.ViewModels.Dialogs;

namespace NeoBoxLauncher.Dialogs;

public partial class ErrorDialog : UserControl, IDialog {
    private readonly IVisualService VisualService;
    private readonly IClipboardProvider ClipboardProvider;

    public ErrorDialogViewModel ViewModel { get; }

    public ErrorDialog(ErrorDialogViewModel viewModel, IVisualService visualService, IClipboardProvider clipboardProvider) {
        InitializeComponent();
        DataContext = viewModel;

        ViewModel = viewModel;

        VisualService = visualService;
        ClipboardProvider = clipboardProvider;
    }

    public void InitDialog(string text) {
        ViewModel.Text = text;
    }

    private async void Copy_OnClick(object? sender, RoutedEventArgs e) {
        var clipboard = ClipboardProvider.GetClipboard();
        if (clipboard != null) {
            await clipboard.SetTextAsync(ViewModel.Text);
        }

        VisualService.ShowSnackbar("Скопировано");
    }
}