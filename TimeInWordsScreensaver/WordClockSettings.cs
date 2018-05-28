using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeInWordsScreensaver
{
    internal class WordClockSettings
    {
        public Color BackgroundColour { get; set; } = Color.Black;

        public Color ActiveFontColour { get; set; } = Color.White;

        public Color InactiveFontColour { get; set; } = Color.Silver;
    }
}
