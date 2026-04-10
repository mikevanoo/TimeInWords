using TextToTimeGridLib.Grids;
using TimeToTextLib;

namespace TextToTimeGridLib.Tests.Grids;

public class TimeGridSpanishPreciseShould : BaseTimeGridShould<TimeGridSpanishPrecise>
{
    protected override LanguagePreset.Language Language => LanguagePreset.Language.SpanishPrecise;
}
