using System.Collections.Generic;
using System.IO;
using NeoBoxLauncher.Extensions;
using Newtonsoft.Json;

namespace NeoBoxLauncher.Data.Common;

public class Pack {
    public LaunchType Type { get; set; } = LaunchType.Box;
    public string? Image { get; set; }
    public string Title { get; set; } = "";
    public string Launcher { get; set; } = "";
    public List<Game> Games { get; set; } = [];

    [JsonIgnore]
    public LazyBitmap ImageCached { get; set; } = default!;

    [JsonIgnore]
    public string PathFile { get; set; } = "";

    [JsonIgnore]
    public string? PathDirectory => Path.GetDirectoryName(PathFile);
}