using TextToTimeGridLib.Grids;

namespace TextToTimeGridLib.Tests.Grids;

public abstract class BaseTimeGridShould<T>
    where T : TimeGrid, new()
{
    [Fact]
    public void ProvideRawGridOfCorrectSize()
    {
        var grid = new T();

        var actual = grid.ToString();

        var lines = actual.Split('\n');
        lines.Should().HaveCount(grid.GridHeight);
        lines.Should().AllSatisfy(line => line.Should().HaveLength(grid.GridWidth));
    }
}
