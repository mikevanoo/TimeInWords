namespace TimeToTextLib.Presets;

public class EnglishPrecisePreset : LanguagePreset
{
    public override TimeToTextFormat Format(DateTime time)
    {
        var hour = HourIn12HourClock(time.Hour);
        var minute = time.Minute;

        var phrase = minute switch
        {
            0 => $"{Hour(hour)} OCLOCK",
            15 => $"A QUARTER PAST {Hour(hour)}",
            30 => $"HALF PAST {Hour(hour)}",
            45 => $"A QUARTER TO {Hour(hour + 1)}",
            <= 29 => $"{GetNumberTextWithSuffix(minute)} PAST {Hour(hour)}",
            _ => $"{GetNumberTextWithSuffix(60 - minute)} TO {Hour(hour + 1)}",
        };

        return new TimeToTextFormat { TimeAsText = $"{Prefix} {phrase}", AdditionalMinutes = 0 };
    }

    private string GetNumberTextWithSuffix(int number)
    {
        var suffix = number switch
        {
            1 => " MINUTE",
            > 1 and < 15 => " MINUTES",
            > 15 and < 30 => " MINUTES",
            > 30 and < 45 => " MINUTES",
            > 45 and < 60 => " MINUTES",
            _ => string.Empty,
        };

        var numberText = GetNumberText(number);

        return numberText + suffix;
    }

    protected override string[] Numbers =>
        [
            "ONE",
            "TWO",
            "THREE",
            "FOUR",
            "FIVE",
            "SIX",
            "SEVEN",
            "EIGHT",
            "NINE",
            "TEN",
            "ELEVEN",
            "TWELVE",
            "THIRTEEN",
            "FOURTEEN",
            "",
            "SIXTEEN",
            "SEVENTEEN",
            "EIGHTEEN",
            "NINETEEN",
            "TWENTY",
            "TWENTY ONE",
            "TWENTY TWO",
            "TWENTY THREE",
            "TWENTY FOUR",
            "TWENTY FIVE",
            "TWENTY SIX",
            "TWENTY SEVEN",
            "TWENTY EIGHT",
            "TWENTY NINE",
        ];

    protected override string Prefix => "IT IS";
}
