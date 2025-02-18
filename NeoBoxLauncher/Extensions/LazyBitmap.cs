using System;
using Avalonia.Media.Imaging;

namespace NeoBoxLauncher.Extensions;

public class LazyBitmap(Bitmap notFound, string? path = null) {
    private Bitmap? _Bitmap;
    private bool Loaded;

    public Bitmap? Bitmap {
        get {
            if (!Loaded) {
                try {
                    if (path != null) {
                        _Bitmap = new Bitmap(path);
                    }
                } catch (Exception) {
                    _Bitmap = notFound;
                }

                Loaded = true;
            }

            return _Bitmap;
        }
    }

    public bool Exist => path != null;
}