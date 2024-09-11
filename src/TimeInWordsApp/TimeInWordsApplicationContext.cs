using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TimeInWordsApp.Views;

namespace TimeInWordsApp;

internal class TimeInWordsApplicationContext : ApplicationContext
{
    private readonly List<Form> _forms = new();

    public TimeInWordsApplicationContext(TimeInWordsSettings settings, bool showFullScreen = false)
    {
        foreach (var screen in Screen.AllScreens)
        {
            var mainView = new MainView(settings, showFullScreen);
            _forms.Add(mainView);
            mainView.Closed += OnMainViewClosed;

            // position the main view on the relevant screen
            mainView.StartPosition = FormStartPosition.Manual;
            var bounds = screen.Bounds;
            mainView.SetBounds(bounds.X, bounds.Y, bounds.Width, bounds.Height);
            mainView.Show();
        }
    }

    private void OnMainViewClosed(object sender, EventArgs e)
    {
        // if one main view closes, close all of them and then exit
        foreach (var form in _forms)
        {
            form.Closed -= OnMainViewClosed;
            form.Close();
        }
        ExitThread();
    }
}
