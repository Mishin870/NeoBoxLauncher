using System.Collections.Generic;
using System.Collections.ObjectModel;
using NeoBoxLauncher.Data.Common;
using NeoBoxLauncher.Data.Query;
using NeoBoxLauncher.Data.Query.Base;
using NeoBoxLauncher.Extensions.Notify;
using NeoBoxLauncher.Extensions.UI;
using NeoBoxLauncher.Interfaces;

namespace NeoBoxLauncher.ViewModels.Dialogs;

public class GameQueryDialogViewModel(GameQuery gameQuery, IAttributeService attributeService, 
    Config config) : NotifyPropertyChangedBase {
    public ObservableCollection<EnumCheckboxNullable> Features { get; } = [];
    public ObservableCollection<EnumCheckbox> Tags { get; } = [];
    public ObservableCollection<EnumCheckbox> Translators { get; } = [];
    public ObservableCollection<EnumCheckbox> LaunchTypes { get; } = [];

    public GameQuery Query => gameQuery;

    public void Init() {
        FillAttributes(Features, Query.FeaturesFilter, attributeService.FeaturesMap);
        FillAttributes(Tags, Query.TagsFilter, attributeService.TagsMap);
        FillAttributes(Translators, Query.TranslatorsFilter, attributeService.TranslatorsMap);

        foreach (var (key, value) in Query.LaunchTypeFilter.States) {
            LaunchTypes.Add(new EnumCheckbox(config.LaunchTypes[key], value));
        }
    }

    private void FillAttributes(ObservableCollection<EnumCheckboxNullable> visualAttributes, OptionalAttributesFilter filter,
        IReadOnlyDictionary<string, Attribute> attributesMap) {
        foreach (var (key, value) in filter.States) {
            if (!attributesMap.TryGetValue(key, out var attribute)) {
                continue;
            }
            
            visualAttributes.Add(new EnumCheckboxNullable(attribute.Value, value));
        }
    }
    
    private void FillAttributes(ObservableCollection<EnumCheckbox> visualAttributes, AttributesFilter filter,
        IReadOnlyDictionary<string, Attribute> attributesMap) {
        foreach (var (key, value) in filter.States) {
            if (!attributesMap.TryGetValue(key, out var attribute)) {
                continue;
            }
            
            visualAttributes.Add(new EnumCheckbox(attribute.Value, value));
        }
    }
}