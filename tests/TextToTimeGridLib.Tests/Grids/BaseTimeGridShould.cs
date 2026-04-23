using TextToTimeGridLib.Grids;
using TimeToTextLib;

namespace TextToTimeGridLib.Tests.Grids;

public abstract class BaseTimeGridShould<T>
    where T : TimeGrid, new()
{
    // Representative times that cover every hour 0-12 plus the key minute
    // buckets (O'CLOCK, five, ten, quarter, twenty, twenty-five, half, to vs
    // past on both sides of the hour). This protects the grid's RawGrid
    // asset against typos without re-running the language-agnostic bitmask
    // algorithm (which is tested directly in BitmaskAlgorithmShould).
    private static readonly (int Hour, int Minute)[] RepresentativeTimes =
    [
        (0, 0),
        (1, 5),
        (2, 10),
        (3, 15),
        (4, 20),
        (5, 25),
        (6, 30),
        (7, 35),
        (8, 40),
        (9, 45),
        (10, 50),
        (11, 55),
        (12, 0),
    ];

    protected abstract LanguagePreset.Language Language { get; }

    [Fact]
    public void ProvideRawGridOfCorrectSize()
    {
        var grid = new T();

        var actual = grid.ToString();

        var lines = actual.Split('\n');
        lines.Should().HaveCount(grid.GridHeight);
        lines.Should().AllSatisfy(line => line.Should().HaveLength(grid.GridWidth));
    }

    [Fact]
    public void ResolveRepresentativePhrasesInStrictBitmask()
    {
        var grid = new T();
        var preset = LanguagePreset.Get(Language);

        foreach (var (hour, minute) in RepresentativeTimes)
        {
            var time = new DateTime(2024, 1, 1, hour, minute, 0);
            var format = preset.Format(time);
            var bitmask = grid.GetBitMask(format.TimeAsText, strict: true);
            var rendered = grid.ToString(bitmask);

            var words = format.TimeAsText.Split(' ');
            var renderedNoNewlines = rendered.Replace("\n", "").Replace(".", "");
            foreach (var word in words)
            {
                renderedNoNewlines.Should().Contain(word,
                    because: $"phrase '{format.TimeAsText}' at {hour}:{minute:D2} should resolve word '{word}' in the grid");
            }
        }
    }
}
