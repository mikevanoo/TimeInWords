using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using TimeToTextLib;

namespace TimeInWords.Views;

public partial class SettingsEditorView : Window, ISettingsEditorView
{
    public TimeInWordsSettings Settings { get; private set; } = new();

    public event EventHandler<TimeInWordsSettings>? Saved;

    public SettingsEditorView()
    {
        InitializeComponent();
    }

    public void Show(TimeInWordsSettings settings)
    {
        Settings = settings;

        LanguageCombo.ItemsSource = Enum.GetValues<LanguagePreset.Language>();
        LanguageCombo.SelectedValue = Settings.Language;

        Show();
    }

    private void LanguageCombo_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (LanguageCombo.SelectedValue != null)
        {
            Settings.Language = (LanguagePreset.Language)LanguageCombo.SelectedValue;
        }
    }

    private void OnSaveButtonClick(object? sender, RoutedEventArgs e)
    {
        Saved?.Invoke(this, Settings);
        Close();
    }

    private void OnExitButtonClick(object? sender, RoutedEventArgs e) => Close();
}
