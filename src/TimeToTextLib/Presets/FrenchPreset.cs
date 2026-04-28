using System.Globalization;
using System.Text;

namespace TimeToTextLib.Presets;

public class FrenchPreset : LanguagePreset
{
    public override TimeToTextFormat Format(DateTime time)
    {
        var s = new StringBuilder($"{Prefix} ");
        var minute = MinuteRoundedDown(time.Minute);
        var additionalMinutes = AdditionalMinutes(time.Minute);

        switch (minute)
        {
            case 0:
                s.Append(CultureInfo.InvariantCulture, $"{HourWithHeures(time.Hour)}");
                break;
            case 5:
                s.Append(CultureInfo.InvariantCulture, $"{HourWithHeures(time.Hour)} {Numbers[4]}");
                break;
            case 10:
                s.Append(CultureInfo.InvariantCulture, $"{HourWithHeures(time.Hour)} {Numbers[9]}");
                break;
            case 15:
                s.Append(CultureInfo.InvariantCulture, $"{HourWithHeures(time.Hour)} ET QUART");
                break;
            case 20:
                s.Append(CultureInfo.InvariantCulture, $"{HourWithHeures(time.Hour)} VINGT");
                break;
            case 25:
                s.Append(CultureInfo.InvariantCulture, $"{HourWithHeures(time.Hour)} VINGT-CINQ");
                break;
            case 30:
                s.Append(CultureInfo.InvariantCulture, $"{HourWithHeures(time.Hour)} ET DEMIE");
                break;
            case 35:
                s.Append(CultureInfo.InvariantCulture, $"{HourWithHeures(time.Hour + 1)} MOINS VINGT-CINQ");
                break;
            case 40:
                s.Append(CultureInfo.InvariantCulture, $"{HourWithHeures(time.Hour + 1)} MOINS VINGT");
                break;
            case 45:
                s.Append(CultureInfo.InvariantCulture, $"{HourWithHeures(time.Hour + 1)} MOINS LE QUART");
                break;
            case 50:
                s.Append(CultureInfo.InvariantCulture, $"{HourWithHeures(time.Hour + 1)} MOINS DIX");
                break;
            case 55:
                s.Append(CultureInfo.InvariantCulture, $"{HourWithHeures(time.Hour + 1)} MOINS CINQ");
                break;
        }

        return new TimeToTextFormat() { TimeAsText = s.ToString(), AdditionalMinutes = additionalMinutes };
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
