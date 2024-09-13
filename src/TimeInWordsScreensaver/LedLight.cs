using System.Drawing;

namespace TimeInWordsScreensaver
{
    internal class LedLight : LedLetter
    {
        public LedLight(WordClockSettings settings)
            : base(settings, char.ConvertFromUtf32(9679))
        {
            Height = 100;
            Width = 100;
            Font = new Font("Arial Unicode MS", 14);
        }
    }
}
