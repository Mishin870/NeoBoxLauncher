using NeoBoxLauncher.Data.Common;
using NeoBoxLauncher.Interfaces;

namespace NeoBoxLauncher.Data.Query;

public interface IGameFilter {
    bool Filter(Game game, ISettingsService settingsService);
    void Reset();
}