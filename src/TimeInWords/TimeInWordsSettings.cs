using Avalonia.Media;
using TimeToTextLib;

namespace TimeInWords;

public class TimeInWordsSettings
{
    public LanguagePreset.Language Language { get; set; } = LanguagePreset.Language.English;

    public Color BackgroundColour { get; set; } = Color.FromRgb(0, 0, 0);

    public Color ActiveFontColour { get; set; } = Color.FromRgb(255, 255, 255);

    public Color InactiveFontColour { get; set; } = Color.FromRgb(55, 55, 55);

    public bool Debug { get; set; }
}
