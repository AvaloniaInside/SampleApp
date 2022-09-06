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

    public override void Render(DrawingContext context)
    {
        base.Render(context);

        if (_firstTimeRendered) return;
        _firstTimeRendered = true;
        PerformanceCounter.Step("First time rendered");
    }
}