using System.Globalization;
using System.Text;
using TimeToTextLib.Presets;
using Xunit.Abstractions;

namespace TimeToTextLib.Tests.Presets;

public class EnglishPrecisePresetShould(ITestOutputHelper testOutputHelper)
{
    private readonly EnglishPrecisePreset _preset = new();

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
            Add(new DateTime(2024, 1, 1, 0, 1, 0), "IT IS ONE MINUTE PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 4, 0), "IT IS FOUR MINUTES PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 5, 0), "IT IS FIVE MINUTES PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 14, 0), "IT IS FOURTEEN MINUTES PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 15, 0), "IT IS A QUARTER PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 16, 0), "IT IS SIXTEEN MINUTES PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 29, 0), "IT IS TWENTY NINE MINUTES PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 30, 0), "IT IS HALF PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 31, 0), "IT IS TWENTY NINE MINUTES TO ONE +0");
            Add(new DateTime(2024, 1, 1, 0, 34, 0), "IT IS TWENTY SIX MINUTES TO ONE +0");
            Add(new DateTime(2024, 1, 1, 0, 35, 0), "IT IS TWENTY FIVE MINUTES TO ONE +0");
            Add(new DateTime(2024, 1, 1, 0, 44, 0), "IT IS SIXTEEN MINUTES TO ONE +0");
            Add(new DateTime(2024, 1, 1, 0, 45, 0), "IT IS A QUARTER TO ONE +0");
            Add(new DateTime(2024, 1, 1, 0, 46, 0), "IT IS FOURTEEN MINUTES TO ONE +0");
            Add(new DateTime(2024, 1, 1, 0, 55, 0), "IT IS FIVE MINUTES TO ONE +0");
            Add(new DateTime(2024, 1, 1, 0, 59, 0), "IT IS ONE MINUTE TO ONE +0");
            Add(new DateTime(2024, 1, 1, 2, 15, 0), "IT IS A QUARTER PAST TWO +0");
            Add(new DateTime(2024, 1, 1, 3, 30, 0), "IT IS HALF PAST THREE +0");
            Add(new DateTime(2024, 1, 1, 4, 15, 0), "IT IS A QUARTER PAST FOUR +0");
            Add(new DateTime(2024, 1, 1, 5, 40, 0), "IT IS TWENTY MINUTES TO SIX +0");
            Add(new DateTime(2024, 1, 1, 7, 25, 0), "IT IS TWENTY FIVE MINUTES PAST SEVEN +0");
            Add(new DateTime(2024, 1, 1, 8, 45, 0), "IT IS A QUARTER TO NINE +0");
            Add(new DateTime(2024, 1, 1, 10, 50, 0), "IT IS TEN MINUTES TO ELEVEN +0");
            Add(new DateTime(2024, 1, 1, 11, 59, 0), "IT IS ONE MINUTE TO TWELVE +0");
            Add(new DateTime(2024, 1, 1, 12, 0, 0), "IT IS TWELVE OCLOCK +0");
            Add(new DateTime(2024, 1, 1, 12, 30, 0), "IT IS HALF PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 12, 59, 0), "IT IS ONE MINUTE TO ONE +0");
        }
    }
}
