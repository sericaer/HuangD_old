using HuangDAPI;
using System.Linq;

namespace native
{
    class DECISION_TEST : DECISION
    {
        string[] TimeLine = { "TEST|10" };
        string FinishEvent = "EVENT_STAB_DEC";


    }

    class DECISION_TEST2 : DECISION
    {
        string[] TimeLine = { "TEST|3" };
        string FinishEvent = "EVENT_STAB_INC";
    }
}