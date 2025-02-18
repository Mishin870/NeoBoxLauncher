using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NeoBoxLauncher.Data.Common;
using NeoBoxLauncher.Extensions.Notify;
using NeoBoxLauncher.Interfaces;

namespace NeoBoxLauncher.Data.Query;

public class GameQueryView(GameQuery gameQuery, ISettingsService settingsService) : NotifyPropertyChangedBase {
    private readonly List<Game> AllGames = [];
    private IGameFilter? CustomGameFilter;

    public ObservableCollection<Game> Games { get; } = [];

    private bool _Empty;

    public bool Empty {
        get => _Empty;
        private set => SetField(ref _Empty, value);
    }

    public void SetAllGames(IEnumerable<Game> allGames) {
        AllGames.Clear();
        AllGames.AddRange(allGames);
        OnQueryApplied();
    }

    public void SetCustomFilter(IGameFilter? customFilter = null) {
        CustomGameFilter = customFilter;
        OnQueryApplied();
    }

    public void Bind() {
        gameQuery.OnApplied += OnQueryApplied;

        OnQueryApplied();
    }

    public void UnBind() {
        gameQuery.OnApplied -= OnQueryApplied;
    }

    private void OnQueryApplied() {
        Games.Clear();

        var gamesQueryable = AllGames.AsQueryable();
        gamesQueryable = gamesQueryable.Where(game => gameQuery.Filter(game, settingsService));

        if (CustomGameFilter != null) {
            gamesQueryable = gamesQueryable.Where(game => CustomGameFilter.Filter(game, settingsService));
        }

        foreach (var game in gamesQueryable) {
            Games.Add(game);
        }

        Empty = Games.Count == 0;
    }
}