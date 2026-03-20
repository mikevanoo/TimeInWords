using TextToTimeGridLib.Grids;
using TimeToTextLib;

namespace TextToTimeGridLib.Tests.Grids;

public class TimeGridEnglishPreciseShould : BaseTimeGridShould<TimeGridEnglishPrecise>
{
    protected override LanguagePreset.Language Language => LanguagePreset.Language.EnglishPrecise;
}
