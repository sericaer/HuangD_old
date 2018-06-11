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
            if(GMData.CountryFlags.Contains("STATUS_YHSX"))
            {
                prob += 0.003f;
            }

            var impwork = GMData.ImpWorks.Find("STATUS_YHSX");
            if (impwork != null && impwork.detail == "STATUS_YHSX_PARAM_STAB")
            {
                prob += 0.003f;
            }

            return prob;
        }
    }

    class EVENT_STAB_INC : EVENT_HD
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
                GMData.Stability++;
            }
        }

        float CalcProb()
        {
            float prob = 0.001f;
            return prob;
        }
    }

}