using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeToTextLib.Presets;

namespace TimeToTextLib
{
    public abstract class LanguagePreset
    {

        public enum Language
        {
            Dutch = 0,
            English = 1
        }

        public static Dictionary<Language, LanguagePreset> instances;

        public static LanguagePreset Get(Language lang)
        {
            if (instances == null)
                instances = new Dictionary<Language, LanguagePreset>();

            if (!instances.ContainsKey(lang))
            {
                LanguagePreset preset;

                switch (lang)
                {
                    case Language.Dutch:
                        preset = new DutchPreset();
                        break;
                    case Language.English:
                        preset = new EnglishPreset();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(lang), lang, "Language not implemented");
                }

                instances.Add(lang, preset);
            }

            return instances[lang];
        }

        public abstract TimeToTextFormat Format(DateTime time);

        protected virtual string Hour(int h)
        {
            if (h == 0)
                h = 12;
            return Numbers[h - 1];
        }

        protected abstract string[] Numbers { get; }

        protected abstract string Prefix { get; }
    }
}
