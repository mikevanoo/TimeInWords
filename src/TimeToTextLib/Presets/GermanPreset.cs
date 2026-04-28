using System.Globalization;
using System.Text;

namespace TimeToTextLib.Presets;

public class GermanPreset : LanguagePreset
{
    public override TimeToTextFormat Format(DateTime time)
    {
        var s = new StringBuilder($"{Prefix} ");
        var hour = HourIn12HourClock(time.Hour);
        var minute = MinuteRoundedDown(time.Minute);
        var additionalMinutes = AdditionalMinutes(time.Minute);

        switch (minute)
        {
            case 0:
                s.Append(CultureInfo.InvariantCulture, $"{HourForUhr(hour)} UHR");
                break;
            case 5:
                s.Append(CultureInfo.InvariantCulture, $"FÜNF NACH {Hour(hour)}");
                break;
            case 10:
                s.Append(CultureInfo.InvariantCulture, $"ZEHN NACH {Hour(hour)}");
                break;
            case 15:
                s.Append(CultureInfo.InvariantCulture, $"VIERTEL NACH {Hour(hour)}");
                break;
            case 20:
                s.Append(CultureInfo.InvariantCulture, $"ZWANZIG NACH {Hour(hour)}");
                break;
            case 25:
                s.Append(CultureInfo.InvariantCulture, $"FÜNF VOR HALB {Hour(hour + 1)}");
                break;
            case 30:
                s.Append(CultureInfo.InvariantCulture, $"HALB {Hour(hour + 1)}");
                break;
            case 35:
                s.Append(CultureInfo.InvariantCulture, $"FÜNF NACH HALB {Hour(hour + 1)}");
                break;
            case 40:
                s.Append(CultureInfo.InvariantCulture, $"ZWANZIG VOR {Hour(hour + 1)}");
                break;
            case 45:
                s.Append(CultureInfo.InvariantCulture, $"VIERTEL VOR {Hour(hour + 1)}");
                break;
            case 50:
                s.Append(CultureInfo.InvariantCulture, $"ZEHN VOR {Hour(hour + 1)}");
                break;
            case 55:
                s.Append(CultureInfo.InvariantCulture, $"FÜNF VOR {Hour(hour + 1)}");
                break;
        }

        return new TimeToTextFormat { TimeAsText = s.ToString(), AdditionalMinutes = additionalMinutes };
    }

    private string HourForUhr(int hour) => hour == 1 ? "EIN" : Hour(hour);

    protected override string[] Numbers =>
        ["EINS", "ZWEI", "DREI", "VIER", "FÜNF", "SECHS", "SIEBEN", "ACHT", "NEUN", "ZEHN", "ELF", "ZWÖLF"];

    protected override string Prefix => "ES IST";
}
