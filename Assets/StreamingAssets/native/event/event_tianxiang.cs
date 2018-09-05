using HuangDAPI;
using System.Linq;
using UnityEngine;

namespace native
{
    class EVENT_YHSX_START : EVENT_HD
    {
        bool Precondition()
        {
            if (!CountryFlags.YHSX.IsEnabled())
            {
                if (Probability.IsProbOccur(1.0))
                {
                    return true;
                }
            }
            return false;
        }

        class OPTION1 : Option
        {
            void Selected(ref string nxtEvent, ref object param)
            {
                CountryFlags.YHSX.Enable();

                param = "1";
                nxtEvent = "EVENT_STAB_DEC";
            }
        }
    }

    class EVENT_YHSX_END : EVENT_HD
    {
        bool Precondition()
        {
            if (CountryFlags.YHSX.IsEnabled())
            {
                if (Probability.IsProbOccur(0.05))
                {
                    return true;
                }
            }
            return false;
        }

        class OPTION1 : Option
        {
            void Selected(ref string nxtEvent, ref object param)
            {
                CountryFlags.YHSX.Disable();
            }
        }
    }
}