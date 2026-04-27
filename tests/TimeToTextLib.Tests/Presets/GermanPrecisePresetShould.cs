using System.Globalization;
using System.Text;
using TimeToTextLib.Presets;

namespace TimeToTextLib.Tests.Presets;

public class GermanPrecisePresetShould(ITestOutputHelper testOutputHelper)
{
    private readonly GermanPrecisePreset _preset = new();

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
            Add(new DateTime(2024, 1, 1, 0, 1, 0), "ES IST EINE MINUTE NACH ZWÖLF +0");
            Add(new DateTime(2024, 1, 1, 0, 4, 0), "ES IST VIER MINUTEN NACH ZWÖLF +0");
            Add(new DateTime(2024, 1, 1, 0, 5, 0), "ES IST FÜNF MINUTEN NACH ZWÖLF +0");
            Add(new DateTime(2024, 1, 1, 0, 14, 0), "ES IST VIERZEHN MINUTEN NACH ZWÖLF +0");
            Add(new DateTime(2024, 1, 1, 0, 15, 0), "ES IST VIERTEL NACH ZWÖLF +0");
            Add(new DateTime(2024, 1, 1, 0, 16, 0), "ES IST VIERZEHN MINUTEN VOR HALB EINS +0");
            Add(new DateTime(2024, 1, 1, 0, 29, 0), "ES IST EINE MINUTE VOR HALB EINS +0");
            Add(new DateTime(2024, 1, 1, 0, 30, 0), "ES IST HALB EINS +0");
            Add(new DateTime(2024, 1, 1, 0, 31, 0), "ES IST EINE MINUTE NACH HALB EINS +0");
            Add(new DateTime(2024, 1, 1, 0, 34, 0), "ES IST VIER MINUTEN NACH HALB EINS +0");
            Add(new DateTime(2024, 1, 1, 0, 35, 0), "ES IST FÜNF MINUTEN NACH HALB EINS +0");
            Add(new DateTime(2024, 1, 1, 0, 44, 0), "ES IST VIERZEHN MINUTEN NACH HALB EINS +0");
            Add(new DateTime(2024, 1, 1, 0, 45, 0), "ES IST VIERTEL VOR EINS +0");
            Add(new DateTime(2024, 1, 1, 0, 46, 0), "ES IST VIERZEHN MINUTEN VOR EINS +0");
            Add(new DateTime(2024, 1, 1, 0, 55, 0), "ES IST FÜNF MINUTEN VOR EINS +0");
            Add(new DateTime(2024, 1, 1, 0, 59, 0), "ES IST EINE MINUTE VOR EINS +0");
            Add(new DateTime(2024, 1, 1, 1, 0, 0), "ES IST EIN UHR +0");
            Add(new DateTime(2024, 1, 1, 2, 0, 0), "ES IST ZWEI UHR +0");
            Add(new DateTime(2024, 1, 1, 3, 0, 0), "ES IST DREI UHR +0");
            Add(new DateTime(2024, 1, 1, 6, 0, 0), "ES IST SECHS UHR +0");
            Add(new DateTime(2024, 1, 1, 7, 0, 0), "ES IST SIEBEN UHR +0");
            Add(new DateTime(2024, 1, 1, 8, 0, 0), "ES IST ACHT UHR +0");
            Add(new DateTime(2024, 1, 1, 9, 0, 0), "ES IST NEUN UHR +0");
            Add(new DateTime(2024, 1, 1, 10, 0, 0), "ES IST ZEHN UHR +0");
            Add(new DateTime(2024, 1, 1, 11, 0, 0), "ES IST ELF UHR +0");
            Add(new DateTime(2024, 1, 1, 11, 59, 0), "ES IST EINE MINUTE VOR ZWÖLF +0");
            Add(new DateTime(2024, 1, 1, 12, 0, 0), "ES IST ZWÖLF UHR +0");
            Add(new DateTime(2024, 1, 1, 12, 30, 0), "ES IST HALB EINS +0");
            Add(new DateTime(2024, 1, 1, 12, 59, 0), "ES IST EINE MINUTE VOR EINS +0");
        }
    }
}
