using HuangDAPI;
using System.Linq;

using UnityEngine;

namespace native
{
    class EVENT_PERSON_SUICDIE : EVENT_HD
    {
        bool Precondition()
        {
            foreach(Person p in Persons.All)
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
            void Selected(ref string nxtEvent, ref object param)
            {
                Office office = (from x in GMData.RelationManager.OfficeMap
                                 where x.person == OUTTER.currPerson
                                 select x.office).FirstOrDefault();
                
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
            if(press != 0)
            {
                Debug.Log(press);
            }
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
                    return 1.0f;
                default:
                    return 0.0f;
            }
        }

        private Person currPerson;
    }
}