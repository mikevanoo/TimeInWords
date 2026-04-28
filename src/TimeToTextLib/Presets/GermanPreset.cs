namespace TimeToTextLib.Presets;

public class GermanPreset : LanguagePreset
{
    public override TimeToTextFormat Format(DateTime time)
    {
        var hour = HourIn12HourClock(time.Hour);
        var minute = MinuteRoundedDown(time.Minute);
        var additionalMinutes = AdditionalMinutes(time.Minute);

        var phrase = minute switch
        {
            0 => $"{HourForUhr(hour)} UHR",
            5 => $"FÜNF NACH {Hour(hour)}",
            10 => $"ZEHN NACH {Hour(hour)}",
            15 => $"VIERTEL NACH {Hour(hour)}",
            20 => $"ZWANZIG NACH {Hour(hour)}",
            25 => $"FÜNF VOR HALB {Hour(hour + 1)}",
            30 => $"HALB {Hour(hour + 1)}",
            35 => $"FÜNF NACH HALB {Hour(hour + 1)}",
            40 => $"ZWANZIG VOR {Hour(hour + 1)}",
            45 => $"VIERTEL VOR {Hour(hour + 1)}",
            50 => $"ZEHN VOR {Hour(hour + 1)}",
            55 => $"FÜNF VOR {Hour(hour + 1)}",
            _ => throw new ArgumentOutOfRangeException(nameof(time)),
        };

        return new TimeToTextFormat { TimeAsText = $"{Prefix} {phrase}", AdditionalMinutes = additionalMinutes };
    }

    private string HourForUhr(int hour) => hour == 1 ? "EIN" : Hour(hour);

    protected override string[] Numbers =>
        ["EINS", "ZWEI", "DREI", "VIER", "FÜNF", "SECHS", "SIEBEN", "ACHT", "NEUN", "ZEHN", "ELF", "ZWÖLF"];

    protected override string Prefix => "ES IST";
}
