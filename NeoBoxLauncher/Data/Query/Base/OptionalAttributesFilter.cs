using System.Collections.Generic;
using NeoBoxLauncher.Data.Common;
using NeoBoxLauncher.Extensions.Notify;
using NeoBoxLauncher.Interfaces;
using Attribute=NeoBoxLauncher.Data.Common.Attribute;

namespace NeoBoxLauncher.Data.Query.Base;

public abstract class OptionalAttributesFilter : IGameFilter {
    public readonly Dictionary<string, ObservableBoolNullable> States = new();

    public void Init(IReadOnlyList<Attribute> attributes) {
        foreach (var attribute in attributes) {
            States[attribute.Id] = new ObservableBoolNullable();
        }
    }

    public bool? this[string key] {
        get => States[key].Value;
        set => States[key].Value = value;
    }

    public bool Filter(Game game, ISettingsService settingsService) {
        var attributes = GetAttributes(game);
        
        foreach (var (key, observableBoolNullable) in States) {
            var value = observableBoolNullable.Value;

            if (value.HasValue) {
                if (value.Value) {
                    if (!attributes.Contains(key)) {
                        return false;
                    }
                } else {
                    if (attributes.Contains(key)) {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    protected abstract HashSet<string> GetAttributes(Game game);

    public void SetAll(bool? stateValue = null) {
        foreach (var (key, _) in States) {
            States[key].Value = stateValue;
        }
    }

    public void Reset() {
        SetAll();
    }
}