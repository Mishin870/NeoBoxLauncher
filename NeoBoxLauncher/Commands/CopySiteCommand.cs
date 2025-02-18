using NeoBoxLauncher.Data.Common;
using NeoBoxLauncher.Interfaces;

namespace NeoBoxLauncher.Commands;

public class CopySiteCommand(IClipboardProvider clipboardProvider, IVisualService visualService) : TypedCommand<Site> {
    protected override async void Execute(Site parameter) {
        var clipboard = clipboardProvider.GetClipboard();

        if (clipboard != null) {
            await clipboard.SetTextAsync(parameter.Url);
            visualService.ShowSnackbar("Скопировано");
        }
    }
}