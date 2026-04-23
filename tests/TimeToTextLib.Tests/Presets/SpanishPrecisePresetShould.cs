using System.Globalization;
using System.Text;
using TimeToTextLib.Presets;
using Xunit.Abstractions;

namespace TimeToTextLib.Tests.Presets;

public class SpanishPrecisePresetShould(ITestOutputHelper testOutputHelper)
{
    private readonly SpanishPrecisePreset _preset = new();

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
            Add(new DateTime(2024, 1, 1, 0, 0, 0), "SON LAS DOCE EN PUNTO +0");
            Add(new DateTime(2024, 1, 1, 0, 1, 0), "SON LAS DOCE Y UNA +0");
            Add(new DateTime(2024, 1, 1, 0, 4, 0), "SON LAS DOCE Y CUATRO +0");
            Add(new DateTime(2024, 1, 1, 0, 5, 0), "SON LAS DOCE Y CINCO +0");
            Add(new DateTime(2024, 1, 1, 0, 14, 0), "SON LAS DOCE Y CATORCE +0");
            Add(new DateTime(2024, 1, 1, 0, 15, 0), "SON LAS DOCE Y CUARTO +0");
            Add(new DateTime(2024, 1, 1, 0, 16, 0), "SON LAS DOCE Y DIECISEIS +0");
            Add(new DateTime(2024, 1, 1, 0, 29, 0), "SON LAS DOCE Y VEINTE Y NUEVE +0");
            Add(new DateTime(2024, 1, 1, 0, 30, 0), "SON LAS DOCE Y MEDIA +0");
            Add(new DateTime(2024, 1, 1, 0, 31, 0), "ES LA UNA MENOS VEINTE Y NUEVE +0");
            Add(new DateTime(2024, 1, 1, 0, 34, 0), "ES LA UNA MENOS VEINTE Y SEIS +0");
            Add(new DateTime(2024, 1, 1, 0, 35, 0), "ES LA UNA MENOS VEINTE Y CINCO +0");
            Add(new DateTime(2024, 1, 1, 0, 44, 0), "ES LA UNA MENOS DIECISEIS +0");
            Add(new DateTime(2024, 1, 1, 0, 45, 0), "ES LA UNA MENOS CUARTO +0");
            Add(new DateTime(2024, 1, 1, 0, 46, 0), "ES LA UNA MENOS CATORCE +0");
            Add(new DateTime(2024, 1, 1, 0, 55, 0), "ES LA UNA MENOS CINCO +0");
            Add(new DateTime(2024, 1, 1, 0, 59, 0), "ES LA UNA MENOS UNA +0");
            Add(new DateTime(2024, 1, 1, 2, 0, 0), "SON LAS DOS EN PUNTO +0");
            Add(new DateTime(2024, 1, 1, 3, 0, 0), "SON LAS TRES EN PUNTO +0");
            Add(new DateTime(2024, 1, 1, 7, 0, 0), "SON LAS SIETE EN PUNTO +0");
            Add(new DateTime(2024, 1, 1, 8, 0, 0), "SON LAS OCHO EN PUNTO +0");
            Add(new DateTime(2024, 1, 1, 10, 0, 0), "SON LAS DIEZ EN PUNTO +0");
            Add(new DateTime(2024, 1, 1, 11, 0, 0), "SON LAS ONCE EN PUNTO +0");
            Add(new DateTime(2024, 1, 1, 11, 59, 0), "SON LAS DOCE MENOS UNA +0");
            Add(new DateTime(2024, 1, 1, 12, 0, 0), "SON LAS DOCE EN PUNTO +0");
            Add(new DateTime(2024, 1, 1, 12, 30, 0), "SON LAS DOCE Y MEDIA +0");
            Add(new DateTime(2024, 1, 1, 12, 59, 0), "ES LA UNA MENOS UNA +0");
        }
    }
}
