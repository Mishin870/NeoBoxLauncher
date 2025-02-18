using NeoBoxLauncher.Data.Common;
using NeoBoxLauncher.Interfaces;

namespace NeoBoxLauncher.Data.Query;

public class FavoriteFilter : IGameFilter {
    public bool Filter(Game game, ISettingsService settingsService) {
        return settingsService.FavoriteGames.Contains(game.Id);
    }

    public void Reset() {
    }
}