using NeoBoxLauncher.Data.Common;
using NeoBoxLauncher.Extensions.Notify;
using NeoBoxLauncher.Interfaces;

namespace NeoBoxLauncher.Data.Query;

public class DurationFilter : NotifyPropertyChangedBase, IGameFilter {
    private int _DurationTo = 20;
    private int _DurationFrom = 0;
    private bool _Enabled = false;

    public bool Enabled {
        get => _Enabled;
        set => SetField(ref _Enabled, value);
    }

    public int DurationFrom {
        get => _DurationFrom;
        set => SetField(ref _DurationFrom, value);
    }

    public int DurationTo {
        get => _DurationTo;
        set => SetField(ref _DurationTo, value);
    }

    public bool Filter(Game game, ISettingsService settingsService) {
        if (!_Enabled) {
            return true;
        }

        return game.Duration >= _DurationFrom
               && game.Duration <= _DurationTo;
    }

    public void Reset() {
        DurationFrom = 0;
        DurationTo = 20;
        Enabled = false;
    }
}