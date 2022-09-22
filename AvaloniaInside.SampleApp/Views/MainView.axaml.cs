using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace AvaloniaInside.SampleApp.Views;

public class MainView : UserControl
{
    private bool _firstTimeRendered;

    public MainView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        e.Root.Renderer.DrawFps = true;
        base.OnAttachedToVisualTree(e);
    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);

        if (_firstTimeRendered) return;
        _firstTimeRendered = true;
        Program.StopSplashScreen();
        PerformanceCounter.Step("First time rendered");
    }
}