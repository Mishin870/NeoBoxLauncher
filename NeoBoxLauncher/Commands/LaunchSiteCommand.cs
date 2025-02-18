using System;
using NeoBoxLauncher.Data.Common;
using NeoBoxLauncher.Interfaces;

namespace NeoBoxLauncher.Commands;

public class LaunchSiteCommand(ILauncherProvider launcherProvider) : TypedCommand<Site> {
    protected override async void Execute(Site parameter) {
        var launcher = launcherProvider.GetLauncher();

        if (launcher != null) {
            await launcher.LaunchUriAsync(new Uri(parameter.Url));
        }
    }
}