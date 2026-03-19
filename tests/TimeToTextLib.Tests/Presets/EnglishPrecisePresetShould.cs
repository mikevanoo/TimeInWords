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

    [Theory]
    [ClassData(typeof(FormatTimeToTextCorrectlyTheoryData))]
    public void AlwaysHaveZeroAdditionalMinutes(DateTime time, string _) =>
        _preset.Format(time).AdditionalMinutes.Should().Be(0);

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
            // Hour 0 (12 o'clock) - all 60 minutes to test every pattern
            Add(new DateTime(2024, 1, 1, 0, 0, 0), "IT IS TWELVE OCLOCK +0");
            Add(new DateTime(2024, 1, 1, 0, 1, 0), "IT IS ONE PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 2, 0), "IT IS TWO PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 3, 0), "IT IS THREE PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 4, 0), "IT IS FOUR PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 5, 0), "IT IS FIVE PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 6, 0), "IT IS SIX PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 7, 0), "IT IS SEVEN PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 8, 0), "IT IS EIGHT PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 9, 0), "IT IS NINE PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 10, 0), "IT IS TEN PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 11, 0), "IT IS ELEVEN PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 12, 0), "IT IS TWELVE PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 13, 0), "IT IS THIRTEEN PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 14, 0), "IT IS FOURTEEN PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 15, 0), "IT IS A QUARTER PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 16, 0), "IT IS SIXTEEN PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 17, 0), "IT IS SEVENTEEN PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 18, 0), "IT IS EIGHTEEN PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 19, 0), "IT IS NINETEEN PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 20, 0), "IT IS TWENTY PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 21, 0), "IT IS TWENTYONE PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 22, 0), "IT IS TWENTYTWO PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 23, 0), "IT IS TWENTYTHREE PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 24, 0), "IT IS TWENTYFOUR PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 25, 0), "IT IS TWENTYFIVE PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 26, 0), "IT IS TWENTYSIX PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 27, 0), "IT IS TWENTYSEVEN PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 28, 0), "IT IS TWENTYEIGHT PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 29, 0), "IT IS TWENTYNINE PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 30, 0), "IT IS HALF PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 0, 31, 0), "IT IS TWENTYNINE TO ONE +0");
            Add(new DateTime(2024, 1, 1, 0, 32, 0), "IT IS TWENTYEIGHT TO ONE +0");
            Add(new DateTime(2024, 1, 1, 0, 33, 0), "IT IS TWENTYSEVEN TO ONE +0");
            Add(new DateTime(2024, 1, 1, 0, 34, 0), "IT IS TWENTYSIX TO ONE +0");
            Add(new DateTime(2024, 1, 1, 0, 35, 0), "IT IS TWENTYFIVE TO ONE +0");
            Add(new DateTime(2024, 1, 1, 0, 36, 0), "IT IS TWENTYFOUR TO ONE +0");
            Add(new DateTime(2024, 1, 1, 0, 37, 0), "IT IS TWENTYTHREE TO ONE +0");
            Add(new DateTime(2024, 1, 1, 0, 38, 0), "IT IS TWENTYTWO TO ONE +0");
            Add(new DateTime(2024, 1, 1, 0, 39, 0), "IT IS TWENTYONE TO ONE +0");
            Add(new DateTime(2024, 1, 1, 0, 40, 0), "IT IS TWENTY TO ONE +0");
            Add(new DateTime(2024, 1, 1, 0, 41, 0), "IT IS NINETEEN TO ONE +0");
            Add(new DateTime(2024, 1, 1, 0, 42, 0), "IT IS EIGHTEEN TO ONE +0");
            Add(new DateTime(2024, 1, 1, 0, 43, 0), "IT IS SEVENTEEN TO ONE +0");
            Add(new DateTime(2024, 1, 1, 0, 44, 0), "IT IS SIXTEEN TO ONE +0");
            Add(new DateTime(2024, 1, 1, 0, 45, 0), "IT IS A QUARTER TO ONE +0");
            Add(new DateTime(2024, 1, 1, 0, 46, 0), "IT IS FOURTEEN TO ONE +0");
            Add(new DateTime(2024, 1, 1, 0, 47, 0), "IT IS THIRTEEN TO ONE +0");
            Add(new DateTime(2024, 1, 1, 0, 48, 0), "IT IS TWELVE TO ONE +0");
            Add(new DateTime(2024, 1, 1, 0, 49, 0), "IT IS ELEVEN TO ONE +0");
            Add(new DateTime(2024, 1, 1, 0, 50, 0), "IT IS TEN TO ONE +0");
            Add(new DateTime(2024, 1, 1, 0, 51, 0), "IT IS NINE TO ONE +0");
            Add(new DateTime(2024, 1, 1, 0, 52, 0), "IT IS EIGHT TO ONE +0");
            Add(new DateTime(2024, 1, 1, 0, 53, 0), "IT IS SEVEN TO ONE +0");
            Add(new DateTime(2024, 1, 1, 0, 54, 0), "IT IS SIX TO ONE +0");
            Add(new DateTime(2024, 1, 1, 0, 55, 0), "IT IS FIVE TO ONE +0");
            Add(new DateTime(2024, 1, 1, 0, 56, 0), "IT IS FOUR TO ONE +0");
            Add(new DateTime(2024, 1, 1, 0, 57, 0), "IT IS THREE TO ONE +0");
            Add(new DateTime(2024, 1, 1, 0, 58, 0), "IT IS TWO TO ONE +0");
            Add(new DateTime(2024, 1, 1, 0, 59, 0), "IT IS ONE TO ONE +0");

            // Hour 1 (ONE) - next hour TWO
            Add(new DateTime(2024, 1, 1, 1, 0, 0), "IT IS ONE OCLOCK +0");
            Add(new DateTime(2024, 1, 1, 1, 1, 0), "IT IS ONE PAST ONE +0");
            Add(new DateTime(2024, 1, 1, 1, 15, 0), "IT IS A QUARTER PAST ONE +0");
            Add(new DateTime(2024, 1, 1, 1, 29, 0), "IT IS TWENTYNINE PAST ONE +0");
            Add(new DateTime(2024, 1, 1, 1, 30, 0), "IT IS HALF PAST ONE +0");
            Add(new DateTime(2024, 1, 1, 1, 31, 0), "IT IS TWENTYNINE TO TWO +0");
            Add(new DateTime(2024, 1, 1, 1, 45, 0), "IT IS A QUARTER TO TWO +0");
            Add(new DateTime(2024, 1, 1, 1, 59, 0), "IT IS ONE TO TWO +0");

            // Hour 2 (TWO) - next hour THREE
            Add(new DateTime(2024, 1, 1, 2, 0, 0), "IT IS TWO OCLOCK +0");
            Add(new DateTime(2024, 1, 1, 2, 1, 0), "IT IS ONE PAST TWO +0");
            Add(new DateTime(2024, 1, 1, 2, 30, 0), "IT IS HALF PAST TWO +0");
            Add(new DateTime(2024, 1, 1, 2, 59, 0), "IT IS ONE TO THREE +0");

            // Hour 3 (THREE) - next hour FOUR
            Add(new DateTime(2024, 1, 1, 3, 0, 0), "IT IS THREE OCLOCK +0");
            Add(new DateTime(2024, 1, 1, 3, 1, 0), "IT IS ONE PAST THREE +0");
            Add(new DateTime(2024, 1, 1, 3, 30, 0), "IT IS HALF PAST THREE +0");
            Add(new DateTime(2024, 1, 1, 3, 59, 0), "IT IS ONE TO FOUR +0");

            // Hour 4 (FOUR) - next hour FIVE
            Add(new DateTime(2024, 1, 1, 4, 0, 0), "IT IS FOUR OCLOCK +0");
            Add(new DateTime(2024, 1, 1, 4, 1, 0), "IT IS ONE PAST FOUR +0");
            Add(new DateTime(2024, 1, 1, 4, 30, 0), "IT IS HALF PAST FOUR +0");
            Add(new DateTime(2024, 1, 1, 4, 59, 0), "IT IS ONE TO FIVE +0");

            // Hour 5 (FIVE) - next hour SIX
            Add(new DateTime(2024, 1, 1, 5, 0, 0), "IT IS FIVE OCLOCK +0");
            Add(new DateTime(2024, 1, 1, 5, 1, 0), "IT IS ONE PAST FIVE +0");
            Add(new DateTime(2024, 1, 1, 5, 30, 0), "IT IS HALF PAST FIVE +0");
            Add(new DateTime(2024, 1, 1, 5, 59, 0), "IT IS ONE TO SIX +0");

            // Hour 6 (SIX) - next hour SEVEN
            Add(new DateTime(2024, 1, 1, 6, 0, 0), "IT IS SIX OCLOCK +0");
            Add(new DateTime(2024, 1, 1, 6, 1, 0), "IT IS ONE PAST SIX +0");
            Add(new DateTime(2024, 1, 1, 6, 30, 0), "IT IS HALF PAST SIX +0");
            Add(new DateTime(2024, 1, 1, 6, 59, 0), "IT IS ONE TO SEVEN +0");

            // Hour 7 (SEVEN) - next hour EIGHT
            Add(new DateTime(2024, 1, 1, 7, 0, 0), "IT IS SEVEN OCLOCK +0");
            Add(new DateTime(2024, 1, 1, 7, 1, 0), "IT IS ONE PAST SEVEN +0");
            Add(new DateTime(2024, 1, 1, 7, 30, 0), "IT IS HALF PAST SEVEN +0");
            Add(new DateTime(2024, 1, 1, 7, 59, 0), "IT IS ONE TO EIGHT +0");

            // Hour 8 (EIGHT) - next hour NINE
            Add(new DateTime(2024, 1, 1, 8, 0, 0), "IT IS EIGHT OCLOCK +0");
            Add(new DateTime(2024, 1, 1, 8, 1, 0), "IT IS ONE PAST EIGHT +0");
            Add(new DateTime(2024, 1, 1, 8, 30, 0), "IT IS HALF PAST EIGHT +0");
            Add(new DateTime(2024, 1, 1, 8, 59, 0), "IT IS ONE TO NINE +0");

            // Hour 9 (NINE) - next hour TEN
            Add(new DateTime(2024, 1, 1, 9, 0, 0), "IT IS NINE OCLOCK +0");
            Add(new DateTime(2024, 1, 1, 9, 1, 0), "IT IS ONE PAST NINE +0");
            Add(new DateTime(2024, 1, 1, 9, 30, 0), "IT IS HALF PAST NINE +0");
            Add(new DateTime(2024, 1, 1, 9, 59, 0), "IT IS ONE TO TEN +0");

            // Hour 10 (TEN) - next hour ELEVEN
            Add(new DateTime(2024, 1, 1, 10, 0, 0), "IT IS TEN OCLOCK +0");
            Add(new DateTime(2024, 1, 1, 10, 1, 0), "IT IS ONE PAST TEN +0");
            Add(new DateTime(2024, 1, 1, 10, 30, 0), "IT IS HALF PAST TEN +0");
            Add(new DateTime(2024, 1, 1, 10, 59, 0), "IT IS ONE TO ELEVEN +0");

            // Hour 11 (ELEVEN) - next hour TWELVE
            Add(new DateTime(2024, 1, 1, 11, 0, 0), "IT IS ELEVEN OCLOCK +0");
            Add(new DateTime(2024, 1, 1, 11, 1, 0), "IT IS ONE PAST ELEVEN +0");
            Add(new DateTime(2024, 1, 1, 11, 30, 0), "IT IS HALF PAST ELEVEN +0");
            Add(new DateTime(2024, 1, 1, 11, 31, 0), "IT IS TWENTYNINE TO TWELVE +0");
            Add(new DateTime(2024, 1, 1, 11, 45, 0), "IT IS A QUARTER TO TWELVE +0");
            Add(new DateTime(2024, 1, 1, 11, 59, 0), "IT IS ONE TO TWELVE +0");

            // Hour 12 (TWELVE again) - next hour ONE
            Add(new DateTime(2024, 1, 1, 12, 0, 0), "IT IS TWELVE OCLOCK +0");
            Add(new DateTime(2024, 1, 1, 12, 1, 0), "IT IS ONE PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 12, 30, 0), "IT IS HALF PAST TWELVE +0");
            Add(new DateTime(2024, 1, 1, 12, 31, 0), "IT IS TWENTYNINE TO ONE +0");
            Add(new DateTime(2024, 1, 1, 12, 59, 0), "IT IS ONE TO ONE +0");
        }
    }
}
