using System.Collections.Generic;
using System.Collections.ObjectModel;
using NeoBoxLauncher.Commands;
using NeoBoxLauncher.Data.Common;
using NeoBoxLauncher.Extensions.Notify;
using NeoBoxLauncher.Interfaces;

namespace NeoBoxLauncher.ViewModels.Dialogs;

public class GameCardDialogViewModel(
    IAttributeService attributeService,
    IResourceService resourceService,
    LikeGameCommand likeGameCommand,
    LaunchGameCommand launchGameCommand) : NotifyPropertyChangedBase {
    public IResourceService ResourceService => resourceService;

    public ObservableCollection<Attribute> Tags { get; } = [];
    public ObservableCollection<Attribute> Features { get; } = [];
    public ObservableCollection<Attribute> Translators { get; } = [];

    private Game _Game = new();
    public bool HasTags => Tags.Count != 0;
    public bool HasFeatures => Features.Count != 0;
    public bool HasTranslators => Translators.Count != 0;

    public LikeGameCommand LikeGameCommand => likeGameCommand;
    public LaunchGameCommand LaunchGameCommand => launchGameCommand;

    public Game Game {
        get => _Game;
        private set => SetField(ref _Game, value);
    }

    public void Init(Game game) {
        Game = game;

        FillAttributes(Features, game.Features, attributeService.FeaturesMap, nameof(HasFeatures));
        FillAttributes(Tags, game.Tags, attributeService.TagsMap, nameof(HasTags));
        FillAttributes(Translators, game.Translators, attributeService.TranslatorsMap, nameof(HasTranslators));
    }

    private void FillAttributes(ObservableCollection<Attribute> visualAttributes, HashSet<string> gameAttributes,
        IReadOnlyDictionary<string, Attribute> attributesMap, string hasPropertyName) {
        foreach (var gameAttribute in gameAttributes) {
            if (!attributesMap.TryGetValue(gameAttribute, out var attribute)) {
                continue;
            }

            visualAttributes.Add(attribute);
        }

        visualAttributes.CollectionChanged += (_, _) => {
            OnPropertyChanged(hasPropertyName);
        };
        OnPropertyChanged(hasPropertyName);
    }
}