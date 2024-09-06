using TextToTimeGridLib.Grids;

namespace TextToTimeGridLib.Tests.Grids;

public class TimeGridDutchShould
{
    [Fact]
    public void ProvideRawGridOfCorrectSize()
    {
        var grid = new TimeGridDutch();

        var actual = grid.ToString();

        var lines = actual.Split('\n');
        lines.Should().HaveCount(10);
        lines.Should().AllSatisfy(line => line.Should().HaveLength(11));
    }
}
