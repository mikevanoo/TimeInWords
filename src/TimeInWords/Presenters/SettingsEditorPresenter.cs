using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using TimeInWords.Views;

namespace TimeInWords.Presenters;

public class SettingsEditorPresenter
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new() { WriteIndented = true };

    public SettingsEditorPresenter(ISettingsEditorView settingsView, CancellationTokenSource mainLoopCts)
    {
        settingsView.Closed += (sender, args) => mainLoopCts.Cancel();
        settingsView.Saved += (sender, settings) => SaveSettings(settings);

        using var fileStream = File.OpenRead(GetFilePath());
        var settings = JsonSerializer.Deserialize<TimeInWordsSettings>(fileStream, _jsonSerializerOptions);

        settingsView.Show(settings ?? new TimeInWordsSettings());
    }

    private void SaveSettings(TimeInWordsSettings settings)
    {
        using var fileStream = File.Create(GetFilePath());
        JsonSerializer.Serialize(fileStream, settings, _jsonSerializerOptions);
    }

    private static string GetFilePath() => Path.Combine(AppContext.BaseDirectory, "appsettings.json");
}
