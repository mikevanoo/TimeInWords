namespace TextToTimeGridLib.Grids;

public class TimeGridGermanPrecise : TimeGrid
{
    public override int GridHeight => 14;
    public override int GridWidth => 18;

    protected override string RawGrid =>
        "ESBISTBEINEBZWEIKG"
        + '\n'
        + "DREIGVIERGFÜNFBJKL"
        + '\n'
        + "SECHSBSIEBENBACHTK"
        + '\n'
        + "NEUNBZEHNBELFJGKLB"
        + '\n'
        + "ZWÖLFBDREIZEHNBKJG"
        + '\n'
        + "GVIERZEHNKUNDBKJGK"
        + '\n'
        + "KJZWANZIGBLPBKJGLP"
        + '\n'
        + "MINUTENBVIERTELBKJ"
        + '\n'
        + "NACHBVORBHALBGKJLP"
        + '\n'
        + "EINSBZWEIBDREIBKJG"
        + '\n'
        + "VIERBFÜNFBSECHSBKJ"
        + '\n'
        + "SIEBENBACHTBNEUNKJ"
        + '\n'
        + "ZEHNBELFBZWÖLFBKJG"
        + '\n'
        + "BUHRBJGKLPBKJGLPBK";
}
