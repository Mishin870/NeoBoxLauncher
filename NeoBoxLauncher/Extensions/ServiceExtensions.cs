using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NeoBoxLauncher.Data.Common;

namespace NeoBoxLauncher.Extensions;

public static class ServiceExtensions {
    public static IServiceCollection AddConfig(this IServiceCollection serviceCollection, IConfiguration configuration) {
        var config = configuration.Get<Config>();
        if (config == null) {
            throw new NullReferenceException();
        }

        serviceCollection.AddSingleton(config);

        return serviceCollection;
    }
}