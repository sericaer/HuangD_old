using HuangDAPI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
namespace native
{
    class EVENT_SG1_SUGGEST_SSYD1 : EVENT_HD
    {
        Person Sponsor
        {
            get
            {
                return Offices.SG1.person;
            }
        }

        class OPTION1 : Option
        {
            void Selected(ref string nxtEvent, ref object param)
            {
                Decision.DECISION_SSYD1.Publish();
            }
        }
        class OPTION2 : Option
        {

        }
    }

    class EVENT_SG1_SUGGEST_SSYD2 : EVENT_HD
    {
        Person Sponsor
        {
            get
            {
                return Offices.SG1.person;
            }
        }

        class OPTION1 : Option
        {
            void Selected(ref string nxtEvent, ref object param)
            {
                Decision.DECISION_SSYD2.Publish();
            }
        }
        class OPTION2 : Option
        {
            bool IsVisable()
            {
                return Decision.DECISION_SSYD1.IsUnPublished(); 
            }

            void Selected(ref string nxtEvent, ref object param)
            {
                Decision.DECISION_SSYD1.Publish();
            }
        }
    }

    //class EVENT_SG1_SUGGEST_SSYD3 : EVENT_HD
    //{
        //Person Sponsor
        //{
        //    get
        //    {
        //        return Offices.SG1.person;
        //    }
        //}

        //class OPTION1 : Option
        //{
        //    void Selected(ref string nxtEvent, ref object param)
        //    {
        //        Decision.DECISION_SSYD3.Publish();
        //    }
        //}
        //class OPTION2 : Option
        //{
        //    bool IsVisable()
        //    {
        //        return Decision.DECISION_SSYD2.IsUnPublished(); 
        //    }

        //    void Selected(ref string nxtEvent, ref object param)
        //    {
        //        Decision.DECISION_SSYD2.Publish();
        //    }
        //}
    //}

    //class EVENT_SG1_SUGGEST_SSYD : EVENT_HD
    //{
    //    Person Sponsor
    //    {
    //        get
    //        {
    //            return Offices.SG1.person;
    //        }
    //    }

    //    bool Precondition()
    //    {
    //        if (LastTriggleInterval < 50)
    //        {
    //            return false;
    //        }

    //        if (Sponsor.faction == Factions.SHI)
    //        {
    //            int powerPercent = Factions.SHI.powerPercent;
    //            if (powerPercent > 70)
    //            {
    //                if (CountryFlags.SSYD.Level < 3)
    //                {
    //                    PreData.maxlevel = 3;
    //                    PreData.minlevel = 3;
    //                    return true;
    //                }
    //            }
    //            else if(powerPercent > 50)
    //            {
    //                if (CountryFlags.SSYD.Level < 2)
    //                {
    //                    PreData.maxlevel = 3;
    //                    PreData.minlevel = 2;
    //                    return true;
    //                }
    //            }
    //            else if(powerPercent > 30)
    //            {
    //                if (CountryFlags.SSYD.Level < 1)
    //                {
    //                    PreData.maxlevel = 2;
    //                    PreData.minlevel = 1;
    //                    return true;
    //                }
    //            }
    //            else
    //            {
    //                if (CountryFlags.SSYD.Level < 1 && LastTriggleInterval > 60 && Probability.IsProbOccur(1/Math.Pow(Stability.current+2, 2)))
    //                {
    //                    PreData.maxlevel = 1;
    //                    PreData.minlevel = 0;
    //                    return true;
    //                }
    //            }
    //        }

    //        return false;
    //    }

    //    class OPTION1 : Option
    //    {
    //        string Desc()
    //        {
    //            return UI.Format("EVENT_SG1_SUGGEST_SSYD" + PreData.minlevel.ToString());

    //        }
    //        void Selected(ref string nxtEvent, ref object param)
    //        {
    //            CountryFlags.SSYD.Level = PreData.maxlevel;
    //            Stability.current++;
    //        }
    //    }
    //    class OPTION2 : Option
    //    {
    //        bool IsVisable()
    //        {
    //            return PreData.maxlevel != PreData.minlevel;
    //        }

