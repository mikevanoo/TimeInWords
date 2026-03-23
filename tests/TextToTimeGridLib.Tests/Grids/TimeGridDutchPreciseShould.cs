using TextToTimeGridLib.Grids;
using TimeToTextLib;

namespace TextToTimeGridLib.Tests.Grids;

public class TimeGridDutchPreciseShould : BaseTimeGridShould<TimeGridDutchPrecise>
{
    protected override LanguagePreset.Language Language => LanguagePreset.Language.DutchPrecise;
}
