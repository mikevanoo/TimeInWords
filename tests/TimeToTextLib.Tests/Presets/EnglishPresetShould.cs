using System.Globalization;
using System.Text;
using TimeToTextLib.Presets;

namespace TimeToTextLib.Tests.Presets;

public class EnglishPresetShould(ITestOutputHelper testOutputHelper)
{
    private readonly EnglishPreset _preset = new();

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
            Add(new DateTime(2024, 1, 1, 0, 0, 0), "IT IS TWELVE OCLOCK +0");
            Add(new DateTime(2024, 1, 1, 0, 1, 0), "IT IS TWELVE OCLOCK +1");
            Add(new DateTime(2024, 1, 1, 0, 4, 0), "IT IS TWELVE OCLOCK +4");
            Add(new DateTime(2024, 1, 1, 0, 5, 0), "IT IS FIVE PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 29, 0), "IT IS TWENTYFIVE PAST TWELVE +4");
            Add(new DateTime(2024, 1, 1, 0, 30, 0), "IT IS HALF PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 31, 0), "IT IS HALF PAST TWELVE +1");
            Add(new DateTime(2024, 1, 1, 0, 34, 0), "IT IS HALF PAST TWELVE +4");
            Add(new DateTime(2024, 1, 1, 0, 35, 0), "IT IS TWENTYFIVE TO ONE +0");
            Add(new DateTime(2024, 1, 1, 1, 10, 0), "IT IS TEN PAST ONE +0");
            Add(new DateTime(2024, 1, 1, 2, 20, 0), "IT IS TWENTY PAST TWO +0");
            Add(new DateTime(2024, 1, 1, 3, 15, 0), "IT IS A QUARTER PAST THREE +0");
            Add(new DateTime(2024, 1, 1, 4, 25, 0), "IT IS TWENTYFIVE PAST FOUR +0");
            Add(new DateTime(2024, 1, 1, 5, 2, 0), "IT IS FIVE OCLOCK +2");
            Add(new DateTime(2024, 1, 1, 6, 3, 0), "IT IS SIX OCLOCK +3");
            Add(new DateTime(2024, 1, 1, 7, 40, 0), "IT IS TWENTY TO EIGHT +0");
            Add(new DateTime(2024, 1, 1, 8, 55, 0), "IT IS FIVE TO NINE +0");
            Add(new DateTime(2024, 1, 1, 9, 45, 0), "IT IS A QUARTER TO TEN +0");
            Add(new DateTime(2024, 1, 1, 10, 50, 0), "IT IS TEN TO ELEVEN +0");
            Add(new DateTime(2024, 1, 1, 11, 0, 0), "IT IS ELEVEN OCLOCK +0");
            Add(new DateTime(2024, 1, 1, 11, 59, 0), "IT IS FIVE TO TWELVE +4");
            Add(new DateTime(2024, 1, 1, 12, 0, 0), "IT IS TWELVE OCLOCK +0");
            Add(new DateTime(2024, 1, 1, 12, 30, 0), "IT IS HALF PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 12, 59, 0), "IT IS FIVE TO ONE +4");
        }
    }
}