    //        string Desc()
    //        {
    //            return UI.Format("EVENT_SG1_SUGGEST_SSYD" + PreData.minlevel.ToString());

    //        }
    //        void Selected(ref string nxtEvent, ref object param)
    //        {
    //            CountryFlags.SSYD.Level = PreData.minlevel;
    //        }
    //    }
    //}

    //class EVENT_SG1_SUGGEST_REDUCE_SSYD : EVENT_HD
    //{
    //    Person Sponsor
    //    {
    //        get
    //        {
    //            return Offices.SG1.person;
    //        }
    //    }

    //    bool Precondition()
    //    {
    //        if (LastTriggleInterval < 60)
    //        {
    //            return false;
    //        }

    //        if(CountryFlags.SSYD.Level == 0)
    //        {
    //            return false;
    //        }

    //        if(Sponsor.faction == Factions.SHI)
    //        {
    //            return false;
    //        }

    //        int powerPercent = Factions.SHI.powerPercent+1;
    //        switch(CountryFlags.SSYD.Level)
    //        {
    //            case 3:
    //                {
    //                    if (powerPercent > 40)
    //                    {
    //                        return false;
    //                    }

    //                    return Probability.IsProbOccur(0.3 / powerPercent);
    //                }
    //                break;
    //            case 2:
    //                {
    //                    if (powerPercent > 30)
    //                    {
    //                        return false;
    //                    }

    //                    return Probability.IsProbOccur(0.2 / powerPercent);
    //                }
    //                break;
    //            case 1:
    //                {
    //                    if (powerPercent > 20)
    //                    {
    //                        return false;
    //                    }

    //                    return Probability.IsProbOccur(0.1 / powerPercent);
    //                }
    //                break;
    //            default:
    //                break;
    //        }

    //        return false;
    //    }

    //    class OPTION1 : Option
    //    {
    //        void Selected(ref string nxtEvent, ref object param)
    //        {
    //            CountryFlags.SSYD.Level--;
    //            Stability.current = Stability.current - 1;
    //        }
    //    }
    //    class OPTION2 : Option
    //    {

    //    }
    //}


    //class EVENT_SG1_SUGGEST_INCREASE_TAX : EVENT_HD
    //{
    //    Person Sponsor
    //    {
    //        get
    //        {
    //            return Offices.SG1.person;
    //        }
    //    }

    //    bool Precondition()
    //    {
    //        Debug.Log("LastTriggleInterval:" + LastTriggleInterval);

    //        if (LastTriggleInterval < 20)
    //        {
    //            return false;
    //        }


    //        if (CountryFlags.KJZS.Level >= CountryFlags.KJZS.MAX_LEVEL)
    //        {
    //            return false;
    //        }

    //        if (Economy.NetIncome > 10)
    //        {
    //            return false;
    //        }

    //        double prob = 0.0;
    //        if (Sponsor.faction == Factions.SHI)
    //        {
    //            prob -= 0.01;
    //        }
    //        if (Diplomacy.current == Diplomacy.WAR)
    //        {
    //            prob += 0.05;
    //        }

    //        if (Economy.NetIncome <= 0)
    //        {
    //            prob = 0.08;
    //        }
    //        else if (Economy.NetIncome <= 3)
    //        {
    //            prob += 0.05;
    //        }
    //        else if (Economy.NetIncome <= 5)
    //        {
    //            prob += 0.03;
    //        }

    //        return Probability.IsProbOccur(prob);
    //    }

    //    class OPTION1 : Option
    //    {
    //        void Selected( ref string nxtEvent, ref object param)
    //        {
    //            CountryFlags.KJZS.Level++;
    //        }
    //    }
    //    class OPTION2 : Option
    //    {

    //    }
    //}

    //class EVENT_SG1_SUGGEST_REDUCE_TAX : EVENT_HD
    //{
    //    Person Sponsor
    //    {
    //        get
    //        {
    //            return Offices.SG1.person;
    //        }
    //    }

    //    bool Precondition()
    //    {
    //        if (LastTriggleInterval < 60)
    //        {
    //            return false;
    //        }

