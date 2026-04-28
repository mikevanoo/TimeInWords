namespace TimeToTextLib.Presets;

public class FrenchPreset : LanguagePreset
{
    public override TimeToTextFormat Format(DateTime time)
    {
        var minute = MinuteRoundedDown(time.Minute);
        var additionalMinutes = AdditionalMinutes(time.Minute);

        var phrase = minute switch
        {
            0 => HourWithHeures(time.Hour),
            5 => $"{HourWithHeures(time.Hour)} {Numbers[4]}",
            10 => $"{HourWithHeures(time.Hour)} {Numbers[9]}",
            15 => $"{HourWithHeures(time.Hour)} ET QUART",
            20 => $"{HourWithHeures(time.Hour)} VINGT",
            25 => $"{HourWithHeures(time.Hour)} VINGT-CINQ",
            30 => $"{HourWithHeures(time.Hour)} ET DEMIE",
            35 => $"{HourWithHeures(time.Hour + 1)} MOINS VINGT-CINQ",
            40 => $"{HourWithHeures(time.Hour + 1)} MOINS VINGT",
            45 => $"{HourWithHeures(time.Hour + 1)} MOINS LE QUART",
            50 => $"{HourWithHeures(time.Hour + 1)} MOINS DIX",
            55 => $"{HourWithHeures(time.Hour + 1)} MOINS CINQ",
            _ => throw new ArgumentOutOfRangeException(nameof(time)),
        };

        return new TimeToTextFormat { TimeAsText = $"{Prefix} {phrase}", AdditionalMinutes = additionalMinutes };
    }

    protected override string[] Numbers =>
        ["UNE", "DEUX", "TROIS", "QUATRE", "CINQ", "SIX", "SEPT", "HUIT", "NEUF", "DIX", "ONZE", "DOUZE"];

    protected override string Prefix => "IL EST";

    private string HourWithHeures(int hour24)
    {
        switch (hour24 % 24)
        {
            case 0:
                return "MINUIT";
            case 12:
                return "MIDI";
            default:
            {
                var h = HourIn12HourClock(hour24);
                return $"{Hour(h)} {Heure(h)}";
            }
        }
    }

    private static string Heure(int hour) => hour == 1 ? "HEURE" : "HEURES";
}
