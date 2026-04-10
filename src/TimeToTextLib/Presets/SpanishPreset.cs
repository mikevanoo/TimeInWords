using System;
using System.Globalization;
using System.Text;

namespace TimeToTextLib.Presets;

public class SpanishPreset : LanguagePreset
{
    public override TimeToTextFormat Format(DateTime time)
    {
        var hour = HourIn12HourClock(time.Hour);
        var minute = MinuteRoundedDown(time.Minute);
        var additionalMinutes = AdditionalMinutes(time.Minute);
        var hourForPrefix = minute >= 35 ? hour + 1 : hour;

        var s = new StringBuilder($"{PrefixForHour(hourForPrefix)} ");

        switch (minute)
        {
            case 0:
                s.Append(CultureInfo.InvariantCulture, $"{Hour(hour)} EN PUNTO");
                break;
            case 5:
                s.Append(CultureInfo.InvariantCulture, $"{Hour(hour)} Y CINCO");
                break;
            case 10:
                s.Append(CultureInfo.InvariantCulture, $"{Hour(hour)} Y DIEZ");
                break;
            case 15:
                s.Append(CultureInfo.InvariantCulture, $"{Hour(hour)} Y CUARTO");
                break;
            case 20:
                s.Append(CultureInfo.InvariantCulture, $"{Hour(hour)} Y VEINTE");
                break;
            case 25:
                s.Append(CultureInfo.InvariantCulture, $"{Hour(hour)} Y VEINTICINCO");
                break;
            case 30:
                s.Append(CultureInfo.InvariantCulture, $"{Hour(hour)} Y MEDIA");
                break;
            case 35:
                s.Append(CultureInfo.InvariantCulture, $"{Hour(hour + 1)} MENOS VEINTICINCO");
                break;
            case 40:
                s.Append(CultureInfo.InvariantCulture, $"{Hour(hour + 1)} MENOS VEINTE");
                break;
            case 45:
                s.Append(CultureInfo.InvariantCulture, $"{Hour(hour + 1)} MENOS CUARTO");
                break;
            case 50:
                s.Append(CultureInfo.InvariantCulture, $"{Hour(hour + 1)} MENOS DIEZ");
                break;
            case 55:
                s.Append(CultureInfo.InvariantCulture, $"{Hour(hour + 1)} MENOS CINCO");
                break;
        }

        return new TimeToTextFormat { TimeAsText = s.ToString(), AdditionalMinutes = additionalMinutes };
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
