using System.Globalization;
using System.Text;
using TextToTimeGridLib.Grids;
using TimeToTextLib;
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
        var result = new StringBuilder();
        string[] inputs =
        [
            "HELLO",
            "GOODBYE",
            "THANK YOU",
            "PLEASE",
            "YES",
            "NO",
            "MAYBE",
            "WELCOME",
            "SEE YOU",
            "TAKE CARE",
            "TIME",
            "SEVEN",
            "QUARTER",
            "OCLOCK",
            "TEN",
            "PAST",
            "TO",
        ];

        foreach (var input in inputs)
        {
            var bitMask = timeGrid.GetBitMask(input, true);
            result.AppendLine(CultureInfo.InvariantCulture, $"Add(\"{input}\", true, @\"{bitMask.ToString()}\");");

            bitMask = timeGrid.GetBitMask(input, false);
            result.AppendLine(CultureInfo.InvariantCulture, $"Add(\"{input}\", false, @\"{bitMask.ToString()}\");");
        }

        testOutputHelper.WriteLine(result.ToString());
    }

    private class GetBitMaskTheoryData : TheoryData<string, bool, string>
    {
        public GetBitMaskTheoryData()
        {
            #region
            Add(
                "HELLO",
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
                "HELLO",
                false,
                @"00000000000
00000000000
00000000000
10000010000
00000000000
00000000000
00000000000
00000010000
00000000100
00000100000
"
            );
            Add(
                "GOODBYE",
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
                "GOODBYE",
                false,
                @"00000000000
00000000000
00000000000
00000000000
00000000000
00000000000
00000000000
00100000000
00000000000
00000100100
"
            );
            Add(
                "THANK YOU",
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
                "THANK YOU",
                false,
                @"01000000000
00000000000
00000000000
11000001000
00000000000
00000000000
00000000000
00000000000
00000000000
00000000001
"
            );
            Add(
                "PLEASE",
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
                "PLEASE",
                false,
                @"00000000000
00000000000
00000000000
00000000000
10000000000
00000000000
00000000000
00000011000
00000000000
00000000000
"
            );
            Add(
                "YES",
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
                "YES",
                false,
                @"00000000000
00000000000
00000100010
00000000000
00100000000
00000000000
00000000000
00000000000
00000000000
00000000000
"
            );
            Add(
                "NO",
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
                "NO",
                false,
                @"00000000000
00000000000
00010000000
00000000001
00000000000
00000000000
00000000000
00000000000
00000000000
00000000000
"
            );
            Add(
                "MAYBE",
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
                "MAYBE",
                false,
                @"00000000010
10000000000
00000100000
00001010000
00000000000
00000000000
00000000000
00000000000
00000000000
00000000000
"
            );
            Add(
                "WELCOME",
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
                "WELCOME",
                false,
                @"00000000000
00000000000
01100000000
00100000000
00000000000
00000000000
00000000000
00000000000
00000000000
00000010100
"
            );
            Add(
                "SEE YOU",
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
                "SEE YOU",
                false,
                @"00001000001
00000001000
00000100000
00000000001
00000010000
00000000000
00000000000
00000000000
00000000000
00000000000
"
            );
            Add(
                "TAKE CARE",
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
                "TAKE CARE",
                false,
                @"01000000000
10000000000
00000000000
00000000000
00000000000
00000000000
00000000000
00000000000
00000000000
00000000001
"
            );
            Add(
                "TIME",
                true,
                @"00000001111
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
                "TIME",
                false,
                @"01010000011
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
                "SEVEN",
                true,
                @"00000000000
00000000000
00000000000
00000000000
00000000000
00000000000
00000000000
00000000000
11111000000
00000000000
"
            );
            Add(
                "SEVEN",
                false,
                @"00001000001
00000000000
00000000110
00000001000
00000000000
00000000000
00000000000
00000000000
00000000000
00000000000
"
            );
            Add(
                "QUARTER",
                true,
                @"00000000000
00111111100
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
                "QUARTER",
                false,
                @"00000000000
00111111100
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
                "OCLOCK",
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
00000111111
"
            );
            Add(
                "OCLOCK",
                false,
                @"00000000000
00000000000
00000000000
00000000001
00000000000
00000000000
00000000000
00000000000
00000000000
00000011111
"
            );
            Add(
                "TEN",
                true,
                @"00000000000
00000000000
00000000000
00000111000
00000000000
00000000000
00000000000
00000000000
00000000000
00000000000
"
            );
            Add(
                "TEN",
                false,
                @"01000000001
00000000000
00010000000
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
                "PAST",
                true,
                @"00000000000
00000000000
00000000000
00000000000
11110000000
00000000000
00000000000
00000000000
00000000000
00000000000
"
            );
            Add(
                "PAST",
                false,
                @"00000000000
00000000000
00000000000
00000000000
11110000000
00000000000
00000000000
00000000000
00000000000
00000000000
"
            );
            Add(
                "TO",
                true,
                @"00000000000
00000000000
00000000000
00000000011
00000000000
00000000000
00000000000
00000000000
00000000000
00000000000
"
            );
            Add(
                "TO",
                false,
                @"01000000000
00000000000
00000000000
00000000001
00000000000
00000000000
00000000000
00000000000
00000000000
00000000000
"
            );
            #endregion
        }
    }

    [Theory]
    [ClassData(typeof(ToStringTheoryData))]
    public void BuildCorrectToStringValueGivenString(string input, bool strict, string expected)
    {
        var timeGrid = new TestTimeGrid();

        var bitMask = timeGrid.GetBitMask(input, strict);
        var actual = timeGrid.ToString(bitMask);

        testOutputHelper.WriteLine(actual);
        actual.Should().BeEquivalentTo(expected);
    }

