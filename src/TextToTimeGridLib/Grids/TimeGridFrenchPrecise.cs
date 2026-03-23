namespace TextToTimeGridLib.Grids;

public class TimeGridFrenchPrecise : TimeGrid
{
    public override int GridHeight => 11;
    public override int GridWidth => 18;

    protected override string RawGrid =>
        "ILBESTFMIDIAMINUIT"
        + '\n'
        + "UNESIXTROISQUATREB"
        + '\n'
        + "CINQDEUXSEPTHUITBF"
        + '\n'
        + "NEUFDIXONZEDOUZEFG"
        + '\n'
        + "HEURESJKMOINSLEFGB"
        + '\n'
        + "TREIZEFQUATORZEBGJ"
        + '\n'
        + "SEIZEJBONZEGDOUZEF"
        + '\n'
        + "VINGTDIXFNEUFHUITB"
        + '\n'
        + "ETBQUARTFDEMIEGJKB"
        + '\n'
        + "SEPTSIXCINQBQUATRE"
        + '\n'
        + "JBTROISKDEUXGUNEBF";
}
