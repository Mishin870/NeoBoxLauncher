using NeoBoxLauncher.Data.Common;
using NeoBoxLauncher.Dialogs;
using NeoBoxLauncher.Interfaces;

namespace NeoBoxLauncher.Commands;

public class OpenGameCardCommand(IDialogService dialogService) : TypedCommand<Game> {
    protected override void Execute(Game parameter) {
        dialogService.Show<GameCardDialog>(dialog => {
            dialog.InitDialog(parameter);
        });
    }
}