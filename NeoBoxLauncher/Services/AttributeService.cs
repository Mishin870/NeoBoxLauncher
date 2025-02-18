using System.Collections.Generic;
using System.Linq;
using NeoBoxLauncher.Data.Common;
using NeoBoxLauncher.Interfaces;

namespace NeoBoxLauncher.Services;

public class AttributeService : IAttributeService {
    public IReadOnlyList<Attribute> Features { get; }
    public IReadOnlyList<Attribute> Tags { get; }
    public IReadOnlyList<Attribute> Translators { get; }
    
    public IReadOnlyDictionary<string, Attribute> FeaturesMap { get; }
    public IReadOnlyDictionary<string, Attribute> TagsMap { get; }
    public IReadOnlyDictionary<string, Attribute> TranslatorsMap { get; }

    public AttributeService(Config config) {
        Features = BuildAttributes(config.Features);
        FeaturesMap = BuildAttributesMap(Features);
        
        Tags = BuildAttributes(config.Tags);
        TagsMap = BuildAttributesMap(Tags);
        
        Translators = BuildAttributes(config.Translators);
        TranslatorsMap = BuildAttributesMap(Translators);
    }

    private static List<Attribute> BuildAttributes(Dictionary<string, string> source) {
        return source.Select(pair => new Attribute {
            Id = pair.Key,
            Value = pair.Value,
        }).ToList();
    }
    
    private static Dictionary<string, Attribute> BuildAttributesMap(IReadOnlyList<Attribute> source) {
        return source.ToDictionary(attribute => attribute.Id);
    }
}