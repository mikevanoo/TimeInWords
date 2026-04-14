using System;
using System.Globalization;
using System.Text;

namespace TimeToTextLib.Presets;

public class GermanPrecisePreset : LanguagePreset
{
    public override TimeToTextFormat Format(DateTime time)
    {
        var s = new StringBuilder($"{Prefix} ");
        var hour = HourIn12HourClock(time.Hour);
        var minute = time.Minute;

        if (minute == 0)
        {
            s.Append(CultureInfo.InvariantCulture, $"{HourForUhr(hour)} UHR");
        }
        else if (minute <= 14)
        {
            s.Append(CultureInfo.InvariantCulture, $"{MinuteNumber(minute)} {MinuteWord(minute)} NACH {Hour(hour)}");
        }
        else if (minute == 15)
        {
            s.Append(CultureInfo.InvariantCulture, $"VIERTEL NACH {Hour(hour)}");
        }
        else if (minute <= 29)
        {
            var diff = 30 - minute;
            s.Append(CultureInfo.InvariantCulture,
                $"{MinuteNumber(diff)} {MinuteWord(diff)} VOR HALB {Hour(hour + 1)}");
        }
        else if (minute == 30)
        {
            s.Append(CultureInfo.InvariantCulture, $"HALB {Hour(hour + 1)}");
        }
        else if (minute <= 44)
        {
            var diff = minute - 30;
            s.Append(CultureInfo.InvariantCulture,
                $"{MinuteNumber(diff)} {MinuteWord(diff)} NACH HALB {Hour(hour + 1)}");
        }
        else if (minute == 45)
        {
            s.Append(CultureInfo.InvariantCulture, $"VIERTEL VOR {Hour(hour + 1)}");
        }
        else
        {
            var diff = 60 - minute;
            s.Append(CultureInfo.InvariantCulture,
                $"{MinuteNumber(diff)} {MinuteWord(diff)} VOR {Hour(hour + 1)}");
        }

        return new TimeToTextFormat { TimeAsText = s.ToString(), AdditionalMinutes = 0 };
    }

    private string HourForUhr(int hour) =>
        hour == 1 ? "EIN" : Hour(hour);

    private static string MinuteWord(int minutes) =>
        minutes == 1 ? "MINUTE" : "MINUTEN";

    private string MinuteNumber(int minutes) =>
        minutes == 1 ? "EINE" : GetMinuteNumberText(minutes);

    private string GetMinuteNumberText(int minutes) =>
        minutes >= 21 ? $"{GetNumberText(minutes - 20)} UND ZWANZIG" : GetNumberText(minutes);

    protected override string[] Numbers =>
        [
            "EINS", "ZWEI", "DREI", "VIER", "FÜNF", "SECHS", "SIEBEN", "ACHT", "NEUN", "ZEHN",
            "ELF", "ZWÖLF", "DREIZEHN", "VIERZEHN",
        ];

    protected override string Prefix => "ES IST";
}
