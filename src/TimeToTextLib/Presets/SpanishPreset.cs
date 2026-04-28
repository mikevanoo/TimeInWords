namespace TimeToTextLib.Presets;

public class SpanishPreset : LanguagePreset
{
    public override TimeToTextFormat Format(DateTime time)
    {
        var hour = HourIn12HourClock(time.Hour);
        var minute = MinuteRoundedDown(time.Minute);
        var additionalMinutes = AdditionalMinutes(time.Minute);
        var hourForPrefix = minute >= 35 ? hour + 1 : hour;

        var phrase = minute switch
        {
            0 => $"{Hour(hour)} EN PUNTO",
            5 => $"{Hour(hour)} Y CINCO",
            10 => $"{Hour(hour)} Y DIEZ",
            15 => $"{Hour(hour)} Y CUARTO",
            20 => $"{Hour(hour)} Y VEINTE",
            25 => $"{Hour(hour)} Y VEINTICINCO",
            30 => $"{Hour(hour)} Y MEDIA",
            35 => $"{Hour(hour + 1)} MENOS VEINTICINCO",
            40 => $"{Hour(hour + 1)} MENOS VEINTE",
            45 => $"{Hour(hour + 1)} MENOS CUARTO",
            50 => $"{Hour(hour + 1)} MENOS DIEZ",
            55 => $"{Hour(hour + 1)} MENOS CINCO",
            _ => throw new ArgumentOutOfRangeException(nameof(time)),
        };

        return new TimeToTextFormat { TimeAsText = $"{PrefixForHour(hourForPrefix)} {phrase}", AdditionalMinutes = additionalMinutes };
    }

    protected override string[] Numbers =>
        ["UNA", "DOS", "TRES", "CUATRO", "CINCO", "SEIS", "SIETE", "OCHO", "NUEVE", "DIEZ", "ONCE", "DOCE"];

    protected override string Prefix => "SON LAS";

    private static string PrefixForHour(int hour12)
    {
        var h = hour12;
        if (h == 0)
        {
            h = 12;
        }

        return h == 1 ? "ES LA" : "SON LAS";
    }
}
