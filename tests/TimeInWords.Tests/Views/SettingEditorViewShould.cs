using Avalonia;
using Avalonia.Controls;
using Avalonia.Headless;
using Avalonia.Headless.XUnit;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Threading;
using TimeInWords.Views;
using TimeToTextLib;

namespace TimeInWords.Tests.Views;

public class SettingsEditorViewShould
{
    [AvaloniaFact]
    public void ShowTheView()
    {
        var view = new SettingsEditorView();

        view.Show(new TimeInWordsSettings());

        view.IsVisible.Should().BeTrue();
    }

    [AvaloniaFact]
    public void PopulateTheLanguageCombo()
    {
        var view = new SettingsEditorView();
        var settings = new TimeInWordsSettings();

        view.Show(settings);

        view.LanguageCombo.ItemsSource.Should().BeEquivalentTo(Enum.GetValues<LanguagePreset.Language>());
        view.LanguageCombo.SelectedValue.Should().Be(settings.Language);
    }

    [AvaloniaFact]
    public void SetLanguageWhenLanguageComboChanges()
    {
        var view = new SettingsEditorView();
        view.Show(new TimeInWordsSettings());
        var newLanguage = LanguagePreset.Language.Dutch;

        view.LanguageCombo.SelectedValue = newLanguage;

        view.Settings.Language.Should().Be(newLanguage);
    }

    [AvaloniaFact]
    public void RaiseSavedAndClosedEventsWhenSaveButtonClicked()
    {
        var view = new SettingsEditorView();
        using var monitoredView = view.Monitor<ISettingsEditorView>();
        var settings = new TimeInWordsSettings();
        view.Show(settings);

        view.SaveButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

        monitoredView
            .Should()
            .Raise("Saved")
            .WithArgs<TimeInWordsSettings>(actualSettings => actualSettings.Language == settings.Language);
        monitoredView.Should().Raise("Closed");
    }

    [AvaloniaFact]
    public void RaiseClosedEventWhenExitButtonClicked()
    {
        var view = new SettingsEditorView();
        using var monitoredView = view.Monitor<ISettingsEditorView>();
        view.Show(new TimeInWordsSettings());

        view.ExitButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

        monitoredView.Should().Raise("Closed");
    }
}
