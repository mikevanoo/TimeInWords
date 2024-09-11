using System;
using System.Drawing;
using TimeToTextLib;

namespace TimeInWordsApp;

public class TimeInWordsSettings
{
    public LanguagePreset.Language Language { get; set; } = LanguagePreset.Language.English;

    public Color BackgroundColour { get; set; } = Color.Black;

    public Color ActiveFontColour { get; set; } = Color.White;

    public Color InactiveFontColour { get; set; } = Color.FromArgb(55, 55, 55);

    public bool Debug { get; set; }

    public static bool IsRunningOnMono() => Type.GetType("Mono.Runtime") != null;
}
