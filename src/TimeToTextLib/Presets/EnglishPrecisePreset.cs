using System;
using System.Globalization;
using System.Text;

namespace TimeToTextLib.Presets;

public class EnglishPrecisePreset : LanguagePreset
{
    public override TimeToTextFormat Format(DateTime time)
    {
        var s = new StringBuilder($"{Prefix} ");
        var hour = HourIn12HourClock(time.Hour);
        var minute = time.Minute;

        if (minute == 0)
        {
            s.Append(CultureInfo.InvariantCulture, $"{Hour(hour)} OCLOCK");
        }
        else if (minute == 15)
        {
            s.Append(CultureInfo.InvariantCulture, $"A QUARTER PAST {Hour(hour)}");
        }
        else if (minute == 30)
        {
            s.Append(CultureInfo.InvariantCulture, $"HALF PAST {Hour(hour)}");
        }
        else if (minute == 45)
        {
            s.Append(CultureInfo.InvariantCulture, $"A QUARTER TO {Hour(hour + 1)}");
        }
        else if (minute <= 29)
        {
            s.Append(CultureInfo.InvariantCulture, $"{GetNumberText(minute)} PAST {Hour(hour)}");
        }
        else
        {
            s.Append(CultureInfo.InvariantCulture, $"{GetNumberText(60 - minute)} TO {Hour(hour + 1)}");
        }

        return new TimeToTextFormat { TimeAsText = s.ToString(), AdditionalMinutes = 0 };
    }

    protected override string[] Numbers =>
    [
        "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN",
        "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN",
        "TWENTY", "TWENTY ONE", "TWENTY TWO", "TWENTY THREE", "TWENTY FOUR", "TWENTY FIVE", "TWENTY SIX",
        "TWENTY SEVEN", "TWENTY EIGHT", "TWENTY NINE",
    ];

    protected override string Prefix => "IT IS";
}