    //        if (CountryFlags.KJZS.Level < 1)
    //        {
    //            return false;
    //        }

    //        var prob = 0.0;
    //        switch(CountryFlags.KJZS.Level)
    //        {
    //            case 3:
    //                {
    //                    prob = 0.001;
    //                    prob += Economy.NetIncome * 0.0001;
    //                    prob -= Math.Pow(Stability.current, 3) * 0.0001;
    //                }
    //                break;
    //            case 2:
    //                {
    //                    prob = 0.0005;
    //                    prob += Economy.NetIncome * 0.0001;
    //                    prob -= Math.Pow(Stability.current, 3) * 0.0001;
    //                }
    //                break;
    //            case 1:
    //                {
    //                    prob += Economy.NetIncome * 0.0001;
    //                    prob -= Math.Pow(Stability.current, 3) * 0.0001;
    //                }
    //                break;
    //            default:
    //                return false;
    //        }

    //        return Probability.IsProbOccur(prob);
    //    }

    //    class OPTION1 : Option
    //    {
    //        void Selected( ref string nxtEvent, ref object param)
    //        {
    //            CountryFlags.KJZS.Level--;

    //            Stability.current++;
    //        }
    //    }
    //    class OPTION2 : Option
    //    {

    //    }
    //}

    //class EVENT_SG1_SUGGEST_INCREACE_MILITARY : EVENT_HD
    //{
    //    Person Sponsor
    //    {
    //        get
    //        {
    //            return Offices.SG1.person;
    //        }
    //    }

    //    bool Precondition()
    //    {
    //        var prob = 0.0;

    //        if (Diplomacy.current == Diplomacy.WAR)
    //        {
    //            prob += 0.05;
    //        }

    //        if(Military.current == 100)
    //        {
    //            return false;
    //        }

    //        if (Military.current > 90)
    //        {
    //            prob += -0.05;
    //        }
    //        else if (Military.current > 85)
    //        {
    //            prob += -0.02;
    //        }
    //        else if (Military.current > 80)
    //        {
    //            prob += -0.01;
    //        }
    //        else if (Military.current > 75)
    //        {
    //            prob += -0.005;
    //        }
    //        else if(Military.current > 70)
    //        {
    //            prob += -0.002;
    //        }
    //        else if (Military.current > 65)
    //        {
    //            prob += -0.001;
    //        }
    //        else if (Military.current > 60)
    //        {
    //            prob += 0.0;
    //        }
    //        else if (Military.current > 50)
    //        {
    //            prob += 0.001;
    //        }
    //        else if (Military.current > 40)
    //        {
    //            prob += 0.005;
    //        }
    //        else if (Military.current > 30)
    //        {
    //            prob += 0.01;
    //        }
    //        else if (Military.current > 20)
    //        {
    //            prob += 0.02;
    //        }
    //        else if (Military.current > 10)
    //        {
    //            prob += 0.05;
    //        }
    //        else
    //        {
    //            prob += 0.1;
    //        }

    //        if (Sponsor.faction == Factions.SHI)
    //        {
    //            prob += -0.05;
    //        }
    //        else if (Sponsor.faction == Factions.XUN)
    //        {
    //            prob += 0.05;
    //        }
    //        else if (Sponsor.faction == Factions.WAI)
    //        {
    //            prob += 0.01;
    //        }

    //        if (Economy.NetIncome < 0)
    //        {
    //            prob += -0.005;
    //        }

    //        return Probability.IsProbOccur(Math.Max(0.0, prob));
    //    }

    //    class OPTION1 : Option
    //    {
    //        void Selected( ref string nxtEvent, ref object param)
    //        {
    //            Military.current = Military.current + 15;
    //            Economy.current = Economy.current - 15;
    //        }
    //    }

    //    class OPTION2 : Option
    //    {
    //        void Selected( ref string nxtEvent, ref object param)
    //        {
    //            Military.current = Military.current + 10;
    //            Economy.current = Economy.current - 10;
    //        }
    //    }

