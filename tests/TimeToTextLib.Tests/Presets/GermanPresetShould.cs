using System.Globalization;
using System.Text;
using TimeToTextLib.Presets;
using Xunit.Abstractions;

namespace TimeToTextLib.Tests.Presets;

public class GermanPresetShould(ITestOutputHelper testOutputHelper)
{
    private readonly GermanPreset _preset = new();

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
            Add(new DateTime(2024, 1, 1, 0, 0, 0), "ES IST ZWÖLF UHR +0");
            Add(new DateTime(2024, 1, 1, 0, 1, 0), "ES IST ZWÖLF UHR +1");
            Add(new DateTime(2024, 1, 1, 0, 4, 0), "ES IST ZWÖLF UHR +4");
            Add(new DateTime(2024, 1, 1, 0, 5, 0), "ES IST FÜNF NACH ZWÖLF +0");
            Add(new DateTime(2024, 1, 1, 0, 29, 0), "ES IST FÜNF VOR HALB EINS +4");
            Add(new DateTime(2024, 1, 1, 0, 30, 0), "ES IST HALB EINS +0");
            Add(new DateTime(2024, 1, 1, 0, 31, 0), "ES IST HALB EINS +1");
            Add(new DateTime(2024, 1, 1, 0, 34, 0), "ES IST HALB EINS +4");
            Add(new DateTime(2024, 1, 1, 0, 35, 0), "ES IST FÜNF NACH HALB EINS +0");
            Add(new DateTime(2024, 1, 1, 1, 0, 0), "ES IST EIN UHR +0");
            Add(new DateTime(2024, 1, 1, 2, 10, 0), "ES IST ZEHN NACH ZWEI +0");
            Add(new DateTime(2024, 1, 1, 3, 15, 0), "ES IST VIERTEL NACH DREI +0");
            Add(new DateTime(2024, 1, 1, 4, 2, 0), "ES IST VIER UHR +2");
            Add(new DateTime(2024, 1, 1, 5, 3, 0), "ES IST FÜNF UHR +3");
            Add(new DateTime(2024, 1, 1, 5, 20, 0), "ES IST ZWANZIG NACH FÜNF +0");
            Add(new DateTime(2024, 1, 1, 5, 40, 0), "ES IST ZWANZIG VOR SECHS +0");
            Add(new DateTime(2024, 1, 1, 6, 45, 0), "ES IST VIERTEL VOR SIEBEN +0");
            Add(new DateTime(2024, 1, 1, 7, 50, 0), "ES IST ZEHN VOR ACHT +0");
            Add(new DateTime(2024, 1, 1, 8, 55, 0), "ES IST FÜNF VOR NEUN +0");
            Add(new DateTime(2024, 1, 1, 10, 0, 0), "ES IST ZEHN UHR +0");
            Add(new DateTime(2024, 1, 1, 11, 0, 0), "ES IST ELF UHR +0");
            Add(new DateTime(2024, 1, 1, 11, 59, 0), "ES IST FÜNF VOR ZWÖLF +4");
            Add(new DateTime(2024, 1, 1, 12, 0, 0), "ES IST ZWÖLF UHR +0");
            Add(new DateTime(2024, 1, 1, 12, 30, 0), "ES IST HALB EINS +0");
            Add(new DateTime(2024, 1, 1, 12, 59, 0), "ES IST FÜNF VOR EINS +4");
        }
    }
}
