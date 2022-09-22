using Avalonia.LinuxFramebuffer.Output;
using SkiaSharp;

namespace AvaloniaInside.SampleApp.Splash;

class AppSplash : LottieSplashToDrm
{
    public AppSplash(DrmOutput drmOutput) : base(drmOutput)
    {
    }

    protected override void Draw(SKCanvas canvas)
    {
        canvas.Clear(SKColors.White);
        base.Draw(canvas);
    }
}