using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using ReactiveUI;

namespace AvaloniaInside.SampleApp.Navigation;

public sealed class NavigationHandler : ReactiveObject
{
    public static NavigationHandler Instance = new();
    private readonly List<NavigationItem> _cachedObjects = new();
    private readonly List<NavigationItem> _navigationStack = new();
    private NavigationItem _currentItem;
    private NavigationItem? _previusItem;

    private NavigationHandler()
    {
    }

    public NavigationItem? PreviusItem
    {
        get => _previusItem;
        set
        {
            this.RaiseAndSetIfChanged(ref _previusItem, value);
            this.RaisePropertyChanged(nameof(CanGoBack));
        }
    }

    public bool CanGoBack => PreviusItem != null;

    public NavigationItem CurrentView
    {
        get => _currentItem;
        set => this.RaiseAndSetIfChanged(ref _currentItem, value);
    }

    public ICommand GoBackCommand => ReactiveCommand.Create(GoBack);
    public ICommand PushCommand => ReactiveCommand.Create<Type>(Push);

    public void Push(Type contentType)
    {
        var cachedObject = _cachedObjects.FirstOrDefault(w => w.ContentType == contentType);
        if (cachedObject == null)
        {
            NavigationItem newItem = new(contentType);
            _cachedObjects.Add(newItem);
            Push(newItem);
            return;
        }

        Push(cachedObject);
    }

    public void Push(NavigationItem item)
    {
        if (item.Control == null)
            item.CreateControl();
        if (_navigationStack.Count == 0 || (_navigationStack.Count > 0 && _navigationStack.Last() != item))
            _navigationStack.Add(item);
        PreviusItem = _navigationStack.Count > 1 ? _navigationStack[_navigationStack.Count - 2] : null;
        CurrentView = item;
    }

    public void GoBack()
    {
        if (!CanGoBack)
            return;
        _navigationStack.Remove(_navigationStack.Last());
        Push(_navigationStack.Last());
    }
}