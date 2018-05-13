using HuangDAPI;
using System.Linq;

namespace native
{
    class EVENT_YHSX_START: EVENT_HD
    {
        bool Precondition()
        {
			if(!GMData.TianWenStatus.Contains("STATUS_YHSX"))
                return true;
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
            if (Probability.IsProbOccur(0.08))
                return true;

            return false;
        }

        class OPTION1 : Option
        {
            void Selected(ref string nxtEvent, ref string param)
            {
				GMData.TianWenStatus.Remove("STATUS_YHSX");
            }
        }
    }
}