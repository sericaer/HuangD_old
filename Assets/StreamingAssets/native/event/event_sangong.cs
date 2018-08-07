using HuangDAPI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
namespace native
{
//    //class DynamicEnum
//    //{
//    //    enum TEST
//    //    {
//    //        t1,
//    //    }
        
//    //    TEST _test;
        
//    //    void Selected(dynamic param)
//    //    {
//    //        _test = param;
//    //    }
//    //}

    class EVENT_SG1_SUGGEST_SSYD : EVENT_HD
    {

        Person Sponsor
        {
            get
            {
                return Offices.SG1.person;
            }
        }

        bool Precondition(ref dynamic Precondition)
        {
            if (Sponsor.faction == Factions.SHI)
            {
                int powerPercent = Factions.SHI.powerPercent;
                if (powerPercent > 70)
                {
                    if (!CountryFlags.SSYD3.IsEnabled())
                    {
                        Precondition.Flag1 = CountryFlags.SSYD3;
                        Precondition.Flag2 = null;
                        return true;
                    }
                }
                if (powerPercent > 50)
                {
                    if (!CountryFlags.SSYD3.IsEnabled() && !CountryFlags.SSYD2.IsEnabled())
                    {
                        Precondition.Flag1 = CountryFlags.SSYD2;
                        Precondition.Flag2 = CountryFlags.SSYD3;
                        return true;
                    }
                }
                if (powerPercent > 40)
                {
                    if (!CountryFlags.SSYD3.IsEnabled() && !CountryFlags.SSYD2.IsEnabled() && !CountryFlags.SSYD1.IsEnabled())
                    {
                        Precondition.Flag1 = CountryFlags.SSYD1;
                        Precondition.Flag2 = CountryFlags.SSYD2;
                        return true;
                    }
                }
                if (powerPercent > 30)
                {
                    if (!CountryFlags.SSYD3.IsEnabled() && !CountryFlags.SSYD2.IsEnabled() && !CountryFlags.SSYD1.IsEnabled())
                    {
                        Precondition.Flag1 = CountryFlags.SSYD1;
                        Precondition.Flag2 = null;
                        //Precondition.Flag2 = CountryFlags.SSYD1;
                        return true;
                    }
                }
            }

            return false;
        }

        class OPTION1 : Option
        {
            string Desc(dynamic Precondition)
            {
                return UI.Format("EVENT_SG1_SUGGEST_" + Precondition.Flag1.GetType().Name);
            }
            void Selected(dynamic Precondition, ref string nxtEvent, ref object param)
            {
                Precondition.Flag1.Enable();
            }
        }

        class OPTION2 : Option
        {
            bool IsVisable(dynamic Precondition)
            {
                return Precondition.Flag2 != null;
            }
            string Desc(dynamic Precondition)
            {
                return UI.Format("EVENT_SG1_SUGGEST_" + Precondition.Flag2.GetType().Name);
            }
            void Selected(dynamic Precondition, ref string nxtEvent, ref object param)
            {
                Precondition.Flag1.Enable();
                Stability.current += 1;
            }
        }
    }

    //class EVENT_SG1_SUGGEST_SSYD : EVENT_HD
    //{
        
    //    Person Sponsor
    //    {
    //        get
    //        {
    //            return Offices.SG1.person;
    //        }
    //    }

