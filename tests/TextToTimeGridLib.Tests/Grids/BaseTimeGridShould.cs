using TextToTimeGridLib.Grids;

namespace TextToTimeGridLib.Tests.Grids;

public abstract class BaseTimeGridShould<T>
    where T : TimeGrid, new()
{
    [Fact]
    public void ProvideRawGridOfCorrectSize()
    {
        const int ExpectedNumberOfLines = 10;
        const int ExpectedLineLength = 11;

        var grid = new T();

        var actual = grid.ToString();

        var lines = actual.Split('\n');
        lines.Should().HaveCount(ExpectedNumberOfLines);
        lines.Should().AllSatisfy(line => line.Should().HaveLength(ExpectedLineLength));
    }
}
