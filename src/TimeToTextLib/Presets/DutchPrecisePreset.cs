using System;
using System.Globalization;
using System.Text;

namespace TimeToTextLib.Presets;

public class DutchPrecisePreset : LanguagePreset
{
    public override TimeToTextFormat Format(DateTime time)
    {
        var s = new StringBuilder($"{Prefix} ");
        var hour = HourIn12HourClock(time.Hour);
        var minute = time.Minute;

        if (minute == 0)
        {
            s.Append(CultureInfo.InvariantCulture, $"{Hour(hour)} UUR");
        }
        else if (minute <= 14)
        {
            s.Append(CultureInfo.InvariantCulture, $"{GetNumberText(minute)} OVER {Hour(hour)}");
        }
        else if (minute == 15)
        {
            s.Append(CultureInfo.InvariantCulture, $"KWART OVER {Hour(hour)}");
        }
        else if (minute <= 29)
        {
            s.Append(CultureInfo.InvariantCulture, $"{GetNumberText(30 - minute)} VOOR HALF {Hour(hour + 1)}");
        }
        else if (minute == 30)
        {
            s.Append(CultureInfo.InvariantCulture, $"HALF {Hour(hour + 1)}");
        }
        else if (minute <= 44)
        {
            s.Append(CultureInfo.InvariantCulture, $"{GetNumberText(minute - 30)} OVER HALF {Hour(hour + 1)}");
        }
        else if (minute == 45)
        {
            s.Append(CultureInfo.InvariantCulture, $"KWART VOOR {Hour(hour + 1)}");
        }
        else
        {
            s.Append(CultureInfo.InvariantCulture, $"{GetNumberText(60 - minute)} VOOR {Hour(hour + 1)}");
        }

        return new TimeToTextFormat { TimeAsText = s.ToString(), AdditionalMinutes = 0 };
    }

    protected override string[] Numbers =>
        [
            "ÉÉN",
            "TWEE",
            "DRIE",
            "VIER",
            "VIJF",
            "ZES",
            "ZEVEN",
            "ACHT",
            "NEGEN",
            "TIEN",
            "ELF",
            "TWAALF",
            "DERTIEN",
            "VEERTIEN",
        ];

    protected override string Prefix => "HET IS";
}
