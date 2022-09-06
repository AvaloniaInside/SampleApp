using System;
using System.Diagnostics;

namespace AvaloniaInside.SampleApp;

public static class PerformanceCounter
{
    private static readonly Stopwatch _watch = new();
    private static long _totalTime;

    public static void Start()
    {
        _watch.Start();
        _totalTime = 0;
    }

    public static void Stop()
    {
        _watch.Stop();
    }

    public static void Step(string message)
    {
        if (!_watch.IsRunning)
            Start();
        var step = _watch.ElapsedMilliseconds;
        _totalTime += step;
        Console.WriteLine(
            $"Total:{_totalTime} ms Step:{step} ms: {message}");
        _watch.Restart();
    }
}