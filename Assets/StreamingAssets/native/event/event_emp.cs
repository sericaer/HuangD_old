using HuangDAPI;
using System.Linq;

namespace native
{
    class EVENT_EMP_HEATH_DEC : EVENT_HD
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
            void Selected(ref string nxtEvent, ref string param)
            {
                GMData.Emp.Heath--;
            }
        }

        float CalcProb()
        {
            float prob = 0.001f;
            if (GMData.TianWenStatus.Contains("STATUS_YHSX"))
            {
                prob = 0.05f;
            }

            return prob;
        }
    }
}