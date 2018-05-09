using HuangDAPI;
using System.Linq;

namespace native
{
    class EVENT_STAB_DEC : EVENT_HD
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