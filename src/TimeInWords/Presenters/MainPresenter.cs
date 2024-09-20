using System;
using System.Collections.Generic;
using System.Threading;
using Avalonia.Controls;
using TimeInWords.Views;

namespace TimeInWords.Presenters;

public class MainPresenter
{
    private readonly CancellationTokenSource _mainLoopCts;
    private readonly List<IMainView> _views = [];

    public MainPresenter(
        TimeInWordsSettings settings,
        IMainViewFactory viewFactory,
        CancellationTokenSource mainLoopCts
    )
    {
        _mainLoopCts = mainLoopCts;

        if (settings.Debug)
        {
            var view = viewFactory.Create(settings, false);
            view.Closed += OnMainViewClosed;
            view.Show();
        }
        else
        {
            int ScaleScreenBound(int bound, double scaling)
            {
                return (int)(bound / scaling);
            }

            var tempWindow = new Window();
            foreach (var screen in tempWindow.Screens.All)
            {
                var newMainView = viewFactory.Create(settings, true);
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

    private void OnMainViewClosed(object? sender, EventArgs e)
    {
        // if one view closes, close all of them and then signal exit
        foreach (var view in _views)
        {
            view.Closed -= OnMainViewClosed;
            view.Close();
        }
        _mainLoopCts.Cancel();
    }
}
