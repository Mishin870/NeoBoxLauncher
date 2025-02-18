using NeoBoxLauncher.Data.Common;
using NeoBoxLauncher.Interfaces;

namespace NeoBoxLauncher.Commands;

public class LaunchGameCommand(IPacksService packsService) : TypedCommand<Game> {
    protected override void Execute(Game parameter) {
        packsService.LaunchGame(parameter);
    }
}