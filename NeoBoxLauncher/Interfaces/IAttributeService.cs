using System.Collections.Generic;
using NeoBoxLauncher.Data.Common;

namespace NeoBoxLauncher.Interfaces;

public interface IAttributeService {
    IReadOnlyList<Attribute> Features { get; }
    IReadOnlyList<Attribute> Tags { get; }
    IReadOnlyList<Attribute> Translators { get; }
    
    IReadOnlyDictionary<string, Attribute> FeaturesMap { get; }
    IReadOnlyDictionary<string, Attribute> TagsMap { get; }
    IReadOnlyDictionary<string, Attribute> TranslatorsMap { get; }
}