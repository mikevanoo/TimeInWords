namespace TextToTimeGridLib.Tests;

internal sealed class TestTimeGrid : TimeGrid
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
