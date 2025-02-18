using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using NeoBoxLauncher.Data.Common;

namespace NeoBoxLauncher.Interfaces;

public interface IPacksService {
    IEnumerable<Pack> GetPacks();
    IEnumerable<Game> GetGames();
    void Reload();
    bool CanLaunchPack(Pack pack);
    void LaunchPack(Pack pack);
    void LaunchGame(Game game);
    Task ShowAddPackDialogAsync(TopLevel topLevel);
}