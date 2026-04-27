using System.Globalization;
using System.Text;
using TimeToTextLib.Presets;

namespace TimeToTextLib.Tests.Presets;

public class DutchPrecisePresetShould(ITestOutputHelper testOutputHelper)
{
    private readonly DutchPrecisePreset _preset = new();

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
            Add(new DateTime(2024, 1, 1, 0, 0, 0), "HET IS TWAALF UUR +0");
            Add(new DateTime(2024, 1, 1, 0, 1, 0), "HET IS ÉÉN OVER TWAALF +0");
            Add(new DateTime(2024, 1, 1, 0, 4, 0), "HET IS VIER OVER TWAALF +0");
            Add(new DateTime(2024, 1, 1, 0, 5, 0), "HET IS VIJF OVER TWAALF +0");
            Add(new DateTime(2024, 1, 1, 0, 14, 0), "HET IS VEERTIEN OVER TWAALF +0");
            Add(new DateTime(2024, 1, 1, 0, 15, 0), "HET IS KWART OVER TWAALF +0");
            Add(new DateTime(2024, 1, 1, 0, 16, 0), "HET IS VEERTIEN VOOR HALF ÉÉN +0");
            Add(new DateTime(2024, 1, 1, 0, 29, 0), "HET IS ÉÉN VOOR HALF ÉÉN +0");
            Add(new DateTime(2024, 1, 1, 0, 30, 0), "HET IS HALF ÉÉN +0");
            Add(new DateTime(2024, 1, 1, 0, 31, 0), "HET IS ÉÉN OVER HALF ÉÉN +0");
            Add(new DateTime(2024, 1, 1, 0, 34, 0), "HET IS VIER OVER HALF ÉÉN +0");
            Add(new DateTime(2024, 1, 1, 0, 35, 0), "HET IS VIJF OVER HALF ÉÉN +0");
            Add(new DateTime(2024, 1, 1, 0, 44, 0), "HET IS VEERTIEN OVER HALF ÉÉN +0");
            Add(new DateTime(2024, 1, 1, 0, 45, 0), "HET IS KWART VOOR ÉÉN +0");
            Add(new DateTime(2024, 1, 1, 0, 46, 0), "HET IS VEERTIEN VOOR ÉÉN +0");
            Add(new DateTime(2024, 1, 1, 0, 55, 0), "HET IS VIJF VOOR ÉÉN +0");
            Add(new DateTime(2024, 1, 1, 0, 59, 0), "HET IS ÉÉN VOOR ÉÉN +0");
            Add(new DateTime(2024, 1, 1, 2, 15, 0), "HET IS KWART OVER TWEE +0");
            Add(new DateTime(2024, 1, 1, 3, 0, 0), "HET IS DRIE UUR +0");
            Add(new DateTime(2024, 1, 1, 5, 13, 0), "HET IS DERTIEN OVER VIJF +0");
            Add(new DateTime(2024, 1, 1, 6, 0, 0), "HET IS ZES UUR +0");
            Add(new DateTime(2024, 1, 1, 7, 0, 0), "HET IS ZEVEN UUR +0");
            Add(new DateTime(2024, 1, 1, 8, 0, 0), "HET IS ACHT UUR +0");
            Add(new DateTime(2024, 1, 1, 9, 0, 0), "HET IS NEGEN UUR +0");
            Add(new DateTime(2024, 1, 1, 10, 0, 0), "HET IS TIEN UUR +0");
            Add(new DateTime(2024, 1, 1, 11, 0, 0), "HET IS ELF UUR +0");
            Add(new DateTime(2024, 1, 1, 11, 59, 0), "HET IS ÉÉN VOOR TWAALF +0");
            Add(new DateTime(2024, 1, 1, 12, 0, 0), "HET IS TWAALF UUR +0");
            Add(new DateTime(2024, 1, 1, 12, 30, 0), "HET IS HALF ÉÉN +0");
            Add(new DateTime(2024, 1, 1, 12, 59, 0), "HET IS ÉÉN VOOR ÉÉN +0");
        }
    }
}
