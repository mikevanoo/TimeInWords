namespace TextToTimeGridLib.Grids;

public class TimeGridDutchPrecise : TimeGrid
{
    public override int GridHeight => 9;
    public override int GridWidth => 18;

    protected override string RawGrid =>
        "HETBISBÉÉNTWEEDRIE"
        + '\n'
        + "VIERVIJFACHTZESBFG"
        + '\n'
        + "ZEVENNEGENELFTIENB"
        + '\n'
        + "TWAALFDERTIENBBFJG"
        + '\n'
        + "VEERTIENBKWARTBFJG"
        + '\n'
        + "HALFOVERVOORBHALFB"
        + '\n'
        + "ÉÉNTWEEDRIEVIERZES"
        + '\n'
        + "VIJFZEVENACHTELFBF"
        + '\n'
        + "NEGENTIENTWAALFUUR";
}
