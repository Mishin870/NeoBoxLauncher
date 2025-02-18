using System;

namespace NeoBoxLauncher.Interfaces;

public interface IDialogService {
    void Show<T>(Action<T>? configureDialog = null) where T : IDialog;
}