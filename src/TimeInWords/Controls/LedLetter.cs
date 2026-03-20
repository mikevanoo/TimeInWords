using System;
using Avalonia.Controls;
using Avalonia.Media;

namespace TimeInWords.Controls;

internal class LedLetter : TextBlock, IFadeableControl
{
    private readonly TimeInWordsSettings _settings;

    public bool Active
    {
        get;
        set
        {
            if (field != value)
            {
                var endColor = value ? _settings.ActiveFontColour : _settings.InactiveFontColour;

                field = value;

                ColorFader.SetControlForeColor(this, endColor);
            }
        }
    }

    public LedLetter(TimeInWordsSettings settings, string text)
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));

        FontFamily = new FontFamily("Sans-Serif");
        FontSize = 37.5f;
        TextAlignment = TextAlignment.Center;
        Text = text;
        Foreground = new SolidColorBrush(_settings.InactiveFontColour);
        Background = new SolidColorBrush(_settings.BackgroundColour);
    }
}