#pragma warning disable xUnit1004
    [Fact(Skip = "test code generator")]
#pragma warning restore xUnit1004
    public void GenerateToStringTheoryData()
    {
        var timeGrid = new TestTimeGrid();
        var result = new StringBuilder();
        string[] inputs =
        [
            "HELLO",
            "GOODBYE",
            "THANK YOU",
            "PLEASE",
            "YES",
            "NO",
            "MAYBE",
            "WELCOME",
            "SEE YOU",
            "TAKE CARE",
            "TIME",
            "SEVEN",
            "QUARTER",
            "OCLOCK",
            "TEN",
            "PAST",
            "TO",
        ];

        foreach (var input in inputs)
        {
            var bitMask = timeGrid.GetBitMask(input, true);
            result.AppendLine(
                CultureInfo.InvariantCulture,
                $"Add(\"{input}\", true, @\"{timeGrid.ToString(bitMask)}\");"
            );

            bitMask = timeGrid.GetBitMask(input, false);
            result.AppendLine(
                CultureInfo.InvariantCulture,
                $"Add(\"{input}\", false, @\"{timeGrid.ToString(bitMask)}\");"
            );
        }

        testOutputHelper.WriteLine(result.ToString());
    }

    private class ToStringTheoryData : TheoryData<string, bool, string>
    {
        public ToStringTheoryData()
        {
            #region
            Add(
                "HELLO",
                true,
                @"...........
...........
...........
...........
...........
...........
...........
...........
...........
...........
"
            );
            Add(
                "HELLO",
                false,
                @"...........
...........
...........
H.....E....
...........
...........
...........
......L....
........L..
.....O.....
"
            );
            Add(
                "GOODBYE",
                true,
                @"...........
...........
...........
...........
...........
...........
...........
...........
...........
...........
"
            );
            Add(
                "GOODBYE",
                false,
                @"...........
...........
...........
...........
...........
...........
...........
..G........
...........
.....O..O..
"
            );
            Add(
                "THANK YOU",
                true,
                @"...........
...........
...........
...........
...........
...........
...........
...........
...........
...........
"
            );
            Add(
                "THANK YOU",
                false,
                @".T.........
...........
...........
HA.....N...
...........
...........
...........
...........
...........
..........K
"
            );
            Add(
                "PLEASE",
                true,
                @"...........
...........
...........
...........
...........
...........
...........
...........
...........
...........
"
            );
            Add(
                "PLEASE",
                false,
                @"...........
...........
...........
...........
P..........
...........
...........
......LE...
...........
...........
"
            );
            Add(
                "YES",
                true,
                @"...........
...........
...........
...........
...........
...........
...........
...........
...........
...........
"
            );
            Add(
                "YES",
                false,
                @"...........
...........
.....Y...E.
...........
..S........
...........
...........
...........
...........
...........
"
            );
            Add(
                "NO",
                true,
                @"...........
...........
...........
...........
...........
...........
...........
...........
...........
...........
"
            );
            Add(
                "NO",
                false,
                @"...........
...........
...N.......
..........O
...........
...........
...........
...........
...........
...........
"
            );
            Add(
                "MAYBE",
                true,
                @"...........
...........
...........
...........
...........
...........
...........
...........
...........
...........
"
            );
            Add(
                "MAYBE",
                false,
                @".........M.
A..........
.....Y.....
....B.E....
...........
...........
...........
...........
...........
...........
"
            );
            Add(
                "WELCOME",
                true,
                @"...........
...........
...........
...........
...........
...........
...........
...........
...........
...........
"
            );
            Add(
                "WELCOME",
                false,
                @"...........
...........
.WE........
..L........
...........
...........
...........
...........
...........
......C.O..
"
            );
            Add(
                "SEE YOU",
                true,
                @"...........
...........
...........
...........
...........
...........
...........
...........
...........
...........
"
            );
            Add(
                "SEE YOU",
                false,
                @"....S.....E
.......E...
.....Y.....
..........O
......U....
...........
...........
...........
...........
...........
"
            );
            Add(
                "TAKE CARE",
                true,
                @"...........
...........
...........
...........
...........
...........
...........
...........
...........
...........
"
            );
            Add(
                "TAKE CARE",
                false,
                @".T.........
A..........
...........
...........
...........
...........
...........
...........
...........
..........K
"
            );
            Add(
                "TIME",
                true,
                @".......TIME
...........
...........
...........
...........
...........
...........
...........
...........
...........
"
            );
            Add(
                "TIME",
                false,
                @".T.I.....ME
...........
...........
...........
...........
...........
...........
...........
...........
...........
"
            );
            Add(
                "SEVEN",
                true,
                @"...........
...........
...........
...........
...........
...........
...........
...........
SEVEN......
...........
"
            );
            Add(
                "SEVEN",
                false,
                @"....S.....E
...........
........VE.
.......N...
...........
...........
...........
...........
...........
...........
"
            );
            Add(
                "QUARTER",
                true,
                @"...........
..QUARTER..
...........
...........
...........
...........
...........
...........
...........
...........
"
            );
            Add(
                "QUARTER",
                false,
                @"...........
..QUARTER..
...........
...........
...........
...........
...........
...........
...........
...........
"
            );
            Add(
                "OCLOCK",
                true,
                @"...........
...........
...........
...........
...........
...........
...........
...........
...........
.....OCLOCK
"
            );
            Add(
                "OCLOCK",
                false,
                @"...........
...........
...........
..........O
...........
...........
...........
...........
...........
......CLOCK
"
            );
            Add(
                "TEN",
                true,
                @"...........
...........
...........
.....TEN...
...........
...........
...........
...........
...........
...........
"
            );
            Add(
                "TEN",
                false,
                @".T........E
...........
...N.......
...........
...........
...........
...........
...........
...........
...........
"
            );
            Add(
                "PAST",
                true,
                @"...........
...........
...........
...........
PAST.......
...........
...........
...........
...........
...........
"
            );
            Add(
                "PAST",
                false,
                @"...........
...........
...........
...........
PAST.......
...........
...........
...........
...........
...........
"
            );
            Add(
                "TO",
                true,
                @"...........
...........
...........
.........TO
...........
...........
...........
...........
...........
...........
"
            );
            Add(
                "TO",
                false,
                @".T.........
...........
...........
..........O
...........
...........
...........
...........
...........
...........
"
            );
            #endregion
        }
    }

    private class TestTimeGrid : TimeGrid
    {
        protected override string RawGrid =>
            "ITLISLSTIME"
            + '\n'
            + "ACQUARTERDC"
            + '\n'
            + "TWENTYFIVEX"
            + '\n'
            + "HALFBTENFTO"
            + '\n'
            + "PASTERUNINE"
            + '\n'
            + "ONESIXTHREE"
            + '\n'
            + "FOURFIVETWO"
            + '\n'
            + "EIGHTELEVEN"
            + '\n'
            + "SEVENTWELVE"
            + '\n'
            + "TENSEOCLOCK";
    }
}
