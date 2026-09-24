using TimeInWords.Presenters;
using TimeInWords.Views;
using TimeToTextLib;

namespace TimeInWords.Tests.Presenters;

public sealed class SettingsEditorPresenterShould : IDisposable
{
    private readonly ISettingsEditorView _view = Substitute.For<ISettingsEditorView>();
    private readonly CancellationTokenSource _mainLoopCts = new();
    private readonly DirectoryInfo _directory = Directory.CreateTempSubdirectory("TimeInWords.Tests.");

    public void Dispose()
    {
        _mainLoopCts.Dispose();
        _directory.Delete(recursive: true);
    }

    [Fact]
    public void ReadSettingsFileAndPassToView()
    {
        var dutchSettingsFilePath = Path.Combine(AppContext.BaseDirectory, "./TestData/Settings_Dutch.json");

        _ = new SettingsEditorPresenter(_view, _mainLoopCts, dutchSettingsFilePath);

        _view.Received(1).Show(Arg.Is<TimeInWordsSettings>(s => s.Language == LanguagePreset.Language.Dutch));
    }

    [Fact]
    public void ReadSettingsFileContainingComments()
    {
        var filePath = GivenFile(
            """
            // written by hand
            {
              /* the clock face */
              "Language": "German"
            }
            """
        );

        _ = new SettingsEditorPresenter(_view, _mainLoopCts, filePath);

        _view.Received(1).Show(Arg.Is<TimeInWordsSettings>(s => s.Language == LanguagePreset.Language.German));
    }

    [Fact]
    public void UseDefaultSettingsWhenFileDoesNotExist()
    {
        var missingSettingsFilePath = SettingsFilePath();

        _ = new SettingsEditorPresenter(_view, _mainLoopCts, missingSettingsFilePath);

        TimeInWordsSettings expectedSettings = new();
        _view
            .Received(1)
            .Show(
                Arg.Is<TimeInWordsSettings>(s =>
                    s.Language == expectedSettings.Language
                    && s.BackgroundColour == expectedSettings.BackgroundColour
                    && s.ActiveFontColour == expectedSettings.ActiveFontColour
                    && s.InactiveFontColour == expectedSettings.InactiveFontColour
                    && s.Debug == expectedSettings.Debug
                )
            );
        File.Exists(missingSettingsFilePath).Should().BeFalse();
    }

    [Fact]
    public void SaveSettingsToFile()
    {
        var filePath = SettingsFilePath();
        _ = new SettingsEditorPresenter(_view, _mainLoopCts, filePath);

        _view.Saved += Raise.Event<EventHandler<TimeInWordsSettings>>(
            null,
            new TimeInWordsSettings { Language = LanguagePreset.Language.French }
        );

        // indented, with "\n" line endings whatever the platform, and only the settings that are not ignored
        File.ReadAllText(filePath).Should().Be("{\n  \"Language\": \"French\"\n}");
        _mainLoopCts.IsCancellationRequested.Should().BeFalse();
    }

    [Fact]
    public void ReplaceTheWholeFileWhenSaving()
    {
        var filePath = GivenFile(
            """
            {
              "Language": "SpanishPrecise",
              "Comment": "this file is longer than the one that replaces it"
            }
            """
        );
        _ = new SettingsEditorPresenter(_view, _mainLoopCts, filePath);

        _view.Saved += Raise.Event<EventHandler<TimeInWordsSettings>>(
            null,
            new TimeInWordsSettings { Language = LanguagePreset.Language.French }
        );

        File.ReadAllText(filePath).Should().Be("{\n  \"Language\": \"French\"\n}");
    }

    [Fact]
    public void ExitWhenTheViewCloses()
    {
        _ = new SettingsEditorPresenter(_view, _mainLoopCts, SettingsFilePath());

        _view.Closed += Raise.Event();

        _mainLoopCts.IsCancellationRequested.Should().BeTrue();
    }

    private string SettingsFilePath() => Path.Combine(_directory.FullName, "appsettings.json");

    private string GivenFile(string contents)
    {
        var filePath = SettingsFilePath();
        File.WriteAllText(filePath, contents);
        return filePath;
    }
}
