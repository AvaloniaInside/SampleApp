using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaInside.SampleApp.ViewModels;
using AvaloniaInside.SampleApp.Views;

namespace AvaloniaInside.SampleApp;

public class App : Application
{
    public override void Initialize()
    {PerformanceCounter.Step("Hit App.Initialize");
        AvaloniaXamlLoader.Load(this);
        PerformanceCounter.Step("App AvaloniaXamlLoader loaded");
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            desktop.MainWindow = new MainWindow();
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleView)
            singleView.MainView = new MainView();
        
        base.OnFrameworkInitializationCompleted();
    }
}