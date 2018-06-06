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
            StatusParam param = GMData.TianWenStatus.Get("STATUS_YHSX");
            if (param == null || param.ID != "")
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
                GMData.TianWenStatus.Set("STATUS_YHSX", OUTTER.jq1Person.Process("STATUS_YHSX_PARAM_STAB"));
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
                GMData.TianWenStatus.Set("STATUS_YHSX", OUTTER.jq1Person.Process("STATUS_YHSX_PARAM_PERSON", OUTTER.suggestPerson));
                OUTTER.suggestPerson.press += 10;
            }

			EVENT_JQ1_DEAL_YHSX OUTTER;
        }

		class OPTION3 : Option
		{
            void Selected(ref string nxtEvent, ref object param)
            {
                GMData.TianWenStatus.Set("STATUS_YHSX", new StatusParam("STATUS_YHSX_PARAM_EMP"));
            }
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
                     where x.debuff.saved == ""
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
            var q = (from a in GMData.RelationManager.ProvinceMap
                     where a.province == province
                     join b in GMData.RelationManager.OfficeMap on a.office equals b.office
                     select new { b.office, b.person }).FirstOrDefault();

            return UI.Format("EVENT_JQ_SAVE_DISASTER", province.name, disaster.name, q.office.name, q.person.name);
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