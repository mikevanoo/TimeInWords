namespace TimeToTextLib.Presets;

public class SpanishPrecisePreset : LanguagePreset
{
    public override TimeToTextFormat Format(DateTime time)
    {
        var hour = HourIn12HourClock(time.Hour);
        var minute = time.Minute;
        var hourForPrefix = minute >= 31 ? hour + 1 : hour;

        var phrase = minute switch
        {
            0 => $"{Hour(hour)} EN PUNTO",
            <= 30 => $"{Hour(hour)} Y {GetMinuteText(minute)}",
            _ => $"{Hour(hour + 1)} MENOS {GetMinuteText(60 - minute)}",
        };

        return new TimeToTextFormat { TimeAsText = $"{PrefixForHour(hourForPrefix)} {phrase}", AdditionalMinutes = 0 };
    }

    private string GetMinuteText(int minutes)
    {
        if (minutes == 15)
        {
            return "CUARTO";
        }

        if (minutes == 30)
        {
            return "MEDIA";
        }

        if (minutes >= 21 && minutes <= 29)
        {
            return $"VEINTE Y {GetNumberText(minutes - 20)}";
        }

        return GetNumberText(minutes);
    }

    protected override string[] Numbers =>
        [
            "UNA",
            "DOS",
            "TRES",
            "CUATRO",
            "CINCO",
            "SEIS",
            "SIETE",
            "OCHO",
            "NUEVE",
            "DIEZ",
            "ONCE",
            "DOCE",
            "TRECE",
            "CATORCE",
            "",
            "DIECISEIS",
            "DIECISIETE",
            "DIECIOCHO",
            "DIECINUEVE",
            "VEINTE",
        ];

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
