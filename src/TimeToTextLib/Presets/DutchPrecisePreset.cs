namespace TimeToTextLib.Presets;

public class DutchPrecisePreset : LanguagePreset
{
    public override TimeToTextFormat Format(DateTime time)
    {
        var hour = HourIn12HourClock(time.Hour);
        var minute = time.Minute;

        var phrase = minute switch
        {
            0 => $"{Hour(hour)} UUR",
            15 => $"KWART OVER {Hour(hour)}",
            30 => $"HALF {Hour(hour + 1)}",
            45 => $"KWART VOOR {Hour(hour + 1)}",
            <= 14 => $"{GetNumberText(minute)} OVER {Hour(hour)}",
            <= 29 => $"{GetNumberText(30 - minute)} VOOR HALF {Hour(hour + 1)}",
            <= 44 => $"{GetNumberText(minute - 30)} OVER HALF {Hour(hour + 1)}",
            _ => $"{GetNumberText(60 - minute)} VOOR {Hour(hour + 1)}",
        };

        return new TimeToTextFormat { TimeAsText = $"{Prefix} {phrase}", AdditionalMinutes = 0 };
    }

    protected override string[] Numbers =>
        [
            "ÉÉN",
            "TWEE",
            "DRIE",
            "VIER",
            "VIJF",
            "ZES",
            "ZEVEN",
            "ACHT",
            "NEGEN",
            "TIEN",
            "ELF",
            "TWAALF",
            "DERTIEN",
            "VEERTIEN",
        ];

    protected override string Prefix => "HET IS";
}
