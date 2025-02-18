using Material.Icons;

namespace NeoBoxLauncher.Data.Common;

public class Tab {
    public TabType Type { get; set; } = TabType.AllPacks;
    public Pack Pack { get; set; } = new();

    public string Title { get; set; } = "";
    public MaterialIconKind Icon { get; set; } = MaterialIconKind.Apps;
}