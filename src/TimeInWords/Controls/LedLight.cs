using Avalonia.Media;

namespace TimeInWords.Controls;

internal class LedLight : LedLetter
{
    public LedLight(TimeInWordsSettings settings)
        : base(settings, char.ConvertFromUtf32(9679))
    {
        Height = 25;
        Width = 25;
        FontFamily = new FontFamily("Arial Unicode MS");
        FontSize = 18;
    }
}
