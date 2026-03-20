using TextToTimeGridLib.Grids;
using TimeToTextLib;

namespace TextToTimeGridLib.Tests.Grids;

public abstract class BaseTimeGridShould<T>
    where T : TimeGrid, new()
{
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
    public void ResolveAllPhrasesInStrictBitmask()
    {
        var grid = new T();
        var preset = LanguagePreset.Get(Language);

        for (var hour = 0; hour < 13; hour++)
        {
            for (var minute = 0; minute < 60; minute++)
            {
                var time = new DateTime(2024, 1, 1, hour, minute, 0);
                var format = preset.Format(time);
                var bitmask = grid.GetBitMask(format.TimeAsText, strict: true);
                var rendered = grid.ToString(bitmask);

                // Verify all words from the phrase appear lit in the rendered output
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
}
