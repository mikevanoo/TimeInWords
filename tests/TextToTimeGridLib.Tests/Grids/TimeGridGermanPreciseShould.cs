using TextToTimeGridLib.Grids;
using TimeToTextLib;

namespace TextToTimeGridLib.Tests.Grids;

public class TimeGridGermanPreciseShould : BaseTimeGridShould<TimeGridGermanPrecise>
{
    protected override LanguagePreset.Language Language => LanguagePreset.Language.GermanPrecise;
}
