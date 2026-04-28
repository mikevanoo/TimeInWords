namespace TimeToTextLib.Presets;

public class EnglishPreset : LanguagePreset
{
    public override TimeToTextFormat Format(DateTime time)
    {
        var hour = HourIn12HourClock(time.Hour);
        var minute = MinuteRoundedDown(time.Minute);
        var additionalMinutes = AdditionalMinutes(time.Minute);

        var phrase = minute switch
        {
            0 => $"{Hour(hour)} OCLOCK",
            5 or 10 => $"{GetNumberText(minute)} PAST {Hour(hour)}",
            15 => $"A QUARTER PAST {Hour(hour)}",
            20 => $"TWENTY PAST {Hour(hour)}",
            25 => $"TWENTYFIVE PAST {Hour(hour)}",
            30 => $"HALF PAST {Hour(hour)}",
            35 => $"TWENTYFIVE TO {Hour(hour + 1)}",
            40 => $"TWENTY TO {Hour(hour + 1)}",
            45 => $"A QUARTER TO {Hour(hour + 1)}",
            50 => $"TEN TO {Hour(hour + 1)}",
            55 => $"FIVE TO {Hour(hour + 1)}",
            _ => throw new ArgumentOutOfRangeException(nameof(time)),
        };

        return new TimeToTextFormat { TimeAsText = $"{Prefix} {phrase}", AdditionalMinutes = additionalMinutes };
    }

    protected override string[] Numbers =>
        ["ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE"];

    protected override string Prefix => "IT IS";
}
