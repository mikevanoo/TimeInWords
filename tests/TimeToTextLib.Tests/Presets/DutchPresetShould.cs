using System.Globalization;
using System.Text;
using TimeToTextLib.Presets;

namespace TimeToTextLib.Tests.Presets;

public class DutchPresetShould(ITestOutputHelper testOutputHelper)
{
    private readonly DutchPreset _preset = new();

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
            Add(new DateTime(2024, 1, 1, 0, 1, 0), "HET IS TWAALF UUR +1");
            Add(new DateTime(2024, 1, 1, 0, 4, 0), "HET IS TWAALF UUR +4");
            Add(new DateTime(2024, 1, 1, 0, 5, 0), "HET IS VIJF OVER TWAALF +0");
            Add(new DateTime(2024, 1, 1, 0, 29, 0), "HET IS VIJF VOOR HALF ÉÉN +4");
            Add(new DateTime(2024, 1, 1, 0, 30, 0), "HET IS HALF TWAALF +0");
            Add(new DateTime(2024, 1, 1, 0, 31, 0), "HET IS HALF TWAALF +1");
            Add(new DateTime(2024, 1, 1, 0, 34, 0), "HET IS HALF TWAALF +4");
            Add(new DateTime(2024, 1, 1, 0, 35, 0), "HET IS VIJF OVER HALF ÉÉN +0");
            Add(new DateTime(2024, 1, 1, 2, 10, 0), "HET IS TIEN OVER TWEE +0");
            Add(new DateTime(2024, 1, 1, 3, 15, 0), "HET IS KWART OVER DRIE +0");
            Add(new DateTime(2024, 1, 1, 3, 20, 0), "HET IS TIEN VOOR HALF VIER +0");
            Add(new DateTime(2024, 1, 1, 5, 2, 0), "HET IS VIJF UUR +2");
            Add(new DateTime(2024, 1, 1, 6, 3, 0), "HET IS ZES UUR +3");
            Add(new DateTime(2024, 1, 1, 6, 40, 0), "HET IS TIEN OVER HALF ZEVEN +0");
            Add(new DateTime(2024, 1, 1, 7, 45, 0), "HET IS KWART VOOR ACHT +0");
            Add(new DateTime(2024, 1, 1, 8, 50, 0), "HET IS TIEN VOOR NEGEN +0");
            Add(new DateTime(2024, 1, 1, 9, 55, 0), "HET IS VIJF VOOR TIEN +0");
            Add(new DateTime(2024, 1, 1, 11, 0, 0), "HET IS ELF UUR +0");
            Add(new DateTime(2024, 1, 1, 11, 59, 0), "HET IS VIJF VOOR TWAALF +4");
            Add(new DateTime(2024, 1, 1, 12, 0, 0), "HET IS TWAALF UUR +0");
            Add(new DateTime(2024, 1, 1, 12, 30, 0), "HET IS HALF TWAALF +0");
            Add(new DateTime(2024, 1, 1, 12, 59, 0), "HET IS VIJF VOOR ÉÉN +4");
        }
    }
}
