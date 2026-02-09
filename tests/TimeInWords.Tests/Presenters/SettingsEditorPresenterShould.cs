using TimeInWords.Presenters;
using TimeInWords.Views;
using TimeToTextLib;

namespace TimeInWords.Tests.Presenters;

public class SettingsEditorPresenterShould
{
    [Fact]
    public void ReadSettingsFileAndPassToView()
    {
        var view = Substitute.For<ISettingsEditorView>();
        var dutchSettingsFilePath = Path.Combine(AppContext.BaseDirectory, "./TestData/Settings_Dutch.json");

        _ = new SettingsEditorPresenter(view, new CancellationTokenSource(), dutchSettingsFilePath);

        view.Received(1).Show(Arg.Is<TimeInWordsSettings>(s => s.Language == LanguagePreset.Language.Dutch));
    }

    [Fact]
    public void UseDefaultSettingsWhenFileDoesNotExist()
    {
        var view = Substitute.For<ISettingsEditorView>();
        var missingSettingsFilePath = Path.Combine(
            AppContext.BaseDirectory,
            "./TestData/this_file_does_not_exist.json"
        );

        _ = new SettingsEditorPresenter(view, new CancellationTokenSource(), missingSettingsFilePath);

        TimeInWordsSettings expectedSettings = new();
        view.Received(1)
            .Show(
                Arg.Is<TimeInWordsSettings>(s =>
                    s.Language == expectedSettings.Language
                    && s.BackgroundColour == expectedSettings.BackgroundColour
                    && s.ActiveFontColour == expectedSettings.ActiveFontColour
                    && s.InactiveFontColour == expectedSettings.InactiveFontColour
                    && s.Debug == expectedSettings.Debug
                )
            );
    }

    [Fact]
    public void SaveSettingsToFile()
    {
        var view = Substitute.For<ISettingsEditorView>();
        var tempFile = Path.Combine(Path.GetTempPath(), $"test_{Guid.NewGuid()}.json");
        _ = new SettingsEditorPresenter(view, new CancellationTokenSource(), tempFile);

        var settingsFrench = new TimeInWordsSettings { Language = LanguagePreset.Language.French };
        view.Saved += Raise.Event<EventHandler<TimeInWordsSettings>>(null, settingsFrench);

        File.Exists(tempFile).Should().BeTrue();
        var actualSettings = File.ReadAllText(tempFile);
        var expectedSettings = File.ReadAllText(
            Path.Combine(AppContext.BaseDirectory, "./TestData/Settings_French.json")
        );
        actualSettings.Should().Be(expectedSettings);
    }
}
