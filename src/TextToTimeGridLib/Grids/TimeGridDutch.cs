using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextToTimeGridLib.Grids
{
    public class TimeGridDutch : TimeGrid
    {
        protected override string RawGrid
        {
            get
            {
                return "HETKISAVIJF"
                    + '\n'
                    + "TIENBTZVOOR"
                    + '\n'
                    + "OVERMEKWART"
                    + '\n'
                    + "HALFSPWOVER"
                    + '\n'
                    + "VOORTHGEENS"
                    + '\n'
                    + "TWEEPVCDRIE"
                    + '\n'
                    + "VIERVIJFZES"
                    + '\n'
                    + "ZEVENONEGEN"
                    + '\n'
                    + "ACHTTIENELF"
                    + '\n'
                    + "TWAALFBFUUR";
            }
        }
    }
}