    //    bool Precondition(ref dynamic Precondition)
    //    {
    //        if (Sponsor.faction == Factions.SHI)
    //        {
    //            int powerPercent = Factions.SHI.powerPercent;
    //            if (powerPercent > 70)
    //            {
    //                if (CountryFlags.SSYD.Level < CountryFlags.SSYD.LEVEL3)
    //                {
    //                    Precondition.Flag1 = CountryFlags.SSYD.LEVEL3;
    //                    Precondition.Flag2 = null;
    //                    return true;
    //                }
    //            }
    //            if (powerPercent > 50)
    //            {
    //                if (CountryFlags.SSYD.Level < CountryFlags.SSYD.LEVEL2)
    //                {
    //                    Precondition.Flag1 = CountryFlags.SSYD.LEVEL2;
    //                    Precondition.Flag2 = CountryFlags.SSYD.LEVEL3;
    //                    return true;
    //                }
    //            }
    //            if (powerPercent > 40)
    //            {
    //                if (CountryFlags.SSYD.Level < CountryFlags.SSYD.LEVEL1)
    //                {
    //                    Precondition.Flag1 = CountryFlags.SSYD.LEVEL1;
    //                    Precondition.Flag2 = CountryFlags.SSYD.LEVEL2;
    //                    return true;
    //                }
    //            }
    //            if (powerPercent > 30)
    //            {
    //                if (!CountryFlags.SSYD.IsEnabled())
    //                {
    //                    Precondition.Flag1 = CountryFlags.SSYD.LEVEL0;
    //                    Precondition.Flag2 = CountryFlags.SSYD.LEVEL1;
    //                    return true;
    //                }
    //            }
    //        }

    //        return false;
    //    }

    //    class OPTION1 : Option
    //    {
    //        string Desc(dynamic Precondition)
    //        {
    //            return UI.Format("EVENT_SG1_SUGGEST_SSYD_LEVEL_" + Precondition.Flag1.ToString());
    //        }
    //        void Selected(dynamic Precondition, ref string nxtEvent, ref object param)
    //        {
    //            CountryFlags.SSYD.Level = Precondition.Flag1;
    //        }
    //    }

    //    class OPTION2 : Option
    //    {
    //        bool IsVisable(dynamic Precondition)
    //        {
    //            return Precondition.Flag2 != null;
    //        }

