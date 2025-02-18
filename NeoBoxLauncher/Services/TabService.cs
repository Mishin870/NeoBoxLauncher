using System;
using Microsoft.Extensions.DependencyInjection;
using NeoBoxLauncher.Interfaces;

namespace NeoBoxLauncher.Services;

public class TabService(IServiceProvider serviceProvider) : ITabService {
    public T Create<T>() where T : ITab {
        return serviceProvider.GetRequiredService<T>();
    }
}