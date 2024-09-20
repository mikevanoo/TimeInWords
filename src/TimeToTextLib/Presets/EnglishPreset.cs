using System;
using System.Globalization;
using System.Text;

namespace TimeToTextLib.Presets;

public class EnglishPreset : LanguagePreset
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
                s.Append(CultureInfo.InvariantCulture, $"{Hour(hour)} OCLOCK");
                break;
            case 5:
                s.Append(CultureInfo.InvariantCulture, $"{Numbers[4]} PAST {Hour(hour)}");
                break;
            case 10:
                s.Append(CultureInfo.InvariantCulture, $"{Numbers[9]} PAST {Hour(hour)}");
                break;
            case 15:
                s.Append(CultureInfo.InvariantCulture, $"A QUARTER PAST {Hour(hour)}");
                break;
            case 20:
                s.Append(CultureInfo.InvariantCulture, $"TWENTY PAST {Hour(hour)}");
                break;
            case 25:
                s.Append(CultureInfo.InvariantCulture, $"TWENTYFIVE PAST {Hour(hour)}");
                break;
            case 30:
                s.Append(CultureInfo.InvariantCulture, $"HALF PAST {Hour(hour)}");
                break;
            case 35:
                s.Append(CultureInfo.InvariantCulture, $"TWENTYFIVE TO {Hour(hour + 1)}");
                break;
            case 40:
                s.Append(CultureInfo.InvariantCulture, $"TWENTY TO {Hour(hour + 1)}");
                break;
            case 45:
                s.Append(CultureInfo.InvariantCulture, $"A QUARTER TO {Hour(hour + 1)}");
                break;
            case 50:
                s.Append(CultureInfo.InvariantCulture, $"TEN TO {Hour(hour + 1)}");
                break;
            case 55:
                s.Append(CultureInfo.InvariantCulture, $"FIVE TO {Hour(hour + 1)}");
                break;
        }

        return new TimeToTextFormat() { TimeAsText = s.ToString(), AdditionalMinutes = additionalMinutes };
    }

    protected override string[] Numbers =>
        new[] { "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE" };

    protected override string Prefix => "IT IS";
}
