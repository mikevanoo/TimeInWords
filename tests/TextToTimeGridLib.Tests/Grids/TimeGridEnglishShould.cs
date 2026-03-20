using TextToTimeGridLib.Grids;
using TimeToTextLib;

namespace TextToTimeGridLib.Tests.Grids;

public class TimeGridEnglishShould : BaseTimeGridShould<TimeGridEnglish>
{
    protected override LanguagePreset.Language Language => LanguagePreset.Language.English;
}
