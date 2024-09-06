using System;

namespace TimeToTextLib
{
    public static class TimeToText
    {
        public static TimeToTextFormat GetSimple(LanguagePreset.Language lang, DateTime time) =>
            LanguagePreset.Get(lang).Format(time);
    }
}
