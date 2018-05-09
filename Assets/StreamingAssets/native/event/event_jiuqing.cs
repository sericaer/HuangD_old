﻿using HuangDAPI;
using System.Linq;

namespace native
{
	class EVENT_JQ1_DEAL_YHSX : EVENT_HD
	{
		bool Precondition()
		{
			if (GMData.TianxiangStatus.Get(TIANXIANG.YHSX) == "" && GMData.GetPerson(Selector.ByOffice("JQ1")) != null)
				return true;
			return false;
		}

		void Initialize(string param)
		{
			suggestPerson = GetSuggestPerson();
		}

		class OPTION1 : Option
		{
			void Selected(ref string nxtEvent, ref string param)
			{
				GMData.GlobalFlag.Set("yhsx", "Stab");
			}
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
				GMData.GlobalFlag.Set("yhsx", OUTTER.suggestPerson.name);
                OUTTER.suggestPerson.AddStatus(PERSON_STATUS.Sacrifice);
			}

			EVENT_JQ1_DEAL_YHSX OUTTER;
        }

		class OPTION3 : Option
		{
            void Selected(ref string nxtEvent, ref string param)
            {
                GMData.GlobalFlag.Set("yhsx", "Stab");
            }
        }

		Person GetSuggestPerson()
		{
			Faction factionJQ1 = GMData.GetFaction(Selector.ByOffice("JQ1"));
			Person  p = GMData.GetPersons(Selector.ByOffice("SGX").ByFactionNOT(factionJQ1.name)).First();
			if (p == null)
			{
				p = GMData.GetPersons(Selector.ByOffice("SGX")).Last();
			}

			return p;
		}

		Person suggestPerson;
	}
}