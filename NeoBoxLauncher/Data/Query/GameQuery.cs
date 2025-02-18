using NeoBoxLauncher.Data.Common;
using NeoBoxLauncher.Interfaces;

namespace NeoBoxLauncher.Data.Query;

public class GameQuery : IGameFilter {
    public delegate void DelegateApplied();

    public event DelegateApplied? OnApplied;

    public FeaturesFilter FeaturesFilter { get; } = new();
    public TagsFilter TagsFilter { get; } = new();
    public TranslatorsFilter TranslatorsFilter { get; } = new();
    public PlayersFilter PlayersFilter { get; } = new();
    public DurationFilter DurationFilter { get; } = new();
    public LaunchTypeFilter LaunchTypeFilter { get; } = new();
    
    public GameQuery(IAttributeService attributeService) {
        FeaturesFilter.Init(attributeService.Features);
        TagsFilter.Init(attributeService.Tags);
        TranslatorsFilter.Init(attributeService.Translators);
    }

    public void Apply() {
        OnApplied?.Invoke();
    }

    public bool Filter(Game game, ISettingsService settingsService) {
        return FeaturesFilter.Filter(game, settingsService)
               && TagsFilter.Filter(game, settingsService)
               && TranslatorsFilter.Filter(game, settingsService)
               && PlayersFilter.Filter(game, settingsService)
               && DurationFilter.Filter(game, settingsService)
               && LaunchTypeFilter.Filter(game, settingsService);
    }

    public void Reset() {
        FeaturesFilter.Reset();
        TagsFilter.Reset();
        TranslatorsFilter.Reset();
        PlayersFilter.Reset();
        DurationFilter.Reset();
        LaunchTypeFilter.Reset();
    }
}