using Avalonia.Platform.Storage;

namespace NeoBoxLauncher.Interfaces;

public interface ILauncherProvider {
    ILauncher? GetLauncher();
}