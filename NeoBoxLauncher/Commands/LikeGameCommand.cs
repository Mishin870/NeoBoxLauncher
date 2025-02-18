using NeoBoxLauncher.Data.Common;
using NeoBoxLauncher.Data.Query;
using NeoBoxLauncher.Interfaces;
using NeoBoxLauncher.ViewModels;

namespace NeoBoxLauncher.Commands;

public class LikeGameCommand(
    ISettingsService settingsService,
    MainViewModel mainViewModel,
    GameQuery gameQuery) : TypedCommand<Game> {
    protected override void Execute(Game parameter) {
        settingsService.ToggleFavorite(parameter);

        if (mainViewModel.CurrentTab?.Type == TabType.FavoriteGames) {
            gameQuery.Apply();
        }
    }
}