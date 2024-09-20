using Avalonia.Media;

namespace TimeInWords.Controls;

internal class LedLight : LedLetter
{
    public LedLight(TimeInWordsSettings settings)
        : base(settings, char.ConvertFromUtf32(9679))
    {
        Height = 100;
        Width = 100;
        FontFamily = new FontFamily("Arial Unicode MS");
        FontSize = 14;
    }
}
