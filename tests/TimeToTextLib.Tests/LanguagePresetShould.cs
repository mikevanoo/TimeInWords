using TimeToTextLib.Presets;

namespace TimeToTextLib.Tests;

public class LanguagePresetShould
{
    [Fact]
    public void ThrowWhenGettingPresetForUnknownLanguage()
    {
        var act = () => LanguagePreset.Get((LanguagePreset.Language)999);

        act.Should()
            .ThrowExactly<ArgumentOutOfRangeException>()
            .WithMessage("Language not implemented*")
            .And.ParamName.Should()
            .Be("lang");
    }

    [Theory]
    [InlineData(LanguagePreset.Language.English, typeof(EnglishPreset))]
    [InlineData(LanguagePreset.Language.Dutch, typeof(DutchPreset))]
    [InlineData(LanguagePreset.Language.French, typeof(FrenchPreset))]
    [InlineData(LanguagePreset.Language.EnglishPrecise, typeof(EnglishPrecisePreset))]
    [InlineData(LanguagePreset.Language.GermanPrecise, typeof(GermanPrecisePreset))]
    public void GetCorrectPresetForGivenLanguage(LanguagePreset.Language language, Type expectedPreset)
    {
        var actual = LanguagePreset.Get(language);

        actual.Should().BeOfType(expectedPreset);
    }
}
