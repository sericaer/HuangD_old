using HuangDAPI;
using System.Linq;

namespace native
{
    class EVENT_STAB_DEC : EVENT_HD
    {
        bool Precondition()
        {
            float prob = CalcProb();

            if (Probability.IsProbOccur(prob))
                return true;
            
            return false;
        }

        class OPTION1 : Option
        {
            void Selected(ref string nxtEvent, ref object param)
            {
                GMData.Stability--;
            }
        }

        float CalcProb()
        {
            float prob = 0.001f;
            StatusParam param = GMData.TianWenStatus.Get("STATUS_YHSX");
            if (param != null && param.ID == "STATUS_YHSX_PARAM_STAB")
            {
                prob += 0.08f;
            }

            return prob;
        }
    }
}