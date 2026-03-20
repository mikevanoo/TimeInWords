using TextToTimeGridLib.Grids;
using TimeToTextLib;

namespace TextToTimeGridLib.Tests.Grids;

public class TimeGridDutchShould : BaseTimeGridShould<TimeGridDutch>
{
    protected override LanguagePreset.Language Language => LanguagePreset.Language.Dutch;
}
