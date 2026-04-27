using System.Globalization;
using System.Text;
using TimeToTextLib.Presets;

namespace TimeToTextLib.Tests.Presets;

public class SpanishPresetShould(ITestOutputHelper testOutputHelper)
{
    private readonly SpanishPreset _preset = new();

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
            Add(new DateTime(2024, 1, 1, 0, 1, 0), "SON LAS DOCE EN PUNTO +1");
            Add(new DateTime(2024, 1, 1, 0, 4, 0), "SON LAS DOCE EN PUNTO +4");
            Add(new DateTime(2024, 1, 1, 0, 5, 0), "SON LAS DOCE Y CINCO +0");
            Add(new DateTime(2024, 1, 1, 0, 29, 0), "SON LAS DOCE Y VEINTICINCO +4");
            Add(new DateTime(2024, 1, 1, 0, 30, 0), "SON LAS DOCE Y MEDIA +0");
            Add(new DateTime(2024, 1, 1, 0, 31, 0), "SON LAS DOCE Y MEDIA +1");
            Add(new DateTime(2024, 1, 1, 0, 34, 0), "SON LAS DOCE Y MEDIA +4");
            Add(new DateTime(2024, 1, 1, 0, 35, 0), "ES LA UNA MENOS VEINTICINCO +0");
            Add(new DateTime(2024, 1, 1, 1, 10, 0), "ES LA UNA Y DIEZ +0");
            Add(new DateTime(2024, 1, 1, 2, 15, 0), "SON LAS DOS Y CUARTO +0");
            Add(new DateTime(2024, 1, 1, 3, 20, 0), "SON LAS TRES Y VEINTE +0");
            Add(new DateTime(2024, 1, 1, 4, 2, 0), "SON LAS CUATRO EN PUNTO +2");
            Add(new DateTime(2024, 1, 1, 4, 40, 0), "SON LAS CINCO MENOS VEINTE +0");
            Add(new DateTime(2024, 1, 1, 5, 3, 0), "SON LAS CINCO EN PUNTO +3");
            Add(new DateTime(2024, 1, 1, 5, 45, 0), "SON LAS SEIS MENOS CUARTO +0");
            Add(new DateTime(2024, 1, 1, 6, 50, 0), "SON LAS SIETE MENOS DIEZ +0");
            Add(new DateTime(2024, 1, 1, 7, 55, 0), "SON LAS OCHO MENOS CINCO +0");
            Add(new DateTime(2024, 1, 1, 9, 0, 0), "SON LAS NUEVE EN PUNTO +0");
            Add(new DateTime(2024, 1, 1, 10, 0, 0), "SON LAS DIEZ EN PUNTO +0");
            Add(new DateTime(2024, 1, 1, 11, 0, 0), "SON LAS ONCE EN PUNTO +0");
            Add(new DateTime(2024, 1, 1, 11, 59, 0), "SON LAS DOCE MENOS CINCO +4");
            Add(new DateTime(2024, 1, 1, 12, 0, 0), "SON LAS DOCE EN PUNTO +0");
            Add(new DateTime(2024, 1, 1, 12, 30, 0), "SON LAS DOCE Y MEDIA +0");
            Add(new DateTime(2024, 1, 1, 12, 59, 0), "ES LA UNA MENOS CINCO +4");
        }
    }
}
