using HuangDAPI;
using System.Linq;
using UnityEngine;

namespace native
{
    class EVENT_YHSX_START: EVENT_HD
    {
        bool Precondition()
        {
			if(!GMData.CountryFlags.Contains("STATUS_YHSX"))
            {
                if(Probability.IsProbOccur(1.0))
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
                GMData.CountryFlags.Add("STATUS_YHSX");

				param = "1";
                nxtEvent = "EVENT_STAB_DEC";
			}
		}
    }

	class EVENT_YHSX_END : EVENT_HD
    {
        bool Precondition()
        {
            if (!GMData.CountryFlags.Contains("STATUS_YHSX"))
                return false; 
            if (Probability.IsProbOccur(0.05))
                return true;

            return false;
        }

        class OPTION1 : Option
        {
            void Selected(ref string nxtEvent, ref object param)
            {
                GMData.CountryFlags.Remove("STATUS_YHSX");
                GMData.ImpWorks.Remove("DEAL_YHSX");

                var query = from x in GMData.RelationManager.OfficeMap
                            select x.person;
                foreach(var person in query)
                {
                    person.Flags.Remove("DST_YHSX");
                }
            }
        }
    }
}