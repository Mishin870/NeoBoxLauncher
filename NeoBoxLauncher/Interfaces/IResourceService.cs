using System.ComponentModel;
using System.IO;
using Avalonia.Media.Imaging;
using NeoBoxLauncher.Extensions;

namespace NeoBoxLauncher.Interfaces;

public interface IResourceService : INotifyPropertyChanged {
    Bitmap BitmapFade { get; }
    Bitmap BitmapBack { get; }

    Stream Load(string path);
    Bitmap LoadBitmap(string path);
    LazyBitmap LoadLazyBitmap(string? path = null);
}