using NeoBoxLauncher.Data.Common;
using NeoBoxLauncher.Extensions.Notify;
using NeoBoxLauncher.Interfaces;

namespace NeoBoxLauncher.Data.Query;

public class PlayersFilter : NotifyPropertyChangedBase, IGameFilter {
    private int _Players = 1;
    private bool _Enabled = false;

    public bool Enabled {
        get => _Enabled;
        set => SetField(ref _Enabled, value);
    }

    public int Players {
        get => _Players;
        set => SetField(ref _Players, value);
    }

    public bool Filter(Game game, ISettingsService settingsService) {
        if (!_Enabled) {
            return true;
        }

        return _Players >= game.Players.From
               && _Players <= game.Players.To;
    }

    public void Reset() {
        Players = 1;
        Enabled = false;
    }
}