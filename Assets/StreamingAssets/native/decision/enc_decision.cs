using HuangDAPI;
using System.Linq;

namespace native
{
    class DECISION_YHSX : DECISION
    {
        bool Visable()
        {
            if (GMData.CountryFlags.Contains("STATUS_YHSX"))
            {
                return true;
            }

            return false;
        }

        bool ProcFinish()
        {
            if (!GMData.CountryFlags.Contains("STATUS_YHSX"))
            {
                return true;
            }

            return false;
        }

        int CostDay = 0;
        string Responsible = "JQ1";
        string EnableEvent()
        {
            return "EVENT_JQ1_SUGGEST_YHSX_FIRST";
        }

        string StartEvent()
        {
            return "EVENT_JQ1_START_YHSX";
        }

        string ProcEvent()
        {
            if (Flags.Contains("STATIC"))
            {
                if (Probability.IsProbOccur(0.02))
                {
                    return "EVENT_STAB_DEC";
                }
            }

            return "";
        }
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

        int CostDay = 20;

        string Responsible = "JQ1";

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
            var probSucc = 0.0;
            if (Flags.Contains("PARAM_JQ1_JS_MAX"))
            {
                probFail = 0.1;
                probSucc = 0.9;
            }
            else if (Flags.Contains("PARAM_JQ1_JS_MID"))
            {
                probFail = 0.2;
                probSucc = 0.8;
            }
            else if (Flags.Contains("PARAM_JQ1_JS_MIN"))
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

            if (Probability.IsProbOccur(0.01))
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