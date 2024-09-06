using System;
using System.Text;

namespace TimeToTextLib.Presets
{
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
                    s.Append($"{Hour(hour)} OCLOCK");
                    break;
                case 5:
                    s.Append($"{Numbers[4]} PAST {Hour(hour)}");
                    break;
                case 10:
                    s.Append($"{Numbers[9]} PAST {Hour(hour)}");
                    break;
                case 15:
                    s.Append($"A QUARTER PAST {Hour(hour)}");
                    break;
                case 20:
                    s.Append($"TWENTY PAST {Hour(hour)}");
                    break;
                case 25:
                    s.Append($"TWENTYFIVE PAST {Hour(hour)}");
                    break;
                case 30:
                    s.Append($"HALF PAST {Hour(hour)}");
                    break;
                case 35:
                    s.Append($"TWENTYFIVE TO {Hour(hour + 1)}");
                    break;
                case 40:
                    s.Append($"TWENTY TO {Hour(hour + 1)}");
                    break;
                case 45:
                    s.Append($"A QUARTER TO {Hour(hour + 1)}");
                    break;
                case 50:
                    s.Append($"TEN TO {Hour(hour + 1)}");
                    break;
                case 55:
                    s.Append($"FIVE TO {Hour(hour + 1)}");
                    break;
            }

            return new TimeToTextFormat() { TimeAsText = s.ToString(), AdditionalMinutes = additionalMinutes };
        }

        protected override string[] Numbers =>
            new[] { "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE" };

        protected override string Prefix => "IT IS";
    }
}
