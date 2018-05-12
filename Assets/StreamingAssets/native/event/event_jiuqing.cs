using HuangDAPI;
using System.Linq;

namespace native
{
	class EVENT_JQ1_DEAL_YHSX : EVENT_HD
	{
		bool Precondition()
		{
            if (!GMData.TianWenStatus.Contains("STATUS_YHSX"))
                return false;
            if (GMData.TianWenStatus.Get("STATUS_YHSX") != null)
                return false;

            jq1Person = GMData.GetPerson(Selector.ByOffice("JQ1"));
            if (jq1Person == null)
                return false;

            suggestPerson = GetSuggestPerson();

            return true;
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

			void Selected(ref string nxtEvent, ref string param)
			{
                GMData.TianWenStatus.Set("STATUS_YHSX", OUTTER.jq1Person.Process("STATUS_YHSX_PARAM_PERSON", OUTTER.suggestPerson));
            }

			EVENT_JQ1_DEAL_YHSX OUTTER;
        }

		class OPTION3 : Option
		{
            void Selected(ref string nxtEvent, ref string param)
            {
                GMData.TianWenStatus.Set("STATUS_YHSX", "STATUS_YHSX_PARAM_EMP");
            }
        }

		Person GetSuggestPerson()
		{
			Faction factionJQ1 = GMData.GetFaction(Selector.ByOffice("JQ1"));
			Person  p = GMData.GetPerson(Selector.ByOffice("SGX").ByFactionNOT(factionJQ1.name));
			if (p == null)
			{
				p = GMData.GetPerson(Selector.ByOffice("SGX"));
			}

			return p;
		}

        Person jq1Person;
        Person suggestPerson;
	}
}