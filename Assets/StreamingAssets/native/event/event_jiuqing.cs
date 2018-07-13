using HuangDAPI;
using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace native
{
    class EVENT_JQ1_SUGGEST_YHSX_FIRST : EVENT_HD
    {
        string Desc()
        {
            return UI.Format("EVENT_JQ1_DEAL_YHSX_DESC", AssocDecision.ResponsiblePerson);
        }

        class OPTION1 : Option
        {
            void Selected(ref string nxtEvent, ref object param)
            {
                AssocDecision.process();
            }
        }

        class OPTION2 : Option
        {
            void Selected(ref string nxtEvent, ref object param)
            {
                
            }
        }
    }

    class EVENT_JQ1_START_YHSX : EVENT_HD
	{
        string Desc()
        {
            return UI.Format("EVENT_JQ1_START_YHSX", AssocDecision.ResponsiblePerson);
        }

        class OPTION1 : Option
		{
            void Selected(ref string nxtEvent, ref object param)
            {
                AssocDecision.Flags.Add("STATIC");
                GMData.Emp.Flags.Add("TRANSFER_YHSX", "press:-2");
            }
        }

		class OPTION2 : Option
        {
			bool Precondition()
            {
                suggestPerson = GetSuggestPerson();
                return (suggestPerson != null);
            }

            string Desc()
            {
                return UI.Format("EVENT_JQ1_START_YHSX_OPTION2_DESC", suggestPerson.ToString());
            }

            void Selected(ref string nxtEvent, ref object param)
			{
                suggestPerson.Flags.Add("TARGET_YHSX", "press:+8");
                GMData.Emp.Flags.Add("TRANSFER_YHSX", "press:-2");
            }

            Person GetSuggestPerson()
            {
                Faction factionJQ1 = AssocDecision.ResponsiblePerson.faction;

                Person p = (from x in GMData.Offices.SG
                            where x.person != null && x.person.faction != factionJQ1
                            select x.person).FirstOrDefault();

                if (p == null)
                {
                    p = (from x in GMData.Offices.SG
                         where x.person != null
                         select x.person).LastOrDefault();
                }

                return p;
            }

            Person suggestPerson = null;
        }
	}

    class EVENT_JQ1_SUGGEST_JS_FIRST : EVENT_HD
    {
        string Desc()
        {
            return UI.Format("EVENT_JQ1_SUGGEST_JS_FIRST", AssocDecision.ResponsiblePerson);
        }

        class OPTION1 : Option
        {
            void Selected(ref string nxtEvent, ref object param)
            {
                AssocDecision.process();
            }
        }
        class OPTION2 : Option
        {

        }
    }

    class EVENT_JQ1_SUGGEST_JS_LAST : EVENT_HD
    {
        string Desc()
        {
            return UI.Format("EVENT_JQ1_SUGGEST_JS_LAST", AssocDecision.ResponsiblePerson);
        }

        class OPTION1 : Option
        {
            void Selected(ref string nxtEvent, ref object param)
            {
                AssocDecision.process();
            }
        }
        class OPTION2 : Option
        {
            void Selected(ref string nxtEvent, ref object param)
            {
                nxtEvent = "EVENT_STAB_DEC";
            }
        }
    }

    class EVENT_JQ1_START_JS : EVENT_HD
    {
        string Desc()
        {
            return UI.Format("EVENT_JQ1_DEAL_JS_DESC", AssocDecision.ResponsiblePerson);
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
                AssocDecision.Flags.Add("PARAM_JQ1_JS_MAX");
                GMData.Economy -= 15;
            }
        }

        class OPTION2 : Option
        {
            bool Precondition()
            {
                if (GMData.Economy > 10)
                    return true;
                return false;
            }

            void Selected(ref string nxtEvent, ref object param)
            {
                AssocDecision.Flags.Add("PARAM_JQ1_JS_MID");
                GMData.Economy -= 10;
            }
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
                AssocDecision.Flags.Add("PARAM_JQ1_JS_MIN");
                GMData.Economy -= 5;
            }
        }
    }

    class EVENT_JQ1_JS_GOOD_EVENT : EVENT_HD
    {
        class OPTION1 : Option
        {
            void Selected(ref string nxtEvent, ref object param)
            {
                nxtEvent = "EVENT_STAB_INC";
            }
        }
    }

    class EVENT_JQ1_JS_BAD_EVENT : EVENT_HD
    {
        class OPTION1 : Option
        {
            void Selected(ref string nxtEvent, ref object param)
            {
                nxtEvent = "EVENT_STAB_DEC";
            }
        }
    }

    class EVENT_JQ1_JS_SUCCESS : EVENT_HD
    {
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
            emptyOffice = (from x in GMData.Offices.JQ
                           where x.person == null
                           select x).FirstOrDefault();
            if(emptyOffice == null)
            {
                return false;
            }

            Debug.Log("emptyOffice");

            var factionSG1 = GMData.Offices.SG[1].person.faction;
            if(factionSG1 == null)
            {
                return false;
            }

            Debug.Log("factionSG1");

            listPerson = new List<Person>();
            if (emptyOffice != null)
            {
                 
                var q = (from x in GMData.Offices.CS
                        select new { person = x.person, score = (x.person.faction == factionSG1) ? x.person.score+2 : x.person.score }).OrderByDescending(y=>y.score).Take(3);

                foreach (var v in q)
                {
                    listPerson.Add(v.person);
                }
            }
            Debug.Log("listPerson");

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
                return UI.Format("EVENT_JQ_EMPTY_OPTION1_DESC", p.ToString(), p.faction.name);
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
                return UI.Format("EVENT_JQ_EMPTY_OPTION1_DESC", p.ToString(), p.faction.name);
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
                return UI.Format("EVENT_JQ_EMPTY_OPTION1_DESC", p.ToString(), p.faction.name);
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

    class EVENT_JQ9_SAVE_DISASTER : EVENT_HD
    {
        bool Precondition()
        {
            disaster = (from x in GMData.Disasters.All
                     where x.saved == ""
                     select x).FirstOrDefault();

            if (disaster == null)
            {
                return false;
            }

            personJQ9 = GMData.Offices.JQ[8].person;
            if(personJQ9 == null)
            {
                return false;
            }

            return true;
        }

        string Desc()
        {
            return UI.Format("EVENT_JQ_SAVE_DISASTER_DESC", disaster.provinces[0].name, disaster.name, "JQ9", personJQ9.name);
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

            EVENT_JQ9_SAVE_DISASTER OUTTER;
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

            EVENT_JQ9_SAVE_DISASTER OUTTER;

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

            EVENT_JQ9_SAVE_DISASTER OUTTER;

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

            EVENT_JQ9_SAVE_DISASTER OUTTER;
        }

        private Disaster disaster;
        private Person personJQ9;
    }
}