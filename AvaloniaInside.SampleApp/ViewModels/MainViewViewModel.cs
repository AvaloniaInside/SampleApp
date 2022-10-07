using System;
using System.Timers;
using AvaloniaInside.SampleApp.Navigation;
using AvaloniaInside.SampleApp.Views;

namespace AvaloniaInside.SampleApp.ViewModels;

public class MainViewViewModel : ViewModelBase
{
    private readonly Timer _dateTimeTimer;
    private string _currentDate;
    private string _currentTime;

    public MainViewViewModel()
    {
        _dateTimeTimer = new Timer
        {
            Interval = 1000,
            Enabled = true
        };
        _dateTimeTimer.Elapsed += DateTimeTimerOnElapsed;
        NavigationHandler.Instance.Push(new NavigationItem("Home", typeof(HomeView)));
    }

    public string CurrentDate
    {
        get => _currentDate;
        set => RaiseAndSetIfChanged(ref _currentDate, value);
    }

    public string CurrentTime
    {
        get => _currentTime;
        set => RaiseAndSetIfChanged(ref _currentTime, value);
    }

    private void DateTimeTimerOnElapsed(object? sender, ElapsedEventArgs e)
    {
        var curDate = DateTime.Now;
        CurrentDate = curDate.ToString("yyyy-MM-dd");
        CurrentTime = curDate.ToString("HH:mm:ss");
    }
}