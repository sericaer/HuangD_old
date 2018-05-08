using HuangDAPI;
using UnityEngine;

namespace native
{
    public partial class EVENT_YHSX_START: EVENT_HD
    {
        bool Precondition()
        {
			if(GMData.GlobalFlag.Get("yhsx") == null)
                return true;
			return false;
        }

		class OPTION1 : Option
		{
			string Selected(out string param)
			{
				GMData.GlobalFlag.Set("yhsx");

				param = "1";
				return "EVENT_STAB_DEC";
			}
		}
    }

	public partial class EVENT_YHSX_END : EVENT_HD
    {
        bool Precondition()
        {
            if (GMData.GlobalFlag.Get("yhsx") != null)
                return true;
            return false;
        }

        class OPTION1 : Option
        {
            string Selected(out string param)
            {
                GMData.GlobalFlag.Clear("yhsx");

                param = "1";
				return null;
            }
        }
    }

	public partial class EVENT_STAB_DEC : EVENT_HD
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
            string Selected(out string param)
            {
                //GMData.globalFlag.Set("yhsx");
                param = "1";
				return null;
            }
        }
    }
}