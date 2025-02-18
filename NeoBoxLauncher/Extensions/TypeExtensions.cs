using System;
using System.Collections.Generic;
using System.Linq;

namespace NeoBoxLauncher.Extensions;

public static class TypeExtensions {
    public static IEnumerable<T> GetEnumValues<T>() where T : Enum {
        return Enum.GetValues(typeof(T)).Cast<T>();
    }

    public static void ForEach<T>(this IEnumerable<T> items, Action<T> action) {
        foreach (var item in items) {
            action(item);
        }
    }
}