namespace TimeToTextLib.Presets;

public class DutchPreset : LanguagePreset
{
    public override TimeToTextFormat Format(DateTime time)
    {
        var hour = HourIn12HourClock(time.Hour);
        var minute = MinuteRoundedDown(time.Minute);
        var additionalMinutes = AdditionalMinutes(time.Minute);

        var phrase = minute switch
        {
            0 => $"{Hour(hour)} UUR",
            5 or 10 => $"{GetNumberText(minute)} OVER {Hour(hour)}",
            15 => $"KWART OVER {Hour(hour)}",
            20 => $"{GetNumberText(10)} VOOR HALF {Hour(hour + 1)}",
            25 => $"{GetNumberText(5)} VOOR HALF {Hour(hour + 1)}",
            30 => $"HALF {Hour(hour)}",
            35 => $"{GetNumberText(5)} OVER HALF {Hour(hour + 1)}",
            40 => $"{GetNumberText(10)} OVER HALF {Hour(hour + 1)}",
            45 => $"KWART VOOR {Hour(hour + 1)}",
            50 => $"{GetNumberText(10)} VOOR {Hour(hour + 1)}",
            55 => $"{GetNumberText(5)} VOOR {Hour(hour + 1)}",
            _ => throw new ArgumentOutOfRangeException(nameof(time)),
        };

        return new TimeToTextFormat { TimeAsText = $"{Prefix} {phrase}", AdditionalMinutes = additionalMinutes };
    }

    protected override string[] Numbers =>
        ["ÉÉN", "TWEE", "DRIE", "VIER", "VIJF", "ZES", "ZEVEN", "ACHT", "NEGEN", "TIEN", "ELF", "TWAALF"];

    protected override string Prefix => "HET IS";
}
