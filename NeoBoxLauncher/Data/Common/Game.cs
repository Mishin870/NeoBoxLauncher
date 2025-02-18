using System.Collections.Generic;
using System.Runtime.Serialization;
using Material.Icons;
using NeoBoxLauncher.Extensions;
using NeoBoxLauncher.Extensions.Notify;
using NeoBoxLauncher.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NeoBoxLauncher.Data.Common;

public class Game : NotifyPropertyChangedBase {
    public string Id { get; set; } = "";
    public LaunchType Type { get; set; } = LaunchType.Box;
    public string? Image { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Path { get; set; } = "";
    public string? Launcher { get; set; }

    public HashSet<string> Tags { get; set; } = [];
    public HashSet<string> Features { get; set; } = [];
    public HashSet<string> Translators { get; set; } = [];
    public Interval Players { get; set; } = new(1, 1000);
    public int Duration { get; set; }

    public string Version { get; set; } = "";
    public string Build { get; set; } = "";

    [JsonIgnore]
    public IResourceService ResourceService { get; set; } = default!;

    [JsonIgnore]
    public bool ShowBuild => !string.IsNullOrWhiteSpace(Build);

    [JsonIgnore]
    public bool ShowVersion => !string.IsNullOrWhiteSpace(Version);

    [JsonIgnore]
    public LazyBitmap ImageCached { get; set; } = default!;

    [JsonIgnore]
    public Pack Pack { get; set; } = null!;

    [JsonIgnore]
    private bool _Favorite;

    [JsonIgnore]
    public bool Favorite {
        get => _Favorite;
        set {
            if (_Favorite == value) {
                return;
            }

            _Favorite = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(FavoriteIcon));
        }
    }

    [JsonIgnore]
    public MaterialIconKind FavoriteIcon => Favorite
        ? MaterialIconKind.CardsHeart
        : MaterialIconKind.CardsHeartOutline;

    [OnError]
    internal void OnError(StreamingContext context, ErrorContext errorContext) {
        errorContext.Handled = true;
    }
}