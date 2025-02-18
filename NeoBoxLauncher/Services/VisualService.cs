using System;
using Avalonia.Threading;
using DialogHostAvalonia;
using Material.Styles.Controls;
using Material.Styles.Models;
using NeoBoxLauncher.Interfaces;

namespace NeoBoxLauncher.Services;

public class VisualService : IVisualService {
    private const string DialogHostMain = "MainDialogHost";
    private const string SnackbarRoot = "Root";

    public void ShowDialog(object content) {
        DialogHost.Show(content, DialogHostMain);
    }

    public void CloseDialog() {
        DialogHost.Close(DialogHostMain);
    }

    public void ShowError(string text) {
        ShowDialog(text);
    }

    public void ShowSnackbar(object content, TimeSpan? duration = null) {
        SnackbarHost.Post(new SnackbarModel(content, duration ?? TimeSpan.FromSeconds(3)),
            SnackbarRoot,
            DispatcherPriority.Default);
    }

    public void RefreshTabs() {
        throw new NotImplementedException();
    }
}