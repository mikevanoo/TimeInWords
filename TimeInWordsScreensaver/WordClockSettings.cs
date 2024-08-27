using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeToTextLib;

namespace TimeInWordsScreensaver
{
    public class WordClockSettings
    {
        public LanguagePreset.Language Language { get; set; } = LanguagePreset.Language.English;

        public Color BackgroundColour { get; set; } = Color.Black;

        public Color ActiveFontColour { get; set; } = Color.White;

        public Color InactiveFontColour { get; set; } = Color.FromArgb(55, 55, 55);

        public bool Debug { get; set; } = false;
        
        public bool IsRunningOnMono ()
        {
            return Type.GetType("Mono.Runtime") != null;
        }
    }
}
