using System.Collections.Generic;
using NeoBoxLauncher.Data.Common;
using NeoBoxLauncher.Data.Query.Base;

namespace NeoBoxLauncher.Data.Query;

public class TagsFilter : AttributesFilter {
    protected override HashSet<string> GetAttributes(Game game) {
        return game.Tags;
    }
}