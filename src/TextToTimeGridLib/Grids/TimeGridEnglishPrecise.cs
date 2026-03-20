namespace TextToTimeGridLib.Grids;

public class TimeGridEnglishPrecise : TimeGrid
{
    public override int GridHeight => 14;
    public override int GridWidth => 18;

    protected override string RawGrid =>
        "ITLISLAMSTIMEBCDEF"
        + '\n'
        + "QUARTERDHALFTWENTY"
        + '\n'
        + "ONETWOTHREEFOURBCD"
        + '\n'
        + "FIVESIXSEVENEIGHTB"
        + '\n'
        + "NINETENELEVENBCDEF"
        + '\n'
        + "TWELVETHIRTEENBCDE"
        + '\n'
        + "FOURTEENSIXTEENBCD"
        + '\n'
        + "SEVENTEENEIGHTEENB"
        + '\n'
        + "NINETEENBCDEFGHJKL"
        + '\n'
        + "PASTBTOBCDEFGHJKLM"
        + '\n'
        + "ONESIXTHREEFOURBCD"
        + '\n'
        + "FIVETWOSEVENEIGHTB"
        + '\n'
        + "NINETENELEVENBCDEF"
        + '\n'
        + "TWELVEOCLOCKBCDEFG";
}
