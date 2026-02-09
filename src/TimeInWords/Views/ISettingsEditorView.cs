using System;

namespace TimeInWords.Views;

public interface ISettingsEditorView
{
    public void Show(TimeInWordsSettings settings);
    public event EventHandler Closed;
    public event EventHandler<TimeInWordsSettings> Saved;
}
