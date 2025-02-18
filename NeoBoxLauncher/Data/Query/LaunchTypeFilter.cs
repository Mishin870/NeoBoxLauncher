using NeoBoxLauncher.Data.Common;
using NeoBoxLauncher.Data.Query.Base;
using NeoBoxLauncher.Interfaces;

namespace NeoBoxLauncher.Data.Query;

public class LaunchTypeFilter : EnumFilter<LaunchType> {
    public override bool Filter(Game game, ISettingsService settingsService) {
        return DoFilter(game, game.Type);
    }
}