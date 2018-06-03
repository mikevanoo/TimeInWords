using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeInWordsScreensaver
{
    internal sealed class LedLetter : Label
    {
        private readonly WordClockSettings _settings;
        private bool? _active;

        public bool? Active
        {
            get => _active;
            set
            {
                if (_active != value)
                {
                    _active = value;
                    ForeColor = _active.HasValue && _active.Value ? _settings.ActiveFontColour : _settings.InactiveFontColour;
                }
            }
        }

        public LedLetter()
            : base()
        {
        }

        public LedLetter(WordClockSettings settings, string text)
            : this()
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));

            Height = 50;
            Width = 50;
            Font = new Font(FontFamily.GenericSansSerif, 25);
            TextAlign = ContentAlignment.MiddleCenter;
            Text = text;
            ForeColor = _settings.ActiveFontColour;
            BackColor = _settings.BackgroundColour;
        }
    }
}
