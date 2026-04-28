namespace TimeToTextLib.Presets;

public class GermanPrecisePreset : LanguagePreset
{
    public override TimeToTextFormat Format(DateTime time)
    {
        var hour = HourIn12HourClock(time.Hour);
        var minute = time.Minute;

        var phrase = minute switch
        {
            0 => $"{HourForUhr(hour)} UHR",
            15 => $"VIERTEL NACH {Hour(hour)}",
            30 => $"HALB {Hour(hour + 1)}",
            45 => $"VIERTEL VOR {Hour(hour + 1)}",
            <= 14 => $"{MinutesPhrase(minute)} NACH {Hour(hour)}",
            <= 29 => $"{MinutesPhrase(30 - minute)} VOR HALB {Hour(hour + 1)}",
            <= 44 => $"{MinutesPhrase(minute - 30)} NACH HALB {Hour(hour + 1)}",
            _ => $"{MinutesPhrase(60 - minute)} VOR {Hour(hour + 1)}",
        };

        return new TimeToTextFormat { TimeAsText = $"{Prefix} {phrase}", AdditionalMinutes = 0 };
    }

    private string HourForUhr(int hour) => hour == 1 ? "EIN" : Hour(hour);

    private string MinutesPhrase(int minutes) => $"{MinuteNumber(minutes)} {MinuteWord(minutes)}";

    private static string MinuteWord(int minutes) => minutes == 1 ? "MINUTE" : "MINUTEN";

    private string MinuteNumber(int minutes) => minutes == 1 ? "EINE" : GetMinuteNumberText(minutes);

    private string GetMinuteNumberText(int minutes) =>
        minutes >= 21 ? $"{GetNumberText(minutes - 20)} UND ZWANZIG" : GetNumberText(minutes);

    protected override string[] Numbers =>
        [
            "EINS",
            "ZWEI",
            "DREI",
            "VIER",
            "FÜNF",
            "SECHS",
            "SIEBEN",
            "ACHT",
            "NEUN",
            "ZEHN",
            "ELF",
            "ZWÖLF",
            "DREIZEHN",
            "VIERZEHN",
        ];

    protected override string Prefix => "ES IST";
}
