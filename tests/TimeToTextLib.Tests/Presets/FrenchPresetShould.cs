using System.Globalization;
using System.Text;
using TimeToTextLib.Presets;

namespace TimeToTextLib.Tests.Presets;

public class FrenchPresetShould(ITestOutputHelper testOutputHelper)
{
    private readonly FrenchPreset _preset = new();

    [Theory]
    [ClassData(typeof(FormatTimeToTextCorrectlyTheoryData))]
    public void FormatTimeToTextCorrectly(DateTime time, string expected) =>
        _preset.Format(time).ToString().Should().BeEquivalentTo(expected);

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

    private class FormatTimeToTextCorrectlyTheoryData : TheoryData<DateTime, string>
    {
        public FormatTimeToTextCorrectlyTheoryData()
        {
            Add(new DateTime(2024, 1, 1, 0, 0, 0), "IL EST MINUIT +0");
            Add(new DateTime(2024, 1, 1, 0, 1, 0), "IL EST MINUIT +1");
            Add(new DateTime(2024, 1, 1, 0, 4, 0), "IL EST MINUIT +4");
            Add(new DateTime(2024, 1, 1, 0, 5, 0), "IL EST MINUIT CINQ +0");
            Add(new DateTime(2024, 1, 1, 0, 29, 0), "IL EST MINUIT VINGT-CINQ +4");
            Add(new DateTime(2024, 1, 1, 0, 30, 0), "IL EST MINUIT ET DEMIE +0");
            Add(new DateTime(2024, 1, 1, 0, 31, 0), "IL EST MINUIT ET DEMIE +1");
            Add(new DateTime(2024, 1, 1, 0, 34, 0), "IL EST MINUIT ET DEMIE +4");
            Add(new DateTime(2024, 1, 1, 0, 35, 0), "IL EST UNE HEURE MOINS VINGT-CINQ +0");
            Add(new DateTime(2024, 1, 1, 1, 10, 0), "IL EST UNE HEURE DIX +0");
            Add(new DateTime(2024, 1, 1, 2, 15, 0), "IL EST DEUX HEURES ET QUART +0");
            Add(new DateTime(2024, 1, 1, 3, 20, 0), "IL EST TROIS HEURES VINGT +0");
            Add(new DateTime(2024, 1, 1, 4, 2, 0), "IL EST QUATRE HEURES +2");
            Add(new DateTime(2024, 1, 1, 4, 40, 0), "IL EST CINQ HEURES MOINS VINGT +0");
            Add(new DateTime(2024, 1, 1, 5, 3, 0), "IL EST CINQ HEURES +3");
            Add(new DateTime(2024, 1, 1, 5, 45, 0), "IL EST SIX HEURES MOINS LE QUART +0");
            Add(new DateTime(2024, 1, 1, 6, 50, 0), "IL EST SEPT HEURES MOINS DIX +0");
            Add(new DateTime(2024, 1, 1, 7, 55, 0), "IL EST HUIT HEURES MOINS CINQ +0");
            Add(new DateTime(2024, 1, 1, 9, 0, 0), "IL EST NEUF HEURES +0");
            Add(new DateTime(2024, 1, 1, 10, 0, 0), "IL EST DIX HEURES +0");
            Add(new DateTime(2024, 1, 1, 11, 0, 0), "IL EST ONZE HEURES +0");
            Add(new DateTime(2024, 1, 1, 11, 59, 0), "IL EST MIDI MOINS CINQ +4");
            Add(new DateTime(2024, 1, 1, 12, 0, 0), "IL EST MIDI +0");
            Add(new DateTime(2024, 1, 1, 12, 30, 0), "IL EST MIDI ET DEMIE +0");
            Add(new DateTime(2024, 1, 1, 12, 59, 0), "IL EST UNE HEURE MOINS CINQ +4");
        }
    }
}
