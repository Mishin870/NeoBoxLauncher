using NeoBoxLauncher.Data.Common;
using NeoBoxLauncher.Dialogs;
using NeoBoxLauncher.Interfaces;

namespace NeoBoxLauncher.Commands;

public class QRSiteCommand(IDialogService dialogService) : TypedCommand<Site> {
    protected override void Execute(Site parameter) {
        dialogService.Show<QRCodeDialog>(dialog => {
            dialog.InitDialog(parameter.Url);
        });
    }
}