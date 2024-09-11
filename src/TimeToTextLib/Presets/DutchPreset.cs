﻿using System;
using System.Text;

namespace TimeToTextLib.Presets
{
    public class DutchPreset : LanguagePreset
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
                    s.Append($"{Hour(hour)} UUR");
                    break;
                case 5:
                    s.Append($"{Numbers[4]} OVER {Hour(hour)}");
                    break;
                case 10:
                    s.Append($"{Numbers[9]} OVER {Hour(hour)}");
                    break;
                case 15:
                    s.Append($"KWART OVER {Hour(hour)}");
                    break;
                case 20:
                    s.Append($"{Numbers[9]} VOOR HALF {Hour(hour + 1)}");
                    break;
                case 25:
                    s.Append($"{Numbers[4]} VOOR HALF {Hour(hour + 1)}");
                    break;
                case 30:
                    s.Append($"HALF {Hour(hour)}");
                    break;
                case 35:
                    s.Append($"{Numbers[4]} OVER HALF {Hour(hour + 1)}");
                    break;
                case 40:
                    s.Append($"{Numbers[9]} OVER HALF {Hour(hour + 1)}");
                    break;
                case 45:
                    s.Append($"KWART VOOR {Hour(hour + 1)}");
                    break;
                case 50:
                    s.Append($"{Numbers[9]} VOOR {Hour(hour + 1)}");
                    break;
                case 55:
                    s.Append($"{Numbers[4]} VOOR {Hour(hour + 1)}");
                    break;
            }

            return new TimeToTextFormat() { TimeAsText = s.ToString(), AdditionalMinutes = additionalMinutes };
        }

        protected override string[] Numbers =>
            new[] { "EEN", "TWEE", "DRIE", "VIER", "VIJF", "ZES", "ZEVEN", "ACHT", "NEGEN", "TIEN", "ELF", "TWAALF" };

        protected override string Prefix => "HET IS";
    }
}