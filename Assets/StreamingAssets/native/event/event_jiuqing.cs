using HuangDAPI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace native
{
	class EVENT_JQ1_DEAL_YHSX : EVENT_HD
	{
		bool Precondition()
		{
            if (!GMData.CountryFlags.Contains("STATUS_YHSX"))
            {
                return false;
            }

            if (GMData.ImpWorks.Contains("DEAL_YHSX"))
            {
                return false;
            }

            jq1Person = (from x in GMData.RelationManager.OfficeMap
                         where x.office.name == "JQ1"
                         select x.person).FirstOrDefault();

            if (jq1Person == null)
                return false;

            suggestPerson = GetSuggestPerson();

            return true;
		}

        string Desc()
        {
            return UI.Format("EVENT_JQ1_DEAL_YHSX_DESC", jq1Person.ToString());
        }

        class OPTION1 : Option
		{
            void Selected(ref string nxtEvent, ref object param)
            {
                GMData.ImpWorks.Add("DEAL_YHSX", "DEAL_YHSX_PARAM_STAB", OUTTER.jq1Person);
            }

            EVENT_JQ1_DEAL_YHSX OUTTER;
        }

		class OPTION2 : Option
        {
			bool Precondition()
            {
				if (OUTTER.suggestPerson != null)
                    return true;
                return false;
            }

            string Desc()
            {
                return UI.Format("EVENT_JQ1_DEAL_YHSX_OPTION2_DESC", OUTTER.suggestPerson.ToString());
            }

            void Selected(ref string nxtEvent, ref object param)
			{
                GMData.ImpWorks.Add("DEAL_YHSX", "STATUS_YHSX_PARAM_PERSON", OUTTER.jq1Person, OUTTER.suggestPerson);
                OUTTER.suggestPerson.Flags.Add("DST_YHSX", "Press:-5");
            }

			EVENT_JQ1_DEAL_YHSX OUTTER;
        }

		class OPTION3 : Option
		{
            void Selected(ref string nxtEvent, ref object param)
            {
                GMData.ImpWorks.Add("DEAL_YHSX", "STATUS_YHSX_PARAM_STAB", OUTTER.jq1Person);
            }

            EVENT_JQ1_DEAL_YHSX OUTTER;
        }

		Person GetSuggestPerson()
		{
            //获取JQ1对应的faction
            Faction factionJQ1 = (from x in GMData.RelationManager.RelationMap
                                  where x.office.name == "JQ1"
                                  select x.faction).FirstOrDefault();

            //获取SG中faction和JQ1的faction不相同的
            Person p = (from x in GMData.RelationManager.RelationMap
                        where x.office.name.Contains("SG")
                        where x.faction != factionJQ1
                        select x.person).FirstOrDefault();
            
			if (p == null)
			{
                //获取任意一个SGperson
                p = (from x in GMData.RelationManager.OfficeMap
                     where x.office.name.Contains("SG")
                     select x.person).LastOrDefault();
			}

            return p;
		}

        Person jq1Person;
        Person suggestPerson;
	}

    class EVENT_JQ1_DEAL_JS : EVENT_HD
    {
        bool Precondition()
        {
            if (GMData.Date.month == 1 && GMData.Date.day == 5)
            {
                jq1Person = (from x in GMData.RelationManager.OfficeMap
                             where x.office.name == "JQ1"
                             select x.person).FirstOrDefault();

                if (jq1Person == null)
                    return false;

                return true;
            }

            return false;
        }

        string Desc()
        {
            return UI.Format("EVENT_JQ1_DEAL_JS_DESC", jq1Person.ToString());
        }

        class OPTION1 : Option
        {
            bool Precondition()
            {
                if (GMData.Economy > 15)
                    return true;
                return false;
            }

            void Selected(ref string nxtEvent, ref object param)
            {
                GMData.ImpWorks.Add("DEAL_JS", "DEAL_JS_PARAM_BIG", OUTTER.jq1Person);
                GMData.Economy -= 15;
            }

            EVENT_JQ1_DEAL_JS OUTTER;
        }

        class OPTION2 : Option
        {
            bool Precondition()
            {
                if (GMData.Economy > 10 )
                    return true;
                return false;
            }

            void Selected(ref string nxtEvent, ref object param)
            {
                GMData.ImpWorks.Add("DEAL_JS", "DEAL_JS_PARAM_MID", OUTTER.jq1Person);
                GMData.Economy -= 10;
            }

            EVENT_JQ1_DEAL_JS OUTTER;
        }

        class OPTION3 : Option
        {
            bool Precondition()
            {
                if (GMData.Economy > 5)
                    return true;
                return false;
            }

            void Selected(ref string nxtEvent, ref object param)
            {
                GMData.ImpWorks.Add("DEAL_JS", "DEAL_JS_PARAM_LOW", OUTTER.jq1Person);
                GMData.Economy -= 5;
            }

            EVENT_JQ1_DEAL_JS OUTTER;
        }

        class OPTION4 : Option
        {
            void Selected(ref string nxtEvent, ref object param)
            {
                nxtEvent = "EVENT_STAB_DEC";
            }
        }

        Person jq1Person;
    }

    class EVENT_JQ1_JS_GOOD_EVENT : EVENT_HD
    {
        bool Precondition()
        {
            if (!GMData.ImpWorks.Contains("DEAL_JS"))
                return false;
            
            var detail = GMData.ImpWorks.Find("DEAL_JS").detail;
            if (detail.Contains("EVENT_JQ1_JS_GOOD_EVENT") || detail.Contains("EVENT_JQ1_JS_BAD_EVENT"))
                return false;
            
            if (Probability.IsProbOccur(0.005))
                return true;
            return false;
        }

        class OPTION1 : Option
        {
            void Selected(ref string nxtEvent, ref object param)
            {
                GMData.ImpWorks.Find("DEAL_JS").detail += "|EVENT_JQ1_JS_GOOD_EVENT";
                nxtEvent = "EVENT_STAB_INC";
            }
        }
    }

    class EVENT_JQ1_JS_BAD_EVENT : EVENT_HD
    {
        bool Precondition()
        {
            if (!GMData.ImpWorks.Contains("DEAL_JS"))
                return false;
            var detail = GMData.ImpWorks.Find("DEAL_JS").detail;
            if (detail.Contains("EVENT_JQ1_JS_GOOD_EVENT") || detail.Contains("EVENT_JQ1_JS_BAD_EVENT"))
                return false;
            if (Probability.IsProbOccur(0.005))
                return true;
            return false;
        }

        class OPTION1 : Option
        {
            void Selected(ref string nxtEvent, ref object param)
            {
                GMData.ImpWorks.Find("DEAL_JS").detail += "|EVENT_JQ1_JS_BAD_EVENT";
                nxtEvent = "EVENT_STAB_DEC";
            }
        }
    }

    class EVENT_JQ1_JS_END : EVENT_HD
    {
        bool Precondition()
        {
            if (GMData.Date.month == 2 && GMData.Date.day == 1)
            {
                if (GMData.ImpWorks.Contains("DEAL_JS"))
                    return true;
            }

            return false;
        }
        string Desc()
        {
            var jq1Person = (from x in GMData.RelationManager.OfficeMap
                         where x.office.name == "JQ1"
                         select x.person).First();
            return UI.Format("EVENT_JQ1_JS_END_DESC", jq1Person.ToString(), GMData.ImpWorks.Find("DEAL_JS").detail);
        }

        class OPTION1 : Option
        {
            void Selected(ref string nxtEvent, ref object param)
            {
                var detail = GMData.ImpWorks.Find("DEAL_JS").detail;
                GMData.ImpWorks.Remove("DEAL_JS");

                if (detail.Contains("EVENT_JQ1_JS_GOOD_EVENT"))
                {
                    nxtEvent = "EVENT_JQ1_JS_SUCCESS";
                    return;
                }
                else if (detail.Contains("EVENT_JQ1_JS_BAD_EVENT"))
                {
                    nxtEvent = "EVENT_JQ1_JS_FAILED";
                    return;
                }
                else if(detail.Contains("DEAL_JS_PARAM_BIG"))
                {
                    nxtEvent = "EVENT_JQ1_JS_SUCCESS";
                    return;
                }
                else if(detail.Contains("DEAL_JS_PARAM_MID"))
                {
                    if (Probability.IsProbOccur(0.7))
                    {
                        nxtEvent = "EVENT_JQ1_JS_SUCCESS";
                        return;
                    }
                    if (Probability.IsProbOccur(0.3))
                    {
                        nxtEvent = "EVENT_JQ1_JS_FAILED";
                        return;
                    }
                }
                else if (detail.Contains("DEAL_JS_PARAM_LOW"))
                {
                    if (Probability.IsProbOccur(0.7))
                    {
                        nxtEvent = "EVENT_JQ1_JS_FAILED";
                        return;
                    }
                    else
                    {
                        nxtEvent = "EVENT_JQ1_JS_SUCCESS";
                        return;
                    }
                }
            }
        }
    }


    class EVENT_JQ1_JS_SUCCESS : EVENT_HD
    {
        bool Precondition()
        {
            return false;
        }

        class OPTION1 : Option
        {
            void Selected(ref string nxtEvent, ref object param)
            {
                nxtEvent = "EVENT_STAB_INC";
            }
        }

    }

    class EVENT_JQ1_JS_FAILED : EVENT_HD
    {
        bool Precondition()
        {
            return false;
        }

        class OPTION1 : Option
        {
            void Selected(ref string nxtEvent, ref object param)
            {
                nxtEvent = "EVENT_STAB_DEC";
            }
        }

    }

    class EVENT_JQ_EMPTY : EVENT_HD
    {
        bool Precondition()
        {
             

            emptyOffice = (from x in GMData.RelationManager.OfficeMap
                           where x.office.name.Contains("JQ")
                           where x.person == null
                           select x.office).FirstOrDefault();
            if(emptyOffice == null)
            {
                return false;
            }

            var factionSG1 = (from x in GMData.RelationManager.RelationMap
                              where x.office.name == "SG1"
                              select x.faction).FirstOrDefault();
            if(factionSG1 == null)
            {
                return false;
            }

            listPerson = new List<Person>();
            if (emptyOffice != null)
            {
                 
                var q = (from x in GMData.RelationManager.RelationMap
                        where x.office.name.Contains("CS")
                        select new { person = x.person, score = (x.faction == factionSG1) ? x.person.score+1 : x.person.score } into g
                        select g).OrderByDescending(y=>y.score).Take(3);

                foreach (var v in q)
                {
                    listPerson.Add(v.person);
                }
            }

            if (listPerson.Count != 0)
                return true;

            return false;
        }

        string Desc()
        {
            return UI.Format("EVENT_JQ_EMPTY_DESC", emptyOffice.name);
        }

        class OPTION1 : Option
        {
            string Desc()
            {
                Person p = OUTTER.listPerson[0];
                Faction f = (from x in GMData.RelationManager.FactionMap
                             where x.person == p
                             select x.faction).FirstOrDefault();

                return UI.Format("EVENT_JQ_EMPTY_OPTION1_DESC", p.ToString(), f.name);
            }
            void Selected(ref string nxtEvent, ref object param)
            {
                GMData.RelationManager.SetOffice(OUTTER.listPerson[0], OUTTER.emptyOffice);
            }

            EVENT_JQ_EMPTY OUTTER;
        }
        class OPTION2 : Option
        {
            bool Precondition()
            {
                return OUTTER.listPerson.Count >= 2;
            }

            string Desc()
            {
                Person p = OUTTER.listPerson[1];
                Faction f = (from x in GMData.RelationManager.FactionMap
                             where x.person == p
                             select x.faction).FirstOrDefault();

                return UI.Format("EVENT_JQ_EMPTY_OPTION1_DESC", p.ToString(), f.name);
            }

            void Selected(ref string nxtEvent, ref object param)
            {
                GMData.RelationManager.SetOffice(OUTTER.listPerson[1], OUTTER.emptyOffice);
            }

            EVENT_JQ_EMPTY OUTTER;
        }

        class OPTION3 : Option
        {
            bool Precondition()
            {
                return OUTTER.listPerson.Count >= 3;
            }

            string Desc()
            {
                Person p = OUTTER.listPerson[2];
                Faction f = (from x in GMData.RelationManager.FactionMap
                             where x.person == p
                             select x.faction).FirstOrDefault();

                return UI.Format("EVENT_JQ_EMPTY_OPTION1_DESC", p.ToString(), f.name);
            }

            void Selected(ref string nxtEvent, ref object param)
            {
                GMData.RelationManager.SetOffice(OUTTER.listPerson[2], OUTTER.emptyOffice);
            }

            EVENT_JQ_EMPTY OUTTER;
        }

        private Office emptyOffice;
        private List<Person> listPerson;
    }

    class EVENT_JQ_SAVE_DISASTER : EVENT_HD
    {
        bool Precondition()
        {
            var q = (from x in GMData.RelationManager.ProvinceStatusMap
                     where x.debuff != null && x.debuff.saved == ""
                     select new {x.province, x.debuff}).FirstOrDefault();

            if (q == null)
            {
                return false;
            }

            disaster = q.debuff;
            province = q.province;

            return true;
        }

        string Desc()
        {
            var q = (from a in GMData.RelationManager.OfficeMap
                     where a.office.name == "JQ9"
                     select a).FirstOrDefault();

            return UI.Format("EVENT_JQ_SAVE_DISASTER_DESC", province.name, disaster.name, q.office.name, q.person.name);
        }

        class OPTION1 : Option
        {
            bool Precondition()
            {
                if(GMData.Economy > 20)
                {
                    return true;
                }

                return false;
            }

            void Selected(ref string nxtEvent, ref object param)
            {
                GMData.Economy = GMData.Economy - 20;
                OUTTER.disaster.saved = "MAX";
            }

            EVENT_JQ_SAVE_DISASTER OUTTER;
        }

        class OPTION2 : Option
        {
            bool Precondition()
            {
                if (GMData.Economy > 10)
                {
                    return true;
                }

                return false;
            }

            void Selected(ref string nxtEvent, ref object param)
            {
                GMData.Economy = GMData.Economy - 10;
                OUTTER.disaster.saved = "MID";
            }

            EVENT_JQ_SAVE_DISASTER OUTTER;

        }

        class OPTION3 : Option
        {
            bool Precondition()
            {
                if (GMData.Economy > 5)
                {
                    return true;
                }

                return false;
            }

            void Selected(ref string nxtEvent, ref object param)
            {
                GMData.Economy = GMData.Economy - 5;
                OUTTER.disaster.saved = "MIN";
            }

            EVENT_JQ_SAVE_DISASTER OUTTER;

        }

        class OPTION4 : Option
        {
            void Selected(ref string nxtEvent, ref object param)
            {
                OUTTER.disaster.saved = "DELAY";

                if (Probability.IsProbOccur(0.1))
                {
                    nxtEvent = "EVENT_STAB_DEC";
                }
            }

            EVENT_JQ_SAVE_DISASTER OUTTER;
        }

        private Disaster disaster;
        private Province province;
    }
}