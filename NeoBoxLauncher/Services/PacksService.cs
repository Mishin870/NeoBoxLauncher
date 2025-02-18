using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Microsoft.Extensions.Logging;
using NeoBoxLauncher.Data.Common;
using NeoBoxLauncher.Interfaces;
using Newtonsoft.Json;

namespace NeoBoxLauncher.Services;

public class PacksService(
    ILogger<PacksService> logger,
    ISettingsService settingsService,
    IResourceService resourceService,
    IVisualService visualService) : IPacksService {
    private readonly List<Pack> Items = [];
    private static readonly FilePickerFileType PackFileFilter = new("Pack") {
        Patterns = new[] { PackFile },
    };
    private const string PackFile = "pack.json";

    public IEnumerable<Pack> GetPacks() {
        return Items;
    }

    public IEnumerable<Game> GetGames() {
        return Items.SelectMany(pack => pack.Games);
    }

    private string BuildServerArgument() {
        var serverUrl = settingsService.ServerUrl;

        return string.IsNullOrWhiteSpace(serverUrl) ? "" : $"serverUrl={serverUrl}";
    }

    private bool LoadPack(string path) {
        logger.LogInformation("Loading pack: {pack}", path);
        
        var file = new FileInfo(path);
        if (!file.Exists) {
            logger.LogWarning("Pack doesn't exist");
            return false;
        }

        var favoriteGames = settingsService.FavoriteGames;

        try {
            var content = File.ReadAllText(file.FullName);
            var pack = JsonConvert.DeserializeObject<Pack>(content);
            if (pack == null) {
                return false;
            }

            pack.PathFile = path;

            var pathDirectory = pack.PathDirectory;
            if (pathDirectory != null && !string.IsNullOrWhiteSpace(pack.Image)) {
                pack.ImageCached = resourceService.LoadLazyBitmap(Path.Combine(pathDirectory, pack.Image));
            } else {
                pack.ImageCached = resourceService.LoadLazyBitmap();
            }

            Items.Add(pack);

            foreach (var game in pack.Games) {
                game.Pack = pack;

                if (pathDirectory != null && !string.IsNullOrWhiteSpace(game.Image)) {
                    game.ImageCached = resourceService.LoadLazyBitmap(Path.Combine(pathDirectory, game.Image));
                } else {
                    pack.ImageCached = resourceService.LoadLazyBitmap();
                }

                game.Favorite = favoriteGames.Contains(game.Id);
                game.ResourceService = resourceService;
            }
        } catch (Exception e) {
            logger.LogError(e, "Error loading pack");
            return false;
        }

        logger.LogInformation("Pack loaded successfully");
        return true;
    }

    public void Reload() {
        Items.Clear();

        var packsToRemove = settingsService.Packs.Where(path => !LoadPack(path)).ToList();
        foreach (var pack in packsToRemove) {
            settingsService.RemovePack(pack);
        }

        Items.Sort((x, y) => string.Compare(x.Title, y.Title, StringComparison.Ordinal));
    }

    public bool CanLaunchPack(Pack pack) {
        return pack.Type == LaunchType.Box;
    }

    public void LaunchPack(Pack pack) {
        logger.LogInformation("Launching pack: {pack}", pack.PathFile);
        
        var pathDirectory = pack.PathDirectory;
        if (pathDirectory == null) {
            logger.LogWarning("Pack directory doesn't exist");
            return;
        }

        pathDirectory = Path.GetFullPath(pathDirectory);
        ProcessStartInfo info;

        switch (pack.Type) {
            case LaunchType.Box:
                info = new ProcessStartInfo {
                    WorkingDirectory = pathDirectory,
                    FileName = Path.Combine(pathDirectory, pack.Launcher),
                    ArgumentList = {
                        "-jbg.config",
                        BuildServerArgument(),
                    },
                };
                break;
            case LaunchType.Native:
                return;
            default:
                throw new ArgumentOutOfRangeException();
        }

        try {
            info.UseShellExecute = true;
            var process = new Process {
                StartInfo = info,
            };
            process.Start();
        } catch (Exception e) {
            logger.LogError(e, "Error launching pack");
            visualService.ShowError("Ошибка запуска пака :(\n" +
                                    "Полный текст ошибки можете найти в логах\n" +
                                    "\n" +
                                    "Текст ошибки на английском:\n\n" +
                                    $"{e}");
        }
    }

    public void LaunchGame(Game game) {
        logger.LogInformation("Launching game: {pack} -> {game}", game.Pack.PathFile, game.Id);
        
        var pathDirectory = game.Pack.PathDirectory;
        if (pathDirectory == null) {
            return;
        }

        pathDirectory = Path.GetFullPath(pathDirectory);
        ProcessStartInfo info;

        switch (game.Type) {
            case LaunchType.Box:
                info = new ProcessStartInfo {
                    WorkingDirectory = pathDirectory,
                    FileName = Path.Combine(pathDirectory, game.Launcher ?? game.Pack.Launcher),
                    ArgumentList = {
                        "-launchTo",
                        game.Path,
                        "-jbg.config",
                        BuildServerArgument(),
                    },
                };
                break;
            case LaunchType.Native:
                info = new ProcessStartInfo {
                    WorkingDirectory = pathDirectory,
                    FileName = Path.Combine(pathDirectory, game.Path),
                };
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        try {
            info.UseShellExecute = true;
            var process = new Process {
                StartInfo = info,
            };
            process.Start();
        } catch (Exception e) {
            logger.LogError(e, "Error launching game");
            visualService.ShowError("Ошибка запуска игры :(\n" +
                                    "Полный текст ошибки можете найти в логах\n" +
                                    "\n" +
                                    "Текст ошибки на английском:\n\n" +
                                    $"{e}");
        }
    }

    public async Task ShowAddPackDialogAsync(TopLevel topLevel) {
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions {
            Title = $"Укажите {PackFile} файл из пака",
            AllowMultiple = false,
            FileTypeFilter = new[] {
                PackFileFilter,
            },
        });

        if (files.Count >= 1) {
            var file = new FileInfo(files[0].Path.LocalPath);

            settingsService.AddPack(file.FullName);

            visualService.ShowSnackbar("Пак добавлен");
        }
    }
}