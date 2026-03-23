using TextToTimeGridLib.Grids;
using TimeToTextLib;

namespace TextToTimeGridLib.Tests.Grids;

public class TimeGridFrenchPreciseShould : BaseTimeGridShould<TimeGridFrenchPrecise>
{
    protected override LanguagePreset.Language Language => LanguagePreset.Language.FrenchPrecise;
}
