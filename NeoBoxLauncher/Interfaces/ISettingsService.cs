using System.Collections.Generic;
using System.ComponentModel;
using NeoBoxLauncher.Data.Common;

namespace NeoBoxLauncher.Interfaces;

public interface ISettingsService : INotifyPropertyChanged {
    bool ThemeDark { get; set; }
    bool ShowAdd { get; set; }
    int PackLogoHeight { get; set; }
    string ServerUrl { get; set; }
    IReadOnlySet<string> Packs { get; }
    IReadOnlySet<string> FavoriteGames { get; }

    void AddPack(string fullName);
    void RemovePack(string fullName);

    void ToggleFavorite(Game game);
}