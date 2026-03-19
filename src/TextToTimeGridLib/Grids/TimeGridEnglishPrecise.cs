namespace TextToTimeGridLib.Grids;

public class TimeGridEnglishPrecise : TimeGrid
{
    public override int GridHeight => 22;
    public override int GridWidth => 18;

    protected override string RawGrid =>
        "ITLISLAMSTIMEBCDEF"
        + '\n'
        + "QUARTERDHALFGHJKLM"
        + '\n'
        + "ONETWOTHREEFOURBCD"
        + '\n'
        + "FIVESIXSEVENEIGHTB"
        + '\n'
        + "NINEBTENBCDEFGHJKL"
        + '\n'
        + "ELEVENBTWELVEBCDEF"
        + '\n'
        + "THIRTEENBFOURTEENB"
        + '\n'
        + "SIXTEENBSEVENTEENB"
        + '\n'
        + "EIGHTEENB" + "NINETEENB"
        + '\n'
        + "TWENTYONETWENTYTWO"
        + '\n'
        + "TWENTYTHREEBCDEFGH"
        + '\n'
        + "TWENTYFOURBCDEFGHJ"
        + '\n'
        + "TWENTYFIVEBCDEFGHJ"
        + '\n'
        + "TWENTYSIXBCDEFGHJK"
        + '\n'
        + "TWENTYSEVENBCDEFGH"
        + '\n'
        + "TWENTYEIGHTBCDEFGH"
        + '\n'
        + "TWENTYNINEBCDEFGHJ"
        + '\n'
        + "PASTBTOBCDEFGHJKLM"
        + '\n'
        + "ONESIXTHREEFOURBCD"
        + '\n'
        + "FIVETWOSEVENEIGHTB"
        + '\n'
        + "NINETENBCDEFGHJKLM"
        + '\n'
        + "ELEVENTWELVEOCLOCK";
}
