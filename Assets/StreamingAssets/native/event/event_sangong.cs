using HuangDAPI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace native
{
    class EVENT_SG_EMPTY : EVENT_HD
    {
        bool Precondition()
        {
            emptyOffice = (from x in GMData.RelationManager.OfficeMap
                           where x.office.name.Contains("SG")
                           where x.person == null
                           select x.office).FirstOrDefault();

            listPerson = new List<Person>();

            if (emptyOffice != null)
            {
                var q = from x in GMData.RelationManager.RelationMap
                        where x.office.name.Contains("JQ")
                        group x by x.faction into g
                        select g.OrderByDescending(y => y.person.score).FirstOrDefault().person;

                foreach (var m in q)
                {
                    listPerson.Add(m);
                }

             }

            if(listPerson != null && listPerson.Count != 0)
                return true;

            return false; 
        }

        class OPTION1 : Option
        {
            string Desc()
            {
                Person p = OUTTER.listPerson[0];
                Faction f = (from x in GMData.RelationManager.FactionMap
                             where x.person == p
                             select x.faction).FirstOrDefault();

                return string.Format("{0}[{1}]", p.ToString(), f.name);
            }
            void Selected(ref string nxtEvent, ref string param)
            {
                GMData.RelationManager.SetOffice(OUTTER.listPerson[0], OUTTER.emptyOffice);
            }

            EVENT_SG_EMPTY OUTTER;
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

                return string.Format("{0}[{1}]", p.ToString(), f.name);
            }

            void Selected(ref string nxtEvent, ref string param)
            {
                GMData.RelationManager.SetOffice(OUTTER.listPerson[1], OUTTER.emptyOffice);
            }

            EVENT_SG_EMPTY OUTTER;
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

                return string.Format("{0}[{1}]", p.ToString(), f.name);
            }

            void Selected(ref string nxtEvent, ref string param)
            {
                GMData.RelationManager.SetOffice(OUTTER.listPerson[2], OUTTER.emptyOffice);
            }

            EVENT_SG_EMPTY OUTTER;
        }

        private Office emptyOffice;
        private List<Person> listPerson;
    }
}