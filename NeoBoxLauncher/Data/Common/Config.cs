using System.Collections.Generic;

namespace NeoBoxLauncher.Data.Common;

public class Config {
    public List<string> Servers { get; set; } = [];
    public List<Site> Sites { get; set; } = [];
    public Dictionary<string, string> Features { get; set; } = [];
    public Dictionary<string, string> Tags { get; set; } = [];
    public Dictionary<string, string> Translators { get; set; } = [];
    public Dictionary<LaunchType, string> LaunchTypes { get; set; } = [];
}