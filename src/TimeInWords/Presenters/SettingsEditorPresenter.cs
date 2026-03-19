using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using TimeInWords.Views;

namespace TimeInWords.Presenters;

public class SettingsEditorPresenter
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new() { WriteIndented = true, NewLine = "\n" };

    private readonly string _filePath;

    public SettingsEditorPresenter(ISettingsEditorView settingsView, CancellationTokenSource mainLoopCts)
        : this(settingsView, mainLoopCts, Path.Combine(AppContext.BaseDirectory, "appsettings.json")) { }

    public SettingsEditorPresenter(
        ISettingsEditorView settingsView,
        CancellationTokenSource mainLoopCts,
        string filePath
    )
    {
        _filePath = filePath;
        settingsView.Closed += (sender, args) => mainLoopCts.Cancel();
        settingsView.Saved += (sender, settings) => SaveSettings(settings);

        TimeInWordsSettings? settings = null;
        if (File.Exists(_filePath))
        {
            using var fileStream = File.OpenRead(_filePath);
            settings = JsonSerializer.Deserialize<TimeInWordsSettings>(fileStream, _jsonSerializerOptions);
        }

        settingsView.Show(settings ?? new TimeInWordsSettings());
    }

    private void SaveSettings(TimeInWordsSettings settings)
    {
        using var fileStream = File.Create(_filePath);
        JsonSerializer.Serialize(fileStream, settings, _jsonSerializerOptions);
    }
}
