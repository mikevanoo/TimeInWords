using System;
using System.Threading;
using TimeInWords.Views;

namespace TimeInWords.Presenters;

public class SettingsEditorPresenter
{
    public SettingsEditorPresenter(ISettingsEditorView settingsView, CancellationTokenSource mainLoopCts)
    {
        settingsView.Closed += (sender, args) => mainLoopCts.Cancel();
        settingsView.Saved += (sender, settings) => SaveSettings(settings);

        // TODO read settings
        var settings = new TimeInWordsSettings();

        settingsView.Show(settings);
    }

    private void SaveSettings(TimeInWordsSettings settings)
    {
        Console.WriteLine(settings.Language);
    }
}
