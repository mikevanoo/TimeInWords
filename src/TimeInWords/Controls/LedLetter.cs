using System;
using Avalonia.Controls;
using Avalonia.Media;

namespace TimeInWords.Controls;

internal class LedLetter : TextBlock, IFadeableControl
{
    private readonly TimeInWordsSettings _settings = null!;
    private bool? _active;

    public bool? Active
    {
        get => _active;
        set
        {
            if (_active != value)
            {
                var endColor =
                    value.HasValue && value.Value ? _settings.ActiveFontColour : _settings.InactiveFontColour;

                _active = value;

                ColorFader.SetControlForeColor(this, endColor);
            }
        }
    }

    public LedLetter() { }

    public LedLetter(TimeInWordsSettings settings, string text)
        : this()
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));

        Height = 75;
        Width = 75;
        FontFamily = new FontFamily("Sans-Serif");
        FontSize = 37.5f;
        TextAlignment = TextAlignment.Center;
        Text = text;
        Foreground = new SolidColorBrush(_settings.InactiveFontColour);
        Background = new SolidColorBrush(_settings.BackgroundColour);
    }
}
