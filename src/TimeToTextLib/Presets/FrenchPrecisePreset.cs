using System;
using System.Globalization;
using System.Text;

namespace TimeToTextLib.Presets;

public class FrenchPrecisePreset : LanguagePreset
{
    public override TimeToTextFormat Format(DateTime time)
    {
        var s = new StringBuilder($"{Prefix} ");
        var minute = time.Minute;

        if (minute == 0)
        {
            s.Append(HourWithHeures(time.Hour));
        }
        else if (minute == 15)
        {
            s.Append(CultureInfo.InvariantCulture, $"{HourWithHeures(time.Hour)} ET QUART");
        }
        else if (minute == 30)
        {
            s.Append(CultureInfo.InvariantCulture, $"{HourWithHeures(time.Hour)} ET {Demie(time.Hour)}");
        }
        else if (minute == 45)
        {
            s.Append(CultureInfo.InvariantCulture, $"{HourWithHeures(time.Hour + 1)} MOINS LE QUART");
        }
        else if (minute <= 29)
        {
            s.Append(CultureInfo.InvariantCulture, $"{HourWithHeures(time.Hour)} {GetNumberText(minute)}");
        }
        else
        {
            s.Append(
                CultureInfo.InvariantCulture,
                $"{HourWithHeures(time.Hour + 1)} MOINS {GetNumberText(60 - minute)}"
            );
        }

        return new TimeToTextFormat { TimeAsText = s.ToString(), AdditionalMinutes = 0 };
    }

    protected override string[] Numbers =>
        [
            "UNE",
            "DEUX",
            "TROIS",
            "QUATRE",
            "CINQ",
            "SIX",
            "SEPT",
            "HUIT",
            "NEUF",
            "DIX",
            "ONZE",
            "DOUZE",
            "TREIZE",
            "QUATORZE",
            "",
            "SEIZE",
            "DIX SEPT",
            "DIX HUIT",
            "DIX NEUF",
            "VINGT",
            "VINGT ET UNE",
            "VINGT DEUX",
            "VINGT TROIS",
            "VINGT QUATRE",
            "VINGT CINQ",
            "VINGT SIX",
            "VINGT SEPT",
            "VINGT HUIT",
            "VINGT NEUF",
        ];

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

    private static string Demie(int hour) => hour is 0 or 12 ? "DEMI" : "DEMIE";
}
