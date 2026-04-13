using TextToTimeGridLib.Grids;
using TimeToTextLib;

namespace TextToTimeGridLib.Tests.Grids;

public class TimeGridGermanShould : BaseTimeGridShould<TimeGridGerman>
{
    protected override LanguagePreset.Language Language => LanguagePreset.Language.German;
}
