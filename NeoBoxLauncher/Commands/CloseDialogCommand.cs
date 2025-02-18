using DialogHostAvalonia;

namespace NeoBoxLauncher.Commands;

public class CloseDialogCommand : TypedCommand<DialogHost> {
    protected override void Execute(DialogHost dialogHost) {
        dialogHost.IsOpen = false;
    }
}