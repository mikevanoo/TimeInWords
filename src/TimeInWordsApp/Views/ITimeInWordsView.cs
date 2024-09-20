using System;
using TextToTimeGridLib;
using TimeToTextLib;

namespace TimeInWordsApp.Views;

public interface ITimeInWordsView
{
    public DateTime Time { get; set; }
    public TimeToTextFormat TimeAsText { get; set; }
    public bool[][] GridBitMask { get; set; }

    void Initialise(TimeInWordsSettings settings, TimeGrid grid);
    void Update(bool force = false);
}
