using System.Drawing;

namespace TimeInWordsApp.Controls;

internal class LedLight : LedLetter
{
    public LedLight(TimeInWordsSettings settings)
        : base(settings, char.ConvertFromUtf32(9679))
    {
        Height = 100;
        Width = 100;
        Font = new Font("Arial Unicode MS", 10);
    }
}
