namespace TextToTimeGridLib.Tests;

public class BitmaskShould
{
    [Fact]
    public void GenerateCorrectStringFor3X3Grid()
    {
        var boolGrid = new bool[][] { [true, false, true], [false, true, false], [true, false, true] };
        var bitmask = new Bitmask(boolGrid);

        bitmask
            .ToString()
            .Should()
            .Be(
                """
                101
                010
                101

                """
            );
    }

    [Fact]
    public void GenerateCorrectStringFor5X5Grid()
    {
        var boolGrid = new bool[][]
        {
            [false, false, false, false, false],
            [true, true, true, true, true],
            [true, false, true, false, true],
        };
        var bitmask = new Bitmask(boolGrid);

        bitmask
            .ToString()
            .Should()
            .Be(
                """
                00000
                11111
                10101

                """
            );
    }
}
