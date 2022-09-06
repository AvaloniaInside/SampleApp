using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Avalonia;
using Avalonia.LinuxFramebuffer.Output;
using Avalonia.Media;
using Avalonia.ReactiveUI;

namespace AvaloniaInside.SampleApp;

internal class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static int Main(string[] args)
    {
        if (args.Contains("--wait-for-debugger"))
            while (!Debugger.IsAttached)
                Thread.Sleep(100);

        PerformanceCounter.Step("Hit Program.Main");
        var app = BuildAvaloniaApp();
        PerformanceCounter.Step("App builded");
        var isDrm = args.Contains("--drm");
        var isFbDev = args.Contains("--fbdev");

        if (isDrm)
        {
            var drmOutput = new DrmOutput(GetArgumentValue(args, "--card", "/dev/dri/card0"));
            PerformanceCounter.Step("DrmOutput created");
            return app.StartLinuxDirect(args, drmOutput);
        }

        if (isFbDev)
            app.StartLinuxFbDev(args);

        return app
            .StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder.Configure<App>()
            .With(new FontManagerOptions
            {
                DefaultFamilyName =
                    "resm:AvaloniaInside.SampleApp.Assets?assembly=AvaloniaInside.SampleApp#Proxima Nova"
            })
            .UsePlatformDetect()
            .LogToTrace()
            .UseReactiveUI();
    }

    private static string GetArgumentValue(string[] args, string parameter, string defaultValue = "")
    {
        foreach (var arg in args)
            if (arg.StartsWith(parameter))
                return arg.Remove(0, parameter.Length + 1);

        return defaultValue;
    }

    private static double GetArgumentValue(string[] args, string parameter, double defaultValue = 1)
    {
        foreach (var arg in args)
            if (arg.StartsWith(parameter))
            {
                if (double.TryParse(arg.Remove(0, parameter.Length + 1), out var value))
                    return value;
                return defaultValue;
            }

        return defaultValue;
    }
}