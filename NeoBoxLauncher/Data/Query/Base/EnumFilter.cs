using System;
using System.Collections.Generic;
using System.Linq;
using NeoBoxLauncher.Data.Common;
using NeoBoxLauncher.Extensions;
using NeoBoxLauncher.Extensions.Notify;
using NeoBoxLauncher.Interfaces;

namespace NeoBoxLauncher.Data.Query.Base;

public abstract class EnumFilter<T> : IGameFilter where T : Enum {
    public readonly Dictionary<T, ObservableBool> States = new();

    protected EnumFilter() {
        foreach (var value in TypeExtensions.GetEnumValues<T>()) {
            States[value] = new ObservableBool(true);
        }
    }

    public bool this[T key] {
        get => States[key].Value;
        set => States[key].Value = value;
    }

    protected bool DoFilter(Game game, HashSet<T> gameItems) {
        return gameItems.Count == 0 || gameItems.Any(item => States[item].Value);
    }

    protected bool DoFilter(Game game, T gameItem) {
        return States[gameItem].Value;
    }

    public abstract bool Filter(Game game, ISettingsService settingsService);

    public void SetAll(bool stateValue) {
        foreach (var (key, _) in States) {
            States[key].Value = stateValue;
        }
    }

    public void Reset() {
        SetAll(true);
    }
}