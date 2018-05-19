using HuangDAPI;
using System.Linq;
using UnityEngine;

namespace native
{
    class EVENT_YHSX_START: EVENT_HD
    {
        bool Precondition()
        {
			if(GMData.TianWenStatus.Contains("STATUS_YHSX"))
            {
                if(Probability.IsProbOccur(0.001))
                {
                    return true;
                }
            }
			return false;
        }

        class OPTION1 : Option
		{
			void Selected(ref string nxtEvent, ref string param)
			{
				GMData.TianWenStatus.Add("STATUS_YHSX");

				param = "1";
                nxtEvent = "EVENT_STAB_DEC";
			}
		}
    }

	class EVENT_YHSX_END : EVENT_HD
    {
        bool Precondition()
        {
			if (!GMData.TianWenStatus.Contains("STATUS_YHSX"))
                return false; 
            if (Probability.IsProbOccur(0.05))
                return true;

            return false;
        }

        class OPTION1 : Option
        {
            void Selected(ref string nxtEvent, ref string param)
            {

                StatusParam statusParam = GMData.TianWenStatus.Get("STATUS_YHSX");
                if (statusParam != null && statusParam.ID == "STATUS_YHSX_PARAM_PERSON")
                {
                    PersonProcess process = statusParam as PersonProcess;
                    if(process != null)
                    {
                        Person p = process.tag[0] as Person;
                        p.press = p.press - 5;
                    }
                }

				GMData.TianWenStatus.Remove("STATUS_YHSX");
            }
        }
    }
}