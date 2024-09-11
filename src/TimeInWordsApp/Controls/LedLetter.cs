using System;
using System.Drawing;
using System.Windows.Forms;

namespace TimeInWordsApp.Controls;

internal class LedLetter : Label, IFadeableControl
{
    private readonly TimeInWordsSettings _settings;
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

        Height = 50;
        Width = 50;
        Font = new Font(FontFamily.GenericSansSerif, 25);
        TextAlign = ContentAlignment.MiddleCenter;
        Text = text;
        ForeColor = _settings.InactiveFontColour;
        BackColor = _settings.BackgroundColour;
    }
}
