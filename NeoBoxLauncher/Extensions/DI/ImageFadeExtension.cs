using System;
using Avalonia;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using NeoBoxLauncher.Interfaces;

namespace NeoBoxLauncher.Extensions.DI;

public class ImageFadeExtension : MarkupExtension {
    public override object ProvideValue(IServiceProvider serviceProvider) {
        if (Application.Current is not App app) {
            throw new InvalidOperationException("Application is not initialized");
        }

        var resources = app.ServiceProvider.GetRequiredService<IResourceService>();
        return resources.BitmapFade;
    }
}