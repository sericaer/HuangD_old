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

    class EVENT_EMP_DEATH : EVENT_HD
    {
        bool Precondition()
        {
            float prob = CalcProb();

            if (Probability.IsProbOccur(1.0))
                return true;

            return false;
        }

        class OPTION1 : Option
        {
            void Selected(ref string nxtEvent, ref string param)
            {
                GMData.Emp.Die();
            }
        }

        float CalcProb()
        {
            float prob = 0.0f;
            switch(GMData.Emp.Heath)
            {
                case 0:
                    prob = 1.0f;
                    break;

                case 1:
                    prob = 0.1f;
                    break;

                case 2:
                    prob = 0.05f;
                    break;

                case 3:
                    prob = 0.01f;
                    break;

                case 4:
                    prob = 0.005f;
                    break;

                case 5:
                    prob = 0.001f;
                    break;

                default:
                    prob = 0.0f;
                    break;

            }

            return prob;
        }
    }

}