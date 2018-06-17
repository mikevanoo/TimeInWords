using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeToTextLib.Presets
{
    public class DutchPreset : LanguagePreset
    {
        public override TimeToTextFormat Format(DateTime time)
        {
            StringBuilder s = new StringBuilder();

            s.Append(Prefix + " ");

            int hour = time.Hour;

            //we want 12 hour clock
            if (hour >= 12)
                hour -= 12;

            int minute = time.Minute;
            int additionalMinutes;

            //round minute to multiple of five
            if (minute >= 35)
            {
                //round up when at 35 minutes past or closer to the hour
                minute = (int)(5.0d * (Math.Ceiling(Math.Abs(minute / 5.0d))));

                if (minute == 60)
                {
                    minute = 0;
                    hour++;
                }
                
                //now get the additional minutes, on top of the rounded value
                additionalMinutes = minute - time.Minute;
            }
            else
            {
                //round down until 34 past the hour, when we start describing the time as "to" rather than "past"
                minute = (int)(5.0d * (Math.Floor(Math.Abs(minute / 5.0d))));    
                
                //now get the additional minutes, on top of the rounded value
                additionalMinutes = time.Minute - minute;
            }

            switch (minute)
            {
                case 0:
                    s.Append(Hour(hour) + " UUR");
                    break;
                case 5:
                    s.Append(Numbers[4] + " OVER " + Hour(hour));
                    break;
                case 10:
                    s.Append(Numbers[9] + " OVER " + Hour(hour));
                    break;
                case 15:
                    s.Append("KWART OVER " + Hour(hour));
                    break;
                case 20:
                    s.Append(Numbers[9] + " VOOR HALF " + Hour(hour + 1));
                    break;
                case 25:
                    s.Append(Numbers[4] + " VOOR HALF " + Hour(hour + 1));
                    break;
                case 30:
                    s.Append("HALF " + Hour(hour));
                    break;
                case 35:
                    s.Append(Numbers[4] + " OVER HALF " + Hour(hour + 1));
                    break;
                case 40:
                    s.Append(Numbers[9] + " OVER HALF " + Hour(hour + 1));
                    break;
                case 45:
                    s.Append("KWART VOOR " + Hour(hour + 1));
                    break;
                case 50:
                    s.Append(Numbers[9] + " VOOR " + Hour(hour + 1));
                    break;
                case 55:
                    s.Append(Numbers[4] + " VOOR " + Hour(hour + 1));
                    break;
            }

            return new TimeToTextFormat()
            {
                TimeAsText = s.ToString(),
                AdditionalMinutes = additionalMinutes
            };
        }

        protected override string[] Numbers
        {
            get
            {
                return new string[] { "EEN", "TWEE", "DRIE", "VIER", "VIJF", "ZES", "ZEVEN", "ACHT", "NEGEN", "TIEN", "ELF", "TWAALF" };
            }
        }

        protected override string Prefix
        {
            get { return "HET IS"; }
        }
    }
}
