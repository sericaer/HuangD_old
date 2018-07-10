using HuangDAPI;
using System.Linq;

namespace native
{
    class DECISION_TEST : DECISION
    {
        string[] TimeLine = { "TEST|10" };
        string FinishEvent = "EVENT_STAB_DEC";

        int CostDay = 3;
    }

    class DECISION_TEST2 : DECISION
    {
        string[] TimeLine = { "TEST|3" };
        string FinishEvent = "EVENT_STAB_INC";

        int CostDay = 3;
    }

    class DECISION_JS : DECISION
    {
        bool Enable()
        {
            if (GMData.Date.month == 1 && GMData.Date.day < 5)
            {
                return true;
            }

            return false;
        }

        int CostDay = 3;

        string mainResponse = "";

        //string[] TimeLine = { "JS_PROCESSING|20" };

        string EnableEvent()
        {
            return "EVENT_JQ1_SUGGEST_JS_FIRST";
        }

        string DisableEvent()
        {
            return  "EVENT_JQ1_SUGGEST_JS_LAST";
        }
        
        string StartEvent()
        {
            return "EVENT_JQ1_START_JS";
        }

        string FinishEvent()
        {
            if (Flags.Contains("EVENT_JQ1_JS_BAD_EVENT"))
            {
                return "EVENT_JQ1_JS_FAILED";
            }
            if (Flags.Contains("EVENT_JQ1_JS_GOOD_EVENT"))
            {
                return "EVENT_JQ1_JS_SUCCESS";
            }

            var probFail = 0.0;
            var probSucc = 1.0;
            if (Flags.Contains("PARAM_JQ1_JS_BIG"))
            {
                probFail = 0.1;
                probSucc = 0.9;
            }
            else if (Flags.Contains("PARAM_JQ1_JS_MID"))
            {
                probFail = 0.2;
                probSucc = 0.8;
            }
            else if (Flags.Contains("PARAM_JQ1_JS_LOW"))
            {
                probFail = 0.4;
                probSucc = 0.6;
            }

            var finEvent = "";
            Probability.ProbGroup(probFail, () => { finEvent = "EVENT_JQ1_JS_FAILED"; },
                                  probSucc, () => { finEvent = "EVENT_JQ1_JS_SUCCESS"; });
            return finEvent;
        }

        string ProcEvent()
        {
            if (Flags.Contains("EVENT_JQ1_JS_BAD_EVENT") || Flags.Contains("EVENT_JQ1_JS_GOOD_EVENT"))
            {
                return "";
            }

            if (Probability.IsProbOccur(0.2))
            {
                var procEvent = "";
                Probability.ProbGroup(0.5, () => { procEvent = "EVENT_JQ1_JS_BAD_EVENT"; },
                                      0.5, () => { procEvent = "EVENT_JQ1_JS_GOOD_EVENT"; });

                Flags.Add(procEvent);
                return procEvent;
            }

            return "";
        }
    }
}