using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using AvaloniaInside.SampleApp.Navigation;

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
        // CreateView();
        // PerformanceCounter.Step("View created");
    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);

        if (_firstTimeRendered) return;
        _firstTimeRendered = true;
        Program.StopSplashScreen();
        PerformanceCounter.Step("First time rendered");
    }

    private void CreateView()
    {
        var grid = new Grid();
        grid.RowDefinitions.Add(new RowDefinition(65, GridUnitType.Pixel));
        grid.RowDefinitions.Add(new RowDefinition(1, GridUnitType.Star));

        var contentControl = new ContentControl();
        var binding = new Binding
        {
            Source = NavigationHandler.Instance,
            Path = "CurrentView.Control"
        };
        contentControl.Bind(ContentProperty, binding);
        Grid.SetRow(contentControl, 1);
        grid.Children.Add(contentControl);

        var header = new Header();
        Grid.SetRow(header, 0);
        grid.Children.Add(header);

        Content = grid;
    }
}