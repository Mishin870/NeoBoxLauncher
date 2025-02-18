using System;
using Microsoft.Extensions.DependencyInjection;
using NeoBoxLauncher.Interfaces;

namespace NeoBoxLauncher.Services;

public class DialogService(IServiceProvider serviceProvider, IVisualService visualService) : IDialogService {
    public void Show<T>(Action<T>? configureDialog = null) where T : IDialog {
        var dialog = serviceProvider.GetRequiredService<T>();
        configureDialog?.Invoke(dialog);

        visualService.ShowDialog(dialog);
    }
}