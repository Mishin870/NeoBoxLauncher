using System;
using Avalonia;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;

namespace NeoBoxLauncher.Extensions.DI;

public class GetServiceExtension(Type serviceType) : MarkupExtension {
    public override object ProvideValue(IServiceProvider serviceProvider) {
        if (Application.Current is not App app) {
            throw new InvalidOperationException("Application is not initialized");
        }

        return app.ServiceProvider.GetRequiredService(serviceType);
    }
}