    //    class OPTION3 : Option
    //    {
    //        void Selected( ref string nxtEvent, ref object param)
    //        {
    //            Military.current = Military.current + 5;
    //            Economy.current = Economy.current - 5;
    //        }
    //    }

    //    class OPTION4 : Option
    //    {

    //    }
    //}

    //class EVENT_SG1_SUGGEST_REDUCE_MILITARY : EVENT_HD
    //{
    //    Person Sponsor
    //    {
    //        get
    //        {
    //            return Offices.SG1.person;
    //        }
    //    }

    //    bool Precondition()
    //    {
    //        var prob = 0.0;

    //        if (Diplomacy.current == Diplomacy.PEACE)
    //        {
    //            prob += 0.01;
    //        }

    //        if (Military.current <= 10)
    //        {
    //            return false;
    //        }

    //        if (Military.current > 90)
    //        {
    //            prob += 0.05;
    //        }
    //        else if (Military.current > 85)
    //        {
    //            prob += 0.02;
    //        }
    //        else if (Military.current > 80)
    //        {
    //            prob += 0.01;
    //        }
    //        else if (Military.current > 75)
    //        {
    //            prob += 0.005;
    //        }
    //        else if (Military.current > 70)
    //        {
    //            prob += 0.002;
    //        }
    //        else if (Military.current > 65)
    //        {
    //            prob += 0.001;
    //        }
    //        else if (Military.current > 60)
    //        {
    //            prob += 0.0;
    //        }
    //        else if (Military.current > 50)
    //        {
    //            prob += -0.001;
    //        }
    //        else if (Military.current > 40)
    //        {
    //            prob += -0.005;
    //        }
    //        else if (Military.current > 30)
    //        {
    //            prob += 0.01;
    //        }
    //        else if (Military.current > 20)
    //        {
    //            prob += -0.02;
    //        }

    //        if (Sponsor.faction == Factions.SHI)
    //        {
    //            prob += 0.05;
    //        }
    //        else if (Sponsor.faction == Factions.XUN)
    //        {
    //            prob += -0.05;
    //        }
    //        else if (Sponsor.faction == Factions.WAI)
    //        {
    //            prob += -0.01;
    //        }

    //        if (Economy.NetIncome > 10)
    //        {
    //            prob += -0.005;
    //        }

    //        return Probability.IsProbOccur(Math.Max(0.0, prob));
    //    }

    //    class OPTION1 : Option
    //    {
    //        void Selected( ref string nxtEvent, ref object param)
    //        {
    //            Military.current = Military.current - 15;
    //            Economy.current = Economy.current - 10;
    //        }
    //    }

    //    class OPTION2 : Option
    //    {
    //        void Selected( ref string nxtEvent, ref object param)
    //        {
    //            Military.current = Military.current - 10;
    //            Economy.current = Economy.current - 8;
    //        }
    //    }

    //    class OPTION3 : Option
    //    {
    //        void Selected( ref string nxtEvent, ref object param)
    //        {
    //            Military.current = Military.current - 5;
    //            Economy.current = Economy.current - 5;
    //        }
    //    }

    //    class OPTION4 : Option
    //    {

    //    }
    //}

    //class EVENT_SG_EMPTY : EVENT_HD
    //{
    //    bool Precondition()
    //    {
    //        Office emptyOffice = Offices.groupCenter1.Where(x=>x.person == null).FirstOrDefault();
    //        if(emptyOffice == null)
    //        {
    //            return false;
    //        }

    //        PreData.emptyOffice = emptyOffice;
    //        PreData.preferPersons = GetPreferPersons();
    //        return true;
    //    }

    //    string Desc()
    //    {
    //        return UI.Format("EVENT_SG_EMPTY_DESC", PreData.emptyOffice.name);
    //    }

    //    class OPTION1 : Option
    //    {
    //        string Desc()
    //        {
    //            Person p = PreData.preferPersons[0];
    //            return UI.Format("EVENT_SG_EMPTY_OPTION1_DESC", p.office.name, p.name, p.score, p.faction.name);
    //        }
    //        void Selected( ref string nxtEvent, ref object param)
    //        {
    //            PreData.emptyOffice.person = PreData.preferPersons[0];
    //        }
    //    }
    //    class OPTION2 : Option
    //    {
    //        bool IsVisable()
    //        {
    //            return PreData.preferPersons.Count >= 2;
    //        }

