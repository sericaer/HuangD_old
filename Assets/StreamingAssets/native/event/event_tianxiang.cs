using HuangDAPI;
using System.Linq;
using UnityEngine;

namespace native
{
    class EVENT_YHSX_START : EVENT_HD
    {
        bool Precondition()
        {
            if (!GMData.CountryFlags.Contains("YHSX"))
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
                GMData.CountryFlags.Add("YHSX");
                GMData.Emp.Flags.Add("YHSX", "press:+5");

                param = "1";
                nxtEvent = "EVENT_STAB_DEC";
            }
        }
    }

    class EVENT_YHSX_END : EVENT_HD
    {
        bool Precondition()
        {
            if (!GMData.CountryFlags.Contains("YHSX"))
                return false;
            if (Probability.IsProbOccur(0.05))
                return true;

            return false;
        }

        class OPTION1 : Option
        {
            void Selected(ref string nxtEvent, ref object param)
            {
                GMData.CountryFlags.Remove("YHSX");
                GMData.Emp.Flags.RemoveAll(x => x.Contains("YHSX"));

                foreach (var p in Persons.All)
                {
                    p.Flags.RemoveAll(x => x.Contains("YHSX"));
                }
            }
        }
    }
}