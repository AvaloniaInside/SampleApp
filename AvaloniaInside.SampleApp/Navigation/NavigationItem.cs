using System;
using Avalonia.Controls;
using ReactiveUI;

namespace AvaloniaInside.SampleApp.Navigation;

public class NavigationItem : ReactiveObject
{
    private Control? _control;

    private string _name;

    public NavigationItem(string name, Type contentType)
    {
        Name = name;
        ContentType = contentType;
    }

    public NavigationItem(Type contentType)
    {
        ContentType = contentType;
    }

    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public Type ContentType { get; set; }

    public Control? Control
    {
        get => _control;
        private set => this.RaiseAndSetIfChanged(ref _control, value);
    }

    public void CreateControl()
    {
        if (Control != null)
            return;
        Control = (Control)Activator.CreateInstance(ContentType);
    }
}