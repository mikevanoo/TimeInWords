using System.Globalization;
using System.Text;
using TimeToTextLib.Presets;

namespace TimeToTextLib.Tests.Presets;

public class FrenchPrecisePresetShould(ITestOutputHelper testOutputHelper)
{
    private readonly FrenchPrecisePreset _preset = new();

    [Theory]
    [ClassData(typeof(FormatTimeToTextCorrectlyTheoryData))]
    public void FormatTimeToTextCorrectly(DateTime time, string expected) =>
        _preset.Format(time).ToString().Should().BeEquivalentTo(expected);

    [Fact]
    public void ThrowWhenAskedForTheFifteenMinuteWord() =>
        new NumberTextAccessor()
            .Invoking(accessor => accessor.Number(15))
            .Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithMessage("No word form for this number*");

#pragma warning disable xUnit1004
    [Fact(Skip = "test code generator")]
#pragma warning restore xUnit1004
    public void GenerateTheoryData()
    {
        var result = new StringBuilder();
        var time = new DateTime(2024, 1, 1, 0, 0, 0);
        while (time.Hour < 13)
        {
            var timeAsText = _preset.Format(time);
            result.AppendLine(
                CultureInfo.InvariantCulture,
                $"Add(new DateTime(2024, 1, 1, {time.Hour}, {time.Minute}, 0), \"{timeAsText}\");"
            );
            time = time.AddMinutes(1);
        }

        testOutputHelper.WriteLine(result.ToString());
    }

    private sealed class NumberTextAccessor : FrenchPrecisePreset
    {
        public string Number(int number) => GetNumberText(number);
    }

    private class FormatTimeToTextCorrectlyTheoryData : TheoryData<DateTime, string>
    {
        public FormatTimeToTextCorrectlyTheoryData()
        {
            Add(new DateTime(2024, 1, 1, 0, 0, 0), "IL EST MINUIT +0");
            Add(new DateTime(2024, 1, 1, 0, 1, 0), "IL EST MINUIT UNE +0");
            Add(new DateTime(2024, 1, 1, 0, 4, 0), "IL EST MINUIT QUATRE +0");
            Add(new DateTime(2024, 1, 1, 0, 5, 0), "IL EST MINUIT CINQ +0");
            Add(new DateTime(2024, 1, 1, 0, 14, 0), "IL EST MINUIT QUATORZE +0");
            Add(new DateTime(2024, 1, 1, 0, 15, 0), "IL EST MINUIT ET QUART +0");
            Add(new DateTime(2024, 1, 1, 0, 16, 0), "IL EST MINUIT SEIZE +0");
            Add(new DateTime(2024, 1, 1, 0, 29, 0), "IL EST MINUIT VINGT NEUF +0");
            Add(new DateTime(2024, 1, 1, 0, 30, 0), "IL EST MINUIT ET DEMI +0");
            Add(new DateTime(2024, 1, 1, 0, 31, 0), "IL EST UNE HEURE MOINS VINGT NEUF +0");
            Add(new DateTime(2024, 1, 1, 0, 34, 0), "IL EST UNE HEURE MOINS VINGT SIX +0");
            Add(new DateTime(2024, 1, 1, 0, 35, 0), "IL EST UNE HEURE MOINS VINGT CINQ +0");
            Add(new DateTime(2024, 1, 1, 0, 44, 0), "IL EST UNE HEURE MOINS SEIZE +0");
            Add(new DateTime(2024, 1, 1, 0, 45, 0), "IL EST UNE HEURE MOINS LE QUART +0");
            Add(new DateTime(2024, 1, 1, 0, 46, 0), "IL EST UNE HEURE MOINS QUATORZE +0");
            Add(new DateTime(2024, 1, 1, 0, 55, 0), "IL EST UNE HEURE MOINS CINQ +0");
            Add(new DateTime(2024, 1, 1, 0, 59, 0), "IL EST UNE HEURE MOINS UNE +0");
            Add(new DateTime(2024, 1, 1, 1, 12, 0), "IL EST UNE HEURE DOUZE +0");
            Add(new DateTime(2024, 1, 1, 2, 0, 0), "IL EST DEUX HEURES +0");
            Add(new DateTime(2024, 1, 1, 2, 13, 0), "IL EST DEUX HEURES TREIZE +0");
            Add(new DateTime(2024, 1, 1, 3, 0, 0), "IL EST TROIS HEURES +0");
            Add(new DateTime(2024, 1, 1, 3, 30, 0), "IL EST TROIS HEURES ET DEMIE +0");
            Add(new DateTime(2024, 1, 1, 3, 43, 0), "IL EST QUATRE HEURES MOINS DIX SEPT +0");
            Add(new DateTime(2024, 1, 1, 4, 18, 0), "IL EST QUATRE HEURES DIX HUIT +0");
            Add(new DateTime(2024, 1, 1, 5, 32, 0), "IL EST SIX HEURES MOINS VINGT HUIT +0");
            Add(new DateTime(2024, 1, 1, 5, 41, 0), "IL EST SIX HEURES MOINS DIX NEUF +0");
            Add(new DateTime(2024, 1, 1, 6, 0, 0), "IL EST SIX HEURES +0");
            Add(new DateTime(2024, 1, 1, 6, 20, 0), "IL EST SIX HEURES VINGT +0");
            Add(new DateTime(2024, 1, 1, 7, 0, 0), "IL EST SEPT HEURES +0");
            Add(new DateTime(2024, 1, 1, 7, 21, 0), "IL EST SEPT HEURES VINGT ET UNE +0");
            Add(new DateTime(2024, 1, 1, 8, 0, 0), "IL EST HUIT HEURES +0");
            Add(new DateTime(2024, 1, 1, 8, 38, 0), "IL EST NEUF HEURES MOINS VINGT DEUX +0");
            Add(new DateTime(2024, 1, 1, 9, 0, 0), "IL EST NEUF HEURES +0");
            Add(new DateTime(2024, 1, 1, 9, 23, 0), "IL EST NEUF HEURES VINGT TROIS +0");
            Add(new DateTime(2024, 1, 1, 10, 0, 0), "IL EST DIX HEURES +0");
            Add(new DateTime(2024, 1, 1, 10, 36, 0), "IL EST ONZE HEURES MOINS VINGT QUATRE +0");
            Add(new DateTime(2024, 1, 1, 11, 0, 0), "IL EST ONZE HEURES +0");
            Add(new DateTime(2024, 1, 1, 11, 27, 0), "IL EST ONZE HEURES VINGT SEPT +0");
            Add(new DateTime(2024, 1, 1, 11, 59, 0), "IL EST MIDI MOINS UNE +0");
            Add(new DateTime(2024, 1, 1, 12, 0, 0), "IL EST MIDI +0");
            Add(new DateTime(2024, 1, 1, 12, 30, 0), "IL EST MIDI ET DEMI +0");
            Add(new DateTime(2024, 1, 1, 12, 59, 0), "IL EST UNE HEURE MOINS UNE +0");
        }
    }
}
