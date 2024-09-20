using System;
using System.Collections.Generic;
using TimeToTextLib.Presets;

namespace TimeToTextLib;

public abstract class LanguagePreset
{
    public enum Language
    {
        Dutch = 0,
        English = 1,
    }

    private static readonly Dictionary<Language, LanguagePreset> Instances = new Dictionary<Language, LanguagePreset>
    {
        { Language.English, new EnglishPreset() },
        { Language.Dutch, new DutchPreset() },
    };

    public static LanguagePreset Get(Language lang)
    {
        if (Instances.TryGetValue(lang, out var preset))
        {
            return preset;
        }

        throw new ArgumentOutOfRangeException(nameof(lang), lang, "Language not implemented");
    }

    public abstract TimeToTextFormat Format(DateTime time);

    protected static int HourIn12HourClock(int hour)
    {
        if (hour >= 12)
        {
            return hour - 12;
        }

        return hour;
    }

    protected static int MinuteRoundedDown(int minute) =>
        // round minutes to multiple of five, always down
        (int)(5.0d * (Math.Floor(Math.Abs(minute / 5.0d))));

    protected static int AdditionalMinutes(int minute) =>
        // additional minutes are extra to the rounded down minutes
        minute - MinuteRoundedDown(minute);

    protected string Hour(int hour)
    {
        if (hour == 0)
        {
            hour = 12;
        }

        return Numbers[hour - 1];
    }

    protected abstract string[] Numbers { get; }

    protected abstract string Prefix { get; }
}
