using System;

namespace NeoBoxLauncher.Interfaces;

public interface IVisualService {
    void ShowDialog(object content);
    void CloseDialog();
    void ShowError(string text);
    void ShowSnackbar(object content, TimeSpan? duration = null);
}