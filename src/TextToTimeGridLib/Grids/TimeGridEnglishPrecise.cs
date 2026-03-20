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
        + "DBONETWOTHREEFOURC"
        + '\n'
        + "FIVESIXSEVENEIGHTB"
        + '\n'
        + "FBNINETENELEVENJDE"
        + '\n'
        + "ETWELVECTHIRTEENBD"
        + '\n'
        + "LFOURTEENCSIXTEENB"
        + '\n'
        + "SEVENTEENEIGHTEENB"
        + '\n'
        + "DBLNINETEENCFEKGJC"
        + '\n'
        + "KBMINUTESJPASTBTOM"
        + '\n'
        + "ONESIXTHREEFOURBCD"
        + '\n'
        + "FIVETWOSEVENEIGHTB"
        + '\n'
        + "BNINEFTENDELEVENCE"
        + '\n'
        + "TWELVEGBOCLOCKFDEC";
}
