using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using TimeInWords.Views;

namespace TimeInWords;

public partial class App : Application
{
    private readonly List<MainView> _views = [];

    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        base.OnFrameworkInitializationCompleted();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var settings = new TimeInWordsSettings { Debug = false };

            if (settings.Debug)
            {
                var view = new MainView(settings, false);
                desktop.MainWindow = view;
                view.Closed += OnMainViewClosed;
            }
            else
            {
                var tempWindow = new Window();

                int ScaleScreenBound(int bound, double scaling)
                {
                    return (int)(bound / scaling);
                }

                foreach (var screen in tempWindow.Screens.All)
                {
                    var newMainView = new MainView(settings, false);
                    _views.Add(newMainView);
                    newMainView.Closed += OnMainViewClosed;

                    // position the view on the relevant screen
                    var bounds = screen.WorkingArea;
                    newMainView.Show(
                        bounds.X,
                        bounds.Y,
                        ScaleScreenBound(bounds.Width, screen.Scaling),
                        ScaleScreenBound(bounds.Height, screen.Scaling)
                    );
                }
            }
        }
    }

    private void OnMainViewClosed(object? sender, EventArgs e)
    {
        // if one view closes, close all of them and then exit
        foreach (var view in _views)
        {
            view.Closed -= OnMainViewClosed;
            view.Close();
        }
    }
}
