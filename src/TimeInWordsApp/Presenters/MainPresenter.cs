using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TimeInWordsApp.Views;

namespace TimeInWordsApp.Presenters;

public class MainPresenter : ApplicationContext
{
    private readonly List<IMainView> _views = new();

    public MainPresenter(TimeInWordsSettings settings, IMainViewFactory viewFactory)
    {
        if (settings.Debug)
        {
            var view = viewFactory.Create(settings, false);
            view.Closed += OnMainViewClosed;
            view.Show();
        }
        else
        {
            foreach (var screen in Screen.AllScreens)
            {
                var newMainView = viewFactory.Create(settings, true);
                _views.Add(newMainView);
                newMainView.Closed += OnMainViewClosed;

                // position the view on the relevant screen
                var bounds = screen.Bounds;
                newMainView.Show(bounds.X, bounds.Y, bounds.Width, bounds.Height);
            }
        }
    }

    private void OnMainViewClosed(object sender, EventArgs e)
    {
        // if one view closes, close all of them and then exit
        foreach (var view in _views)
        {
            view.Closed -= OnMainViewClosed;
            view.Close();
        }
        ExitThread();
    }
}
