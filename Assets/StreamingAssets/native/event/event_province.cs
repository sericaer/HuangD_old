using HuangDAPI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

namespace native
{
    class EVENT_CS_EMPTY : EVENT_HD
    {
        bool Precondition()
        {
            Debug.Log("EVENT_CS_EMPTY 1");

            emptyOffice = (from x in GMData.RelationManager.OfficeMap
                           where x.office.name.Contains("CS")
                           where x.person == null
                           select x.office).FirstOrDefault();

            if (emptyOffice == null)
            {
                return false;
            }

            Faction factionSG1 = (from x in GMData.RelationManager.RelationMap
                              where x.office.name == "SG1"
                              select x.faction).FirstOrDefault();
            if (factionSG1 == null)
            {
                return false;
            }

            Debug.Log("EVENT_CS_EMPTY 2");

            listPerson.Add(GMData.NewMalePerson(factionSG1));
            listPerson.Add(GMData.NewMalePerson(factionSG1));

            Faction faction3rd;
            if(Probability.IsProbOccur(0.8))
            {
                faction3rd = factionSG1;
            }
            else
            {
                Faction[] factions = (from x in GMData.RelationManager.FactionMap
                                      select x.faction).ToArray();
                faction3rd = factions[Probability.GetRandomNum(0, factions.Length)];
            }

            listPerson.Add(GMData.NewMalePerson(faction3rd));

            return true;
        }

        string Desc()
        {
            return UI.Format("EVENT_CS_EMPTY_DESC", emptyOffice.name);
        }

        class OPTION1 : Option
        {
            string Desc()
            {
                Person p = OUTTER.listPerson[0];
                Faction f = (from x in GMData.ListNewPersonInfo
                             where x._person == p
                             select x._faction).FirstOrDefault();

                return UI.Format("EVENT_CS_EMPTY_OPTION1_DESC", p.ToString(), f.name);
            }
            void Selected(ref string nxtEvent, ref string param)
            {
                GMData.RelationManager.SetOffice(OUTTER.listPerson[0], OUTTER.emptyOffice);
            }

            EVENT_CS_EMPTY OUTTER;
        }
        class OPTION2 : Option
        {

            string Desc()
            {
                Person p = OUTTER.listPerson[1];
                Faction f = (from x in GMData.ListNewPersonInfo
                             where x._person == p
                             select x._faction).FirstOrDefault();

                return UI.Format("EVENT_CS_EMPTY_OPTION1_DESC", p.ToString(), f.name);
            }

            void Selected(ref string nxtEvent, ref string param)
            {
                GMData.RelationManager.SetOffice(OUTTER.listPerson[1], OUTTER.emptyOffice);
            }

            EVENT_CS_EMPTY OUTTER;
        }

        class OPTION3 : Option
        {
            string Desc()
            {
                Person p = OUTTER.listPerson[2];
                Faction f = (from x in GMData.ListNewPersonInfo
                             where x._person == p
                             select x._faction).FirstOrDefault();

                return UI.Format("EVENT_CS_EMPTY_OPTION1_DESC", p.ToString(), f.name);
            }

            void Selected(ref string nxtEvent, ref string param)
            {
                GMData.RelationManager.SetOffice(OUTTER.listPerson[2], OUTTER.emptyOffice);
            }

            EVENT_CS_EMPTY OUTTER;
        }

        private Office emptyOffice;
        private List<Person> listPerson = new List<Person>();
    }

    //class EVENT_CS_EMPTY : EVENT_HD
    //{
    //}
}