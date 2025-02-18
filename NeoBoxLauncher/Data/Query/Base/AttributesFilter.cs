using System.Collections.Generic;
using System.Linq;
using NeoBoxLauncher.Data.Common;
using NeoBoxLauncher.Extensions.Notify;
using NeoBoxLauncher.Interfaces;
using Attribute=NeoBoxLauncher.Data.Common.Attribute;

namespace NeoBoxLauncher.Data.Query.Base;

public abstract class AttributesFilter : IGameFilter {
    public readonly Dictionary<string, ObservableBool> States = new();

    public void Init(IReadOnlyList<Attribute> attributes) {
        foreach (var attribute in attributes) {
            States[attribute.Id] = new ObservableBool(true);
        }
    }

    public bool this[string key] {
        get => States[key].Value;
        set => States[key].Value = value;
    }

    public bool Filter(Game game, ISettingsService settingsService) {
        var attributes = GetAttributes(game);
        
        return attributes.Count == 0 || attributes.Any(item => States[item].Value);
    }

    protected abstract HashSet<string> GetAttributes(Game game);

    public void SetAll(bool stateValue) {
        foreach (var (key, _) in States) {
            States[key].Value = stateValue;
        }
    }

    public void Reset() {
        SetAll(true);
    }
}