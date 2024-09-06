namespace TimeToTextLib.Tests.Presets;

public class TimeToTextShould
{
    [Fact]
    public void ReturnEnglishTimeToText()
    {
        var actual = TimeToText.GetSimple(LanguagePreset.Language.English, new DateTime(2024, 1, 1, 12, 7, 0));

        var expected = new TimeToTextFormat { TimeAsText = "IT IS FIVE PAST TWELVE", AdditionalMinutes = 2 };

        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void ReturnDutchTimeToText()
    {
        var actual = TimeToText.GetSimple(LanguagePreset.Language.Dutch, new DateTime(2024, 1, 1, 12, 7, 0));

        var expected = new TimeToTextFormat { TimeAsText = "HET IS VIJF OVER TWAALF", AdditionalMinutes = 2 };

        actual.Should().BeEquivalentTo(expected);
    }
}
