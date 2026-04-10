namespace TextToTimeGridLib.Grids;

public class TimeGridSpanish : TimeGrid
{
    public override int GridHeight => 11;
    public override int GridWidth => 12;

    protected override string RawGrid =>
        "ESONBLASDOCE"
        + '\n'
        + "CUATROBCINCO"
        + '\n'
        + "SIETEBOCHOBR"
        + '\n'
        + "NUEVEBSEISBF"
        + '\n'
        + "TRESBDOSBUNA"
        + '\n'
        + "ONCEBDIEZRFH"
        + '\n'
        + "YBENHPUNTORF"
        + '\n'
        + "MENOSBCUARTO"
        + '\n'
        + "VEINTEBCINCO"
        + '\n'
        + "BVEINTICINCO"
        + '\n'
        + "MEDIABDIEZRF";
}
