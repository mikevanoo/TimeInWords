using System.Globalization;
using System.Text;
using TextToTimeGridLib.Grids;
using TimeToTextLib;
using TimeToTextLib.Presets;
using Xunit.Abstractions;

namespace TextToTimeGridLib.Tests;

public class TimeGridShould(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void ThrowWhenGettingGridForUnknownLanguage()
    {
        var act = () => TimeGrid.Get((LanguagePreset.Language)999);

        act.Should()
            .ThrowExactly<ArgumentOutOfRangeException>()
            .WithMessage("Language not implemented*")
            .And.ParamName.Should()
            .Be("lang");
    }

    [Theory]
    [InlineData(LanguagePreset.Language.English, typeof(TimeGridEnglish))]
    [InlineData(LanguagePreset.Language.Dutch, typeof(TimeGridDutch))]
    [InlineData(LanguagePreset.Language.EnglishPrecise, typeof(TimeGridEnglishPrecise))]
    public void GetCorrectTimeGridForGivenLanguage(LanguagePreset.Language language, Type expectedPreset)
    {
        var actual = TimeGrid.Get(language);

        actual.Should().BeOfType(expectedPreset);
    }

    [Fact]
    public void BuildCorrectCharGridFromGivenRawGridString()
    {
        var timeGrid = new TestTimeGrid();

        var expected = new char[][]
        {
            ['I', 'T', 'L', 'I', 'S', 'L', 'S', 'T', 'I', 'M', 'E'],
            ['A', 'C', 'Q', 'U', 'A', 'R', 'T', 'E', 'R', 'D', 'C'],
            ['T', 'W', 'E', 'N', 'T', 'Y', 'F', 'I', 'V', 'E', 'X'],
            ['H', 'A', 'L', 'F', 'B', 'T', 'E', 'N', 'F', 'T', 'O'],
            ['P', 'A', 'S', 'T', 'E', 'R', 'U', 'N', 'I', 'N', 'E'],
            ['O', 'N', 'E', 'S', 'I', 'X', 'T', 'H', 'R', 'E', 'E'],
            ['F', 'O', 'U', 'R', 'F', 'I', 'V', 'E', 'T', 'W', 'O'],
            ['E', 'I', 'G', 'H', 'T', 'E', 'L', 'E', 'V', 'E', 'N'],
            ['S', 'E', 'V', 'E', 'N', 'T', 'W', 'E', 'L', 'V', 'E'],
            ['T', 'E', 'N', 'S', 'E', 'O', 'C', 'L', 'O', 'C', 'K'],
        };

        timeGrid.CharGrid.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [ClassData(typeof(GetBitMaskTheoryData))]
    public void BuildCorrectBitmaskFromGivenString(string input, bool strict, string expected)
    {
        var timeGrid = new TestTimeGrid();

        var actual = timeGrid.GetBitMask(input, strict);

        testOutputHelper.WriteLine(actual.ToString());
        actual.ToString().Should().BeEquivalentTo(expected);
    }

#pragma warning disable xUnit1004
    [Fact(Skip = "test code generator")]
#pragma warning restore xUnit1004
    public void GenerateGetBitMaskTheoryData()
    {
        var timeGrid = new TestTimeGrid();
        var preset = new EnglishPreset();
        var result = new StringBuilder();

        var time = new DateTime(2024, 1, 1, 0, 0, 0);
        while (time.Hour < 12)
        {
            var timeAsText = preset.Format(time).TimeAsText;

            var bitMask = timeGrid.GetBitMask(timeAsText, true);
            result.AppendLine(CultureInfo.InvariantCulture, $"Add(\"{timeAsText}\", true, @\"{bitMask.ToString()}\");");

            bitMask = timeGrid.GetBitMask(timeAsText, false);
            result.AppendLine(
                CultureInfo.InvariantCulture,
                $"Add(\"{timeAsText}\", false, @\"{bitMask.ToString()}\");"
            );

            time = time.AddMinutes(5);
        }

        testOutputHelper.WriteLine(result.ToString());
    }

    private class GetBitMaskTheoryData : TheoryData<string, bool, string>
    {
        public GetBitMaskTheoryData()
        {
            // Empty input — neither mode lights any cell.
            Add(
                "",
                true,
                @"00000000000
00000000000
00000000000
00000000000
00000000000
00000000000
00000000000
00000000000
00000000000
00000000000
"
            );
            Add(
                "",
                false,
                @"00000000000
00000000000
00000000000
00000000000
00000000000
00000000000
00000000000
00000000000
00000000000
00000000000
"
            );
            // Midnight / OCLOCK path.
            Add(
                "IT IS TWELVE OCLOCK",
                true,
                @"11011000000
00000000000
00000000000
00000000000
00000000000
00000000000
00000000000
00000000000
00000111111
00000111111
"
            );
            Add(
                "IT IS TWELVE OCLOCK",
                false,
                @"11011001000
00000000000
01100000000
00100000000
00000000000
00000000000
00000011001
00000000000
00000000000
00000011111
"
            );
            // Typical multi-row PAST phrase.
            Add(
                "IT IS FIVE PAST TWELVE",
                true,
                @"11011000000
00000000000
00000011110
00000000000
11110000000
00000000000
00000000000
00000000000
00000111111
00000000000
"
            );
            Add(
                "IT IS FIVE PAST TWELVE",
                false,
                @"11011000000
00000000000
00000011110
00000000000
11110000000
00000010000
00000000010
10000010110
00000000000
00000000000
"
            );
            // Hour boundary at :30 using the HALF path.
            Add(
                "IT IS HALF PAST NINE",
                true,
                @"11011000000
00000000000
00000000000
11110000000
11110001111
00000000000
00000000000
00000000000
00000000000
00000000000
"
            );
            Add(
                "IT IS HALF PAST NINE",
                false,
                @"11011000000
00000000000
00000000000
11110000000
11110001111
00000000000
00000000000
00000000000
00000000000
00000000000
"
            );
            // Duplicate-letter reset: row 5 "ONESIXTHREE" ends in "EE", and
            // the strict scan must step past that duplicate before finding
            // ELEVEN on row 7.
            Add(
                "IT IS HALF PAST ELEVEN",
                true,
                @"11011000000
00000000000
00000000000
11110000000
11110000000
00000000000
00000000000
00000111111
00000000000
00000000000
"
            );
            Add(
                "IT IS HALF PAST ELEVEN",
                false,
                @"11011000000
00000000000
00000000000
11110000000
11111000000
00000000000
00000000000
00000011111
00000000000
00000000000
"
            );
            // Dense phrase spanning rows 0, 2, 3, and 8.
            Add(
                "IT IS TWENTYFIVE TO TWELVE",
                true,
                @"11011000000
00000000000
11111111110
00000000011
00000000000
00000000000
00000000000
00000000000
00000111111
00000000000
"
            );
            Add(
                "IT IS TWENTYFIVE TO TWELVE",
                false,
                @"11011001000
00000000000
01111111110
00000100001
00010000000
00000000000
00000000010
10000010110
00000000000
00000000000
"
            );
        }
    }
}