    //        string Desc(dynamic Precondition)
    //        {
    //            return UI.Format("EVENT_SG1_SUGGEST_SSYD_LEVEL_" + Precondition.Flag2.ToString());
    //        }
    //        void Selected(dynamic Precondition, ref string nxtEvent, ref object param)
    //        {
    //            CountryFlags.SSYD.Level = Precondition.Flag2;
    //            Stability.current += 1;
    //        }
    //    }
    //}

//    //class EVENT_SG1_SUGGEST_LOW_TAX
//    //{
//    //    Precondition
//    //    {
//    //        if (Offices.SG1.faction == Factions.SHI)
//    //        {
//    //            int powerPercent = Factions.SHI.PowerPercent);
//    //            if (PowerPercent > 80)
//    //            {
//    //                if (!CountryFlags.Contains(Flags.LOW_TAX_LEVEL_3))
//    //                {
//    //                    result = Flags.LOW_TAX_LEVEL_3;
//    //                }
//    //            }
//    //        }
//    //    }

//    //    OPTION1
//    //    {
//    //        Desc
//    //        {
//    //            result = UI.Format(Precondition.result[0]);
//    //        }
//    //        Selected
//    //        {
//    //            CountryFlags.Add(Precondition.result[0]);
//    //        }
//    //    }
//    //}

////    //class EVENT_SG1_SUGGEST_LOW_TAX : EVENT_HD
////    //{
////    //    bool Precondition()
////    //    {
////    //        if (Offices.Get("SG1").faction == "SHI")
////    //        {
////    //            levels = CalcSuggestLevel(Factions.Get("SHI").PowerPercent);
////    //            if (levels == null || levels.Count != 0)
////    //            {
////    //                return true;
////    //            }
////    //        }

////    //        return false;
////    //    }

////    //    class OPTION1 : Option
////    //    {
////    //        string Desc()
////    //        {
////    //            return UI.Format(OUTTER.levels[0]);
////    //        }
////    //        void Selected(ref string nxtEvent, ref object param)
////    //        {
////    //            CountryFlags.Add(levels[0]);
////    //        }
////    //    }
////    //    class OPTION2 : Option
////    //    {
////    //        bool Precondition()
////    //        {
////    //            return (levels.Count == 2);
////    //        }

////    //        string Desc()
////    //        {
////    //            return UI.Format(levels[1]);
////    //        }

////    //        void Selected(ref string nxtEvent, ref object param)
////    //        {
////    //            if(levels[1] != "LOW_TAX_LEVEL_NULL")
////    //            {
////    //                CountryFlags.Add(levels[1]);
////    //            }
////    //        }
////    //    }

////    //    List<string> CalcSuggestLevel(int PowerPercent)
////    //    {
////    //        List<string> rslt = new List<string>();
////    //        double prob = 0.0;
////    //        if (PowerPercent > 80)
////    //        {
////    //            if (CountryFlag.Contains("LOW_TAX_LEVEL_3"))
////    //            {
////    //                return null;
////    //            }

////    //            rslt.Add("LOW_TAX_LEVEL_3");

////    //            if(Economy.netIncome >= 30)
////    //            {
////    //                prob = 1.0;
////    //            }
////    //            else if (Economy.netIncome >= 20)
////    //            {
////    //                prob = 0.5;
////    //            }
////    //            else if (Economy.netIncome >= 10)
////    //            {
////    //                prob = 0.1;
////    //            }
////    //            else
////    //            {
////    //                return null;
////    //            }

////    //            if (Economy.current >= 200)
////    //            {
////    //                prob = prob * (Math.Pow((Economy.current / 100), 2.0) * 0.01);
////    //            }
////    //            else
////    //            {
////    //                return null;
////    //            }
////    //        }
////    //        else if (PowerPercent > 70)
////    //        {
////    //            if (CountryFlag.Contains("LOW_TAX_LEVEL_2") || CountryFlag.Contains("LOW_TAX_LEVEL_3"))
////    //            {
////    //                return null;
////    //            }

////    //            rslt.AddRange("LOW_TAX_LEVEL_3");
////    //            rslt.AddRange("LOW_TAX_LEVEL_2");

////    //            if (Economy.netIncome >= 30)
////    //            {
////    //                prob = 1.0;
////    //            }
////    //            else if (Economy.netIncome >= 20)
////    //            {
////    //                prob = 0.5;
////    //            }
////    //            else if (Economy.netIncome >= 10)
////    //            {
////    //                prob = 0.1;
////    //            }
////    //            else
////    //            {
////    //                return null;
////    //            }

////    //            if (Economy.current >= 200)
////    //            {
////    //                prob = prob * (Math.Pow((Economy.current / 100), 2.0) * 0.01);
////    //            }
////    //            else
////    //            {
////    //                return null;
////    //            }
////    //        }
////    //        else if (PowerPercent > 50)
////    //        {
////    //            if (CountryFlag.Contains("LOW_TAX_LEVEL_1") || CountryFlag.Contains("LOW_TAX_LEVEL_2") || CountryFlag.Contains("LOW_TAX_LEVEL_3"))
////    //            {
////    //                return null;
////    //            }
////    //            rslt.AddRange("LOW_TAX_LEVEL_2");
////    //            rslt.AddRange("LOW_TAX_LEVEL_1");

////    //            if (Economy.netIncome >= 40)
////    //            {
////    //                prob = 1.0;
////    //            }
////    //            else if (Economy.netIncome >= 30)
////    //            {
////    //                prob = 0.5;
////    //            }
////    //            else if (Economy.netIncome >= 20)
////    //            {
////    //                prob = 0.1;
////    //            }
////    //            else
////    //            {
////    //                return null;
////    //            }

////    //            if (Economy.current >= 300)
////    //            {
////    //                prob = prob * (Math.Pow((Economy.current / 100), 2.0) * 0.01);
////    //            }
////    //            else
////    //            {
////    //                return null;
////    //            }
////    //        }
////    //        else if (PowerPercent > 30)
////    //        {
////    //            if(CountryFlag.Contains("LOW_TAX_LEVEL_1") || CountryFlag.Contains("LOW_TAX_LEVEL_2") || CountryFlag.Contains("LOW_TAX_LEVEL_3"))
////    //            {
////    //                return null;
////    //            }

////    //            rslt.AddRange("LOW_TAX_LEVEL_1");
////    //            rslt.AddRange("LOW_TAX_LEVEL_NULL");

////    //            if (Economy.netIncome >= 30)
////    //            {
////    //                prob = 0.1;
////    //            }
////    //            else
////    //            {
////    //                return null;
////    //            }

////    //            if (Economy.current >= 500)
////    //            {
////    //                prob = prob * (Math.Pow((Economy.current / 100), 2.0) * 0.01);
////    //            }
////    //            else
////    //            {
////    //                return null;
////    //            }
////    //        }
////    //        else
////    //        {
////    //            return null;
////    //        }

////    //        if (Probability.IsProbOccur(prob))
////    //        {
////    //            return rslt.ToArray();
////    //        }

////    //        return null;
////    //    }

////    //    private List<string> levels = null;
////    //}

////    //class EVENT_SG_EMPTY : EVENT_HD
////    //{
////    //    bool Precondition()
////    //    {
////    //        emptyOffice = (from x in GMData.Offices.SG
////    //                       where x.person == null
////    //                       select x).FirstOrDefault();

////    //        listPerson = new List<Person>();

////    //        if (emptyOffice != null)
////    //        {
////    //            var q = from x in GMData.Offices.JQ
////    //                    group x by x.person.faction into g
////    //                    select g.OrderByDescending(y => y.person.score).FirstOrDefault().person;

////    //            foreach (var m in q)
////    //            {
////    //                listPerson.Add(m);
////    //            }

////    //         }

////    //        if(listPerson != null && listPerson.Count != 0)
////    //            return true;

////    //        return false; 
////    //    }

////    //    string Desc()
////    //    {
////    //        return UI.Format("EVENT_SG_EMPTY_DESC", emptyOffice.name);
////    //    }

////    //    class OPTION1 : Option
////    //    {
////    //        string Desc()
////    //        {
////    //            Person p = OUTTER.listPerson[0];
////    //            return UI.Format("EVENT_SG_EMPTY_OPTION1_DESC", p.ToString(), p.faction.name);
////    //        }
////    //        void Selected(ref string nxtEvent, ref object param)
////    //        {
////    //            GMData.RelationManager.SetOffice(OUTTER.listPerson[0], OUTTER.emptyOffice);
////    //        }
////    //    }
////    //    class OPTION2 : Option
////    //    {
////    //        bool Precondition()
////    //        {
////    //            return OUTTER.listPerson.Count >= 2;
////    //        }

////    //        string Desc()
////    //        {
////    //            Person p = OUTTER.listPerson[1];
////    //            return UI.Format("EVENT_SG_EMPTY_OPTION1_DESC", p.ToString(), p.faction.name);
////    //        }

////    //        void Selected(ref string nxtEvent, ref object param)
////    //        {
////    //            GMData.RelationManager.SetOffice(OUTTER.listPerson[1], OUTTER.emptyOffice);
////    //        }
////    //    }

////    //    class OPTION3 : Option
////    //    {
////    //        bool Precondition()
////    //        {
////    //            return OUTTER.listPerson.Count >= 3;
////    //        }

////    //        string Desc()
////    //        {
////    //            Person p = OUTTER.listPerson[2];
////    //            return UI.Format("EVENT_SG_EMPTY_OPTION1_DESC", p.ToString(), p.faction.name);
////    //        }

////    //        void Selected(ref string nxtEvent, ref object param)
////    //        {
////    //            GMData.RelationManager.SetOffice(OUTTER.listPerson[2], OUTTER.emptyOffice);
////    //        }
////    //    }

////    //    private Office emptyOffice;
////    //    private List<Person> listPerson;
////    //}

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