    //        string Desc()
    //        {
    //            Person p = PreData.preferPersons[1];
    //            return UI.Format("EVENT_SG_EMPTY_OPTION1_DESC", p.office.name, p.name, p.score, p.faction.name);
    //        }

    //        void Selected( ref string nxtEvent, ref object param)
    //        {
    //            PreData.emptyOffice.person = PreData.preferPersons[1];
    //        }
    //    }

    //    class OPTION3 : Option
    //    {
    //        bool IsVisable()
    //        {
    //            return PreData.preferPersons.Count >= 3;
    //        }

    //        string Desc()
    //        {
    //            Person p = PreData.preferPersons[2];
    //            return UI.Format("EVENT_SG_EMPTY_OPTION1_DESC", p.office.name, p.name, p.score, p.faction.name);
    //        }

    //        void Selected( ref string nxtEvent, ref object param)
    //        {
    //            PreData.emptyOffice.person = PreData.preferPersons[2];
    //        }
    //    }


    //    List<Person> GetPreferPersons()
    //    {
    //        var q = from x in Offices.groupCenter2
    //                group x by x.person.faction into g
    //                select g.OrderByDescending(y => y.person.score).FirstOrDefault().person;

    //        return new List<Person>(q);
    //    }
    //}

    ////    //class EVENT_SG2_DEAL_TL : EVENT_HD
    ////    //{
    ////    //    bool Precondition()
    ////    //    {
    ////    //        if (GMData.Date.month == 10 && GMData.Date.day == 5)
    ////    //        {
    ////    //            sg2Person = GMData.Offices.SG[1].person;

    ////    //            if (sg2Person == null)
    ////    //                return false;

    ////    //            return true;
    ////    //        }

    ////    //        return false;
    ////    //    }

    ////    //    class OPTION1 : Option
    ////    //    {
    ////    //        bool Precondition()
    ////    //        {
    ////    //            if (GMData.Emp.heath > 5)
    ////    //                return true;
    ////    //            return false;
    ////    //        }

    ////    //        void Selected(ref string nxtEvent, ref object param)
    ////    //        {
    ////    //            GMData.ImpWorks.Add("DEAL_TL", "DEAL_TL_PARAM", OUTTER.sg2Person);
    ////    //            nxtEvent = "EVENT_ECO_DEC";
    ////    //            param = 5;
    ////    //        }

    ////    //        EVENT_SG2_DEAL_TL OUTTER;
    ////    //    }
    ////    //    class OPTION2 : Option
    ////    //    {
    ////    //        void Selected(ref string nxtEvent, ref object param)
    ////    //        {
    ////    //            GMData.Military -= 5;
    ////    //        }
    ////    //    }

    ////    //    Person sg2Person;
    ////    //}

    ////    //class EVENT_SG2_TL_END : EVENT_HD
    ////    //{
    ////    //    bool Precondition()
    ////    //    {
    ////    //        if (GMData.Date.month == 10 && GMData.Date.day == 20)
    ////    //        {
    ////    //            if (GMData.ImpWorks.Contains("DEAL_TL"))
    ////    //                return true;
    ////    //        }

    ////    //        return false;
    ////    //    }
    ////    //    string Desc()
    ////    //    {
    ////    //        var p = GMData.Offices.SG[1].person;
    ////    //        return UI.Format("EVENT_SG2_TL_END", p.ToString(), GMData.ImpWorks.Find("DEAL_TL").detail);
    ////    //    }

    ////    //    class OPTION1 : Option
    ////    //    {
    ////    //        void Selected(ref string nxtEvent, ref object param)
    ////    //        {
    ////    //        //    var detail = GMData.ImpWorks.Find("DEAL_JS").detail;
    ////    //        //    GMData.ImpWorks.Remove("DEAL_JS");

