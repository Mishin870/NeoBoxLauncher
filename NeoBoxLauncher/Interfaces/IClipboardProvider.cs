using Avalonia.Input.Platform;

namespace NeoBoxLauncher.Interfaces;

public interface IClipboardProvider {
    IClipboard? GetClipboard();
}