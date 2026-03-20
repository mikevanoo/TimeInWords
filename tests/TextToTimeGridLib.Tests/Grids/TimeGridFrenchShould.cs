using TextToTimeGridLib.Grids;
using TimeToTextLib;

namespace TextToTimeGridLib.Tests.Grids;

public class TimeGridFrenchShould : BaseTimeGridShould<TimeGridFrench>
{
    protected override LanguagePreset.Language Language => LanguagePreset.Language.French;
}