    ////    //        //    if (detail.Contains("EVENT_JQ1_JS_GOOD_EVENT"))
    ////    //        //    {
    ////    //        //        nxtEvent = "EVENT_JQ1_JS_SUCCESS";
    ////    //        //        return;
    ////    //        //    }
    ////    //        //    else if (detail.Contains("EVENT_JQ1_JS_BAD_EVENT"))
    ////    //        //    {
    ////    //        //        nxtEvent = "EVENT_JQ1_JS_FAILED";
    ////    //        //        return;
    ////    //        //    }
    ////    //        //    else if (detail.Contains("DEAL_JS_PARAM_BIG"))
    ////    //        //    {
    ////    //        //        nxtEvent = "EVENT_JQ1_JS_SUCCESS";
    ////    //        //        return;
    ////    //        //    }
    ////    //        //    else if (detail.Contains("DEAL_JS_PARAM_MID"))
    ////    //        //    {
    ////    //        //        if (Probability.IsProbOccur(0.7))
    ////    //        //        {
    ////    //        //            nxtEvent = "EVENT_JQ1_JS_SUCCESS";
    ////    //        //            return;
    ////    //        //        }
    ////    //        //        if (Probability.IsProbOccur(0.3))
    ////    //        //        {
    ////    //        //            nxtEvent = "EVENT_JQ1_JS_FAILED";
    ////    //        //            return;
    ////    //        //        }
    ////    //        //    }
    ////    //        //    else if (detail.Contains("DEAL_JS_PARAM_LOW"))
    ////    //        //    {
    ////    //        //        if (Probability.IsProbOccur(0.7))
    ////    //        //        {
    ////    //        //            nxtEvent = "EVENT_JQ1_JS_FAILED";
    ////    //        //            return;
    ////    //        //        }
    ////    //        //        else
    ////    //        //        {
    ////    //        //            nxtEvent = "EVENT_JQ1_JS_SUCCESS";
    ////    //        //            return;
    ////    //        //        }
    ////    //        //    }
    ////    //        }
    ////    //    }
    ////    //}

    ////    //class EVENT_SG2_TL_WHITE_DEER : EVENT_HD
    ////    //{
    ////    //    bool Precondition()
    ////    //    {
    ////    //        if (GMData.ImpWorks.Contains("DEAL_TL"))
    ////    //            return false;
    ////    //        if (Probability.IsProbOccur(0.005))
    ////    //            return true;
    ////    //        return false;
    ////    //    }

    ////    //    class OPTION1 : Option
    ////    //    {
    ////    //        void Selected(ref string nxtEvent, ref object param)
    ////    //        {
    ////    //            nxtEvent = "EVENT_STAB_INC";
    ////    //        }
    ////    //    }
    ////    //}

    ////    //class EVENT_SG2_TL_TIGER_EVENT : EVENT_HD
    ////    //{
    ////    //    bool Precondition()
    ////    //    {
    ////    //        if (!GMData.ImpWorks.Contains("DEAL_TL"))
    ////    //            return false;
    ////    //        if (Probability.IsProbOccur(0.005))
    ////    //            return true;
    ////    //        return false;
    ////    //    }

