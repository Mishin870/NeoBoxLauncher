using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using NeoBoxLauncher.Extensions.Notify;
using ZXing;

namespace NeoBoxLauncher.ViewModels.Dialogs;

public class QRCodeDialogViewModel : NotifyPropertyChangedBase {
    private int _QrWidth = 300;
    private int _QrHeight = 300;
    private IImage _DataImage = null!;

    public int QRWidth {
        get => _QrWidth;
        set => SetField(ref _QrWidth, value);
    }

    public int QRHeight {
        get => _QrHeight;
        set => SetField(ref _QrHeight, value);
    }

    public IImage DataImage {
        get => _DataImage;
        set => SetField(ref _DataImage, value);
    }

    public void Init(string data) {
        var barcodeWriter = new BarcodeWriterPixelData {
            Format = BarcodeFormat.QR_CODE,
            Options = new ZXing.Common.EncodingOptions {
                PureBarcode = false,
                Width = QRWidth,
                Height = QRHeight,
            },
        };
        var imageData = barcodeWriter.Write(data);
        DataImage = CreateBitmapFromPixelData(imageData.Pixels,
            imageData.Width, imageData.Height);
    }

    private static WriteableBitmap CreateBitmapFromPixelData(byte[] bgraPixelData, int pixelWidth, int pixelHeight) {
        var dpi = new Vector(96, 96);

        var bitmap = new WriteableBitmap(new PixelSize(pixelWidth, pixelHeight),
            dpi, PixelFormat.Bgra8888, AlphaFormat.Premul);

        using var frameBuffer = bitmap.Lock();
        Marshal.Copy(bgraPixelData, 0, frameBuffer.Address, bgraPixelData.Length);

        return bitmap;
    }
}