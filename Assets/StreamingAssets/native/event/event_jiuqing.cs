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

            jq1Person = GMData.GetPerson(Selector.ByOffice("JQ1"));
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

    class EVENT_PERSON_SUICDIE : EVENT_HD
    {
        bool Precondition()
        {
            Person[] persons = GMData.GetPersons();
            foreach(Person p in persons)
            {
                float prob = CalcProb(p.press);
                if (Probability.IsProbOccur(prob))
                {
                    currPerson = p;
                    return true;
                }
            }

            return false;
        }

        string Desc()
        {
            return UI.Format("EVENT_PERSON_SUICDIE_DESC", currPerson.ToString());
        }

        class OPTION1 : Option
        {
            void Selected(ref string nxtEvent, ref string param)
            {
                Office office = GMData.GetOffice(Selector.ByPerson(OUTTER.currPerson.name));
                OUTTER.currPerson.Die();

                if (office.name == "SG1" || office.name == "SG2" || office.name == "SG3")
                {
                    nxtEvent = "EVENT_STAB_DEC";
                }
            }

            EVENT_PERSON_SUICDIE OUTTER;
        }

        float CalcProb(int press)
        {
            switch(press)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    return 0.0f;
                case 5:
                case 6:
                    return 0.03f;
                case 7:
                    return 0.05f;
                case 8:
                    return 0.08f;
                case 9:
                    return 0.2f;
                case 10:
                    return 0.4f;
                default:
                    return 0.0f;
            }
        }

        private Person currPerson;
    }

    class EVENT_EMP_HEATH_DEC : EVENT_HD
    {
        bool Precondition()
        {
            float prob = CalcProb();

            if (Probability.IsProbOccur(prob))
                return true;

            return false;
        }

        class OPTION1 : Option
        {
            void Selected(ref string nxtEvent, ref string param)
            {
                GMData.Emp.Heath--;
            }
        }

        float CalcProb()
        {
            float prob = 0.001f;
            if (GMData.TianWenStatus.Contains("STATUS_YHSX"))
            {
                prob = 0.05f;
            }

            return prob;
        }
    }

    //class EVENT_SG_EMPTY : EVENT_HD
    //{
    //    bool Precondition()
    //    {
    //        emptyOffice = GMData.GetOffice(Selector.ByOffice("SGX").ByPerson(null));
    //        if (emptyOffice != null)
    //        {
    //            Person[] persons = GMData.GetPersons(Selector.ByOffice("JQX")));

    //            persons.Where();

    //            return true;
    //        }
                
    //        return false;         
    //    }

    //    class OPTION1 : Option
    //    {
    //        void Selected(ref string nxtEvent, ref string param)
    //        {
    //            GMData.Emp.Heath--;
    //        }
    //    }

    //    float CalcProb()
    //    {
    //        float prob = 0.001f;
    //        if (GMData.TianWenStatus.Contains("STATUS_YHSX"))
    //        {
    //            prob = 0.05f;
    //        }

    //        return prob;
    //    }

    //    private Office emptyOffice;
    //}
}