    ////    //    class OPTION1 : Option
    ////    //    {
    ////    //        void Selected(ref string nxtEvent, ref object param)
    ////    //        {
    ////    //            if (Probability.IsProbOccur(0.6))
    ////    //            {
    ////    //                //GMData.Emp.Flags.Add("TIGER_BITE", "heath:-4");
    ////    //                nxtEvent = "EVENT_TIGER_BITE_EMP";
    ////    //                return;
    ////    //            }
    ////    //            if (Probability.IsProbOccur(0.4))
    ////    //            {
    ////    //                //GMData.Emp.Flags.Add("TIGER_KILLER", "power:+5");
    ////    //                nxtEvent = "EVENT_TIGER_KILLER_EMP";
    ////    //                return;
    ////    //            }
    ////    //        }
    ////    //    }
    ////    //    class OPTION2 : Option
    ////    //    {
    ////    //        void Selected(ref string nxtEvent, ref object param)
    ////    //        {
    ////    //            if (Probability.IsProbOccur(0.4))
    ////    //            {
    ////    //                nxtEvent = "EVENT_TIGER_BITE_SG2";
    ////    //                return;
    ////    //            }
    ////    //            if (Probability.IsProbOccur(0.4))
    ////    //            {
    ////    //                //GMData.Emp.Flags.Add("TIGER_KILLER", "power:+5");
    ////    //                nxtEvent = "EVENT_TIGER_KILLER_SG2";
    ////    //                return;
    ////    //            }
    ////    //            if (Probability.IsProbOccur(0.1))
    ////    //            {
    ////    //                //GMData.Emp.Flags.Add("TIGER_KILLER", "power:+5");
    ////    //                nxtEvent = "EVENT_TIGER_KILLER_EMP";
    ////    //                return;
    ////    //            }
    ////    //            if (Probability.IsProbOccur(0.1))
    ////    //            {
    ////    //                nxtEvent = "EVENT_TIGER_BITE_EMP";
    ////    //                return;
    ////    //            }
    ////    //        }
    ////    //    }
    ////    //    class OPTION3 : Option
    ////    //    {
    ////    //        void Selected(ref string nxtEvent, ref object param)
    ////    //        {
    ////    //            nxtEvent = "EVENT_TIGER_LOSER_EMP";
    ////    //        }
    ////    //    }
    ////    //}

    ////    //class EVENT_TIGER_BITE_EMP : EVENT_HD
    ////    //{
    ////    //    bool Precondition()
    ////    //    {
    ////    //        return false;
    ////    //    }

    ////    //    class OPTION1 : Option
    ////    //    {
    ////    //        void Selected(ref string nxtEvent, ref object param)
    ////    //        {
    ////    //            GMData.Emp.Flags.Add("TIGER_BITE", "heath:-4");
    ////    //            nxtEvent = "EVENT_STAB_DEC";
    ////    //        }
    ////    //    }
    ////    //}

    ////    //class EVENT_TIGER_KILLER_EMP : EVENT_HD
    ////    //{
    ////    //    bool Precondition()
    ////    //    {
    ////    //        return false;
    ////    //    }

    ////    //    class OPTION1 : Option
    ////    //    {
    ////    //        void Selected(ref string nxtEvent, ref object param)
    ////    //        {
    ////    //            GMData.Emp.Flags.Add("TIGER_KILLER", "power:+5");

    ////    //        }
    ////    //    }
    ////    //}

    ////    //class EVENT_TIGER_BITE_SG2 : EVENT_HD
    ////    //{
    ////    //    bool Precondition()
    ////    //    {
    ////    //        return false;
    ////    //    }

    ////    //    class OPTION1 : Option
    ////    //    {
    ////    //        void Selected(ref string nxtEvent, ref object param)
    ////    //        {
    ////    //            var sg2Person = GMData.Offices.SG[1].person;

    ////    //            sg2Person.Flags.Add("TIGER_BITE", "heath:-4");

    ////    //        }
    ////    //    }
    ////    //}

    ////    //class EVENT_TIGER_KILLER_SG2 : EVENT_HD
    ////    //{
    ////    //    bool Precondition()
    ////    //    {
    ////    //        return false;
    ////    //    }

    ////    //    class OPTION1 : Option
    ////    //    {
    ////    //        void Selected(ref string nxtEvent, ref object param)
    ////    //        {
    ////    //            var sg2Person = GMData.Offices.SG[1].person;

    ////    //            sg2Person.Flags.Add("TIGER_KILLER", "power:+5");

    ////    //        }
    ////    //    }
    ////    //}

    ////    //class EVENT_TIGER_LOSER_EMP : EVENT_HD
    ////    //{
    ////    //    bool Precondition()
    ////    //    {
    ////    //        return false;
    ////    //    }

    ////    //    class OPTION1 : Option
    ////    //    {
    ////    //        void Selected(ref string nxtEvent, ref object param)
    ////    //        {
    ////    //            GMData.Emp.Flags.Add("TIGER_LOSER", "power:-4");
    ////    //        }
    ////    //    }
    ////    //}
}