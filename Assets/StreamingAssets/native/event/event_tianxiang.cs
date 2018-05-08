using HuangDAPI;
using UnityEngine;

namespace native
{
    public class EVENT_YHSX_START: EVENT_HD
    {
        bool Precondition()
        {
			if(GMData.GlobalFlag.Get("yhsx") == null)
                return true;
			return false;
        }

        class OPTION1 : Option
		{
			void Selected(ref string nxtEvent, ref string param)
			{
				GMData.GlobalFlag.Set("yhsx");

				param = "1";
                nxtEvent = "EVENT_STAB_DEC";
			}
		}
    }

	public class EVENT_YHSX_END : EVENT_HD
    {
        bool Precondition()
        {
            if (GMData.GlobalFlag.Get("yhsx") == null)
                return false;
            if (Probability.IsProbOccur(0.08))
                return true;

            return false;
        }

        class OPTION1 : Option
        {
            void Selected(ref string nxtEvent, ref string param)
            {
                GMData.GlobalFlag.Clear("yhsx");
            }
        }
    }

    public class EVENT_YHSX_SOLUTION : EVENT_HD
    {
        bool Precondition()
        {
            if (GMData.GlobalFlag.Get("yhsx") == "" && GMData.GetPerson(Selector.ByOffice("JQ1")) != null)
                return true;
            return false;
        }

        class OPTION1 : Option
        {
            void Selected(ref string nxtEvent, ref string param)
            {
                GMData.GlobalFlag.Clear("yhsx");
            }
        }
    }

    public class EVENT_STAB_DEC : EVENT_HD
    {
        bool Precondition()
        {
			return false;
        }

		void Initialize(string param)
		{
			
		}

        class OPTION1 : Option
        {
            void Selected(ref string nxtEvent, ref string param)
            {
                GMData.Stability--;
            }
        }
    }
}