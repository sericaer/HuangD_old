﻿using HuangDAPI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
namespace native
{
    class EVENT_SG1_SUGGEST_SSYD : EVENT_HD
    {
        Person Sponsor
        {
            get
            {
                return Offices.SG1.person;
            }
        }

        bool Precondition(ref dynamic result)
        {
            if (LastTriggleInterval < 50)
            {
                return false;
            }

            if (Sponsor.faction == Factions.SHI)
            {
                int powerPercent = Factions.SHI.powerPercent;
                if (powerPercent > 70)
                {
                    if (CountryFlags.SSYD.Level < 3)
                    {
                        result.maxlevel = 3;
                        result.minlevel = 3;
                        return true;
                    }
                }
                else if(powerPercent > 50)
                {
                    if (CountryFlags.SSYD.Level < 2)
                    {
                        result.maxlevel = 3;
                        result.minlevel = 2;
                        return true;
                    }
                }
                else if(powerPercent > 30)
                {
                    if (CountryFlags.SSYD.Level < 1)
                    {
                        result.maxlevel = 2;
                        result.minlevel = 1;
                        return true;
                    }
                }
                else
                {
                    if (CountryFlags.SSYD.Level < 1 && LastTriggleInterval > 60 && Probability.IsProbOccur(1/Math.Pow(Stability.current+2, 2)))
                    {
                        result.maxlevel = 1;
                        result.minlevel = 0;
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
                return UI.Format("EVENT_SG1_SUGGEST_SSYD" + Precondition.minlevel.ToString());

            }
            void Selected(dynamic Precondition, ref string nxtEvent, ref object param)
            {
                CountryFlags.SSYD.Level = Precondition.maxlevel;
                Stability.current++;
            }
        }
        class OPTION2 : Option
        {
            bool IsVisable(dynamic Precondition)
            {
                return Precondition.maxlevel != Precondition.minlevel;
            }

            string Desc(dynamic Precondition)
            {
                return UI.Format("EVENT_SG1_SUGGEST_SSYD" + Precondition.minlevel.ToString());

            }
            void Selected(dynamic Precondition, ref string nxtEvent, ref object param)
            {
                CountryFlags.SSYD.Level = Precondition.minlevel;
            }
        }
    }

    class EVENT_SG1_SUGGEST_REDUCE_SSYD : EVENT_HD
    {
        Person Sponsor
        {
            get
            {
                return Offices.SG1.person;
            }
        }

        bool Precondition(ref dynamic result)
        {
            if (LastTriggleInterval < 60)
            {
                return false;
            }

            if(Sponsor.faction == Factions.SHI)
            {
                return false;
            }

            int powerPercent = Factions.SHI.powerPercent;
            if (powerPercent < 40)
            {
                if (CountryFlags.SSYD.Level >= 3)
                {
                    if (Probability.IsProbOccur((double)1 / powerPercent))
                    {
                        return true;
                    }
                }
            }
            else if (powerPercent < 20)
            {
                if (CountryFlags.SSYD.Level >= 2)
                {
                    if (Probability.IsProbOccur((double)1/powerPercent))
                    {
                        return true;
                    }
                }
                else
                {
                    if (Probability.IsProbOccur((double)1 / (powerPercent+200)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        class OPTION1 : Option
        {
            void Selected(dynamic Precondition, ref string nxtEvent, ref object param)
            {
                CountryFlags.SSYD.Level--;

                Stability.current = Stability.current - Probability.GetRandomNum(0, (Factions.SHI.powerPercent)/10-1);
            }
        }
        class OPTION2 : Option
        {
            
        }
    }


    class EVENT_SG1_SUGGEST_INCREASE_TAX : EVENT_HD
    {
        Person Sponsor
        {
            get
            {
                return Offices.SG1.person;
            }
        }

        bool Precondition(ref dynamic result)
        {
            if (LastTriggleInterval < 20)
            {
                return false;
            }

            if (CountryFlags.TSJZ.Level > 3)
            {
                return false;
            }

            if (Economy.NetIncome > 30)
            {
                return false;
            }

            double prob = 0.0;
            if (Sponsor.faction == Factions.SHI)
            {
                prob -= 0.01;
            }
            if(Dip.current = Dip.WAR)
            {
                prob += 0.05;
            }

            if (Economy.NetIncome <= 10)
            {
                prob += 0.02;
            }
            else if (Economy.NetIncome <= 5)
            {
                prob += 0.03;
            }
            else if (Economy.NetIncome <= 0)
            {
                prob = 1.0;
            }

            return Probability.IsProbOccur(prob);
        }

        string Desc(dynamic Precondition)
        {
            return UI.Format("EVENT_SG1_SUGGEST_INCREASE_TAX");
        }

        class OPTION1 : Option
        {
            void Selected(dynamic Precondition, ref string nxtEvent, ref object param)
            {
                CountryFlags.TSJZ.Level++;
            }
        }
    }

    class EVENT_SG1_SUGGEST_REDUCE_TAX : EVENT_HD
    {
        Person Sponsor
        {
            get
            {
                return Offices.SG1.person;
            }
        }

        bool Precondition(ref dynamic result)
        {
            if (LastTriggleInterval < 60)
            {
                return false;
            }

            if (Sponsor.faction == Factions.SHI)
            {
                return false;
            }

            int powerPercent = Factions.SHI.powerPercent;
            if (powerPercent < 40)
            {
                if (CountryFlags.SSYD.Level >= 3)
                {
                    if (Probability.IsProbOccur((double)1 / powerPercent))
                    {
                        return true;
                    }
                }
            }
            else if (powerPercent < 20)
            {
                if (CountryFlags.SSYD.Level >= 2)
                {
                    if (Probability.IsProbOccur((double)1 / powerPercent))
                    {
                        return true;
                    }
                }
                else
                {
                    if (Probability.IsProbOccur((double)1 / (powerPercent + 200)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        class OPTION1 : Option
        {
            void Selected(dynamic Precondition, ref string nxtEvent, ref object param)
            {
                CountryFlags.SSYD.Level--;

                Stability.current = Stability.current - Probability.GetRandomNum(0, (Factions.SHI.powerPercent) / 10 - 1);
            }
        }
        class OPTION2 : Option
        {

        }
    }


    class EVENT_SG1_SUGGEST_INCREASE_TAX : EVENT_HD
    {
        Person Sponsor
        {
            get
            {
                return Offices.SG1.person;
            }
        }

        bool Precondition(ref dynamic result)
        {
            if (LastTriggleInterval < 30)
            {
                return false;
            }

            if (CountryFlags.TSJZ.Level == 0)
            {
                return false;
            }

            if (Economy.current < 300)
            {
                return false;
            }

            if (Economy.NetIncome <= 10)
            {
                return Probability.IsProbOccur(0.02);
            }

            if (Economy.NetIncome <= 5)
            {
                return Probability.IsProbOccur(0.03);
            }

            if (Economy.NetIncome <= 0)
            {
                return true;
            }

            return false;
        }

        string Desc(dynamic Precondition)
        {
            return UI.Format("EVENT_SG1_SUGGEST_INCREASE_TAX");
        }

        class OPTION1 : Option
        {
            void Selected(dynamic Precondition, ref string nxtEvent, ref object param)
            {
                CountryFlags.TSJZ.Level++;
            }
        }
    }

    class EVENT_SG1_SUGGEST_REDUCE_MILITARY : EVENT_HD
    {
        Person Sponsor
        {
            get
            {
                return Offices.SG1.person;
            }
        }

        bool Precondition(ref dynamic result)
        {
            if (Sponsor.faction == Factions.SHI)
            {
                if(Economy.NetIncome < 30)
                {
                    Debug.Log(((double)LastTriggleInterval - 60) / 10);
                    if(LastTriggleInterval > 90 && Probability.IsProbOccur(((double)LastTriggleInterval - 90)/10))
                    {
                        result.LowPercent = Sponsor.faction.powerPercent / 2;
                        return true;
                    }

                }
            }

            return false;
        }

        string Desc(dynamic Precondition)
        {
            return UI.Format("EVENT_SG1_SUGGEST_REDUCE_MILITARY_DESC", Precondition.LowPercent);
        }

        class OPTION1 : Option
        {
            void Selected(dynamic Precondition, ref string nxtEvent, ref object param)
            {
                Military.current = Military.current * Precondition.LowPercent / 100;
            }
        }
    }

    class EVENT_SG1_SUGGEST_INCREACE_MILITARY : EVENT_HD
    {
        Person Sponsor
        {
            get
            {
                return Offices.SG1.person;
            }
        }


        bool Precondition(ref dynamic result)
        {
            if (Sponsor.faction == Factions.XUN)
            {
                if (Economy.NetIncome > 10)
                {
                    Debug.Log(((double)LastTriggleInterval - 90) / 10);
                    if (LastTriggleInterval > 60 && Probability.IsProbOccur(((double)LastTriggleInterval - 90) / 10))
                    {
                        result.LowPercent = Sponsor.faction.powerPercent / 2;
                        return true;
                    }

                }
            }

            return false;
        }

        string Desc(dynamic Precondition)
        {
            return UI.Format("EVENT_SG1_SUGGEST_INCREACE_MILITARY", Precondition.LowPercent);
        }

        class OPTION1 : Option
        {
            void Selected(dynamic Precondition, ref string nxtEvent, ref object param)
            {
                Military.current = Military.current * (100+Precondition.LowPercent) / 100;
            }
        }
    }


    //class EVENT_SG1_SUGGEST_SSYD : EVENT_HD
    //{
    //    class OPTION1 : Option
    //    {
    //        string Desc(dynamic Precondition)
    //        {
    //            return UI.Format("EVENT_SG1_SUGGEST_SSYD" + Precondition.minlevel.ToString());

    //        }
    //        void Selected(dynamic Precondition, ref string nxtEvent, ref object param)
    //        {
    //            CountryFlags.SSYD.Level = Precondition.minlevel;
    //        }
    //    }

    //    class OPTION2 : Option
    //    {
    //        bool IsVisable(dynamic Precondition)
    //        {
    //            return Precondition.maxlevel != Precondition.minlevel;
    //        }
    //        string Desc(dynamic Precondition)
    //        {
    //            return UI.Format("EVENT_SG1_SUGGEST_SSYD" + Precondition.maxlevel.ToString());
    //        }
    //        void Selected(dynamic Precondition, ref string nxtEvent, ref object param)
    //        {
    //            CountryFlags.SSYD.Level = Precondition.maxlevel;
    //            Stability.current += 1;
    //        }
    //    }
    //}


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