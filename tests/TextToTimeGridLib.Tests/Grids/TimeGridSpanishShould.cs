using TextToTimeGridLib.Grids;
using TimeToTextLib;

namespace TextToTimeGridLib.Tests.Grids;

public class TimeGridSpanishShould : BaseTimeGridShould<TimeGridSpanish>
{
    protected override LanguagePreset.Language Language => LanguagePreset.Language.Spanish;
}
