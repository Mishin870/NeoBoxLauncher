using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.Extensions.Logging;
using NeoBoxLauncher.Data.Common;
using NeoBoxLauncher.Extensions.Notify;
using NeoBoxLauncher.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NeoBoxLauncher.Services;

public class SettingsService : NotifyPropertyChangedBase, ISettingsService {
    private static string FilePath => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "Mishin870", App.AssemblyName, "settings.json");

    private readonly SettingsContainer Settings;

    public bool ThemeDark {
        get => Settings.ThemeDark;
        set => SetAndSave(ref Settings.ThemeDark, value);
    }

    public bool ShowAdd {
        get => Settings.ShowAdd;
        set => SetAndSave(ref Settings.ShowAdd, value);
    }

    public int PackLogoHeight {
        get => Settings.PackLogoHeight;
        set => SetAndSave(ref Settings.PackLogoHeight, value);
    }

    public string ServerUrl {
        get => Settings.ServerUrl;
        set => SetAndSave(ref Settings.ServerUrl, value);
    }

    public IReadOnlySet<string> Packs => Settings.Packs;
    public IReadOnlySet<string> FavoriteGames => Settings.FavoriteGames;

    public void AddPack(string fullName) {
        Settings.Packs.Add(fullName);
        Save();
        OnPropertyChanged(nameof(Packs));
    }

    public void RemovePack(string fullName) {
        Settings.Packs.Remove(fullName);
        Save();
        OnPropertyChanged(nameof(Packs));
    }

    public void ToggleFavorite(Game game) {
        // ReSharper disable once CanSimplifySetAddingWithSingleCall
        if (Settings.FavoriteGames.Contains(game.Id)) {
            Settings.FavoriteGames.Remove(game.Id);
            game.Favorite = false;
        } else {
            Settings.FavoriteGames.Add(game.Id);
            game.Favorite = true;
        }
        Save();
        OnPropertyChanged(nameof(FavoriteGames));
    }

    public SettingsService(ILogger<SettingsService> logger) {
        var loadedSettings = (SettingsContainer?)null;

        try {
            if (File.Exists(FilePath)) {
                loadedSettings = JsonConvert.DeserializeObject<SettingsContainer>(File.ReadAllText(FilePath));
            }
        } catch (Exception e) {
            logger.LogError(e, "Error loading settings");
        }

        Settings = loadedSettings ?? new SettingsContainer();
    }

    private class SettingsContainer {
        public bool ThemeDark;
        public bool ShowAdd = true;
        public int PackLogoHeight = 200;
        public string ServerUrl = "";
        public HashSet<string> Packs = [];
        public HashSet<string> FavoriteGames = [];

        [OnError]
        internal void OnError(StreamingContext context, ErrorContext errorContext) {
            errorContext.Handled = true;
        }
    }

    private void SetAndSave<T>(ref T field, T value, [CallerMemberName] string? propertyName = null) {
        if (EqualityComparer<T>.Default.Equals(field, value)) {
            return;
        }

        field = value;
        Save();

        OnPropertyChanged(propertyName);
    }

    private void Save() {
        Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);
        File.WriteAllText(FilePath, JsonConvert.SerializeObject(Settings, Formatting.Indented));
    }
}