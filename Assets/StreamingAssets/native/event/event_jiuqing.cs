using HuangDAPI;
using System.Linq;

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

            jq1Person = (from x in GMData.OfficeMap
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
			void Selected(ref string nxtEvent, ref string param)
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

            void Selected(ref string nxtEvent, ref string param)
			{
                GMData.TianWenStatus.Set("STATUS_YHSX", OUTTER.jq1Person.Process("STATUS_YHSX_PARAM_PERSON", OUTTER.suggestPerson));
                OUTTER.suggestPerson.press += 10;
            }

			EVENT_JQ1_DEAL_YHSX OUTTER;
        }

		class OPTION3 : Option
		{
            void Selected(ref string nxtEvent, ref string param)
            {
                GMData.TianWenStatus.Set("STATUS_YHSX", new StatusParam("STATUS_YHSX_PARAM_EMP"));
            }
        }

		Person GetSuggestPerson()
		{
            //获取JQ1对应的faction
            Faction factionJQ1 = (from x in GMData.RelationMap
                                  where x.office.name == "JQ1"
                                  select x.faction).FirstOrDefault();

            //获取SG中faction和JQ1的faction不相同的
            Person p = (from x in GMData.RelationMap
                        where x.office.name.Contains("SG")
                        where x.faction != factionJQ1
                        select x.person).FirstOrDefault();
            
			if (p == null)
			{
                //获取任意一个SGperson
                p = (from x in GMData.OfficeMap
                     where x.office.name.Contains("SG")
                     select x.person).LastOrDefault();
			}

            return p;
		}

        Person jq1Person;
        Person suggestPerson;
	}
}