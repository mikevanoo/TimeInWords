using System.Text.Json.Serialization;
using Avalonia.Media;
using TimeToTextLib;

namespace TimeInWords;

public class TimeInWordsSettings
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public LanguagePreset.Language Language { get; set; } = LanguagePreset.Language.English;

    [JsonIgnore]
    public Color BackgroundColour { get; set; } = Color.FromRgb(0, 0, 0);

    [JsonIgnore]
    public Color ActiveFontColour { get; set; } = Color.FromRgb(255, 255, 255);

    [JsonIgnore]
    public Color InactiveFontColour { get; set; } = Color.FromRgb(66, 66, 66);

    [JsonIgnore]
    public bool Debug { get; set; }
}
