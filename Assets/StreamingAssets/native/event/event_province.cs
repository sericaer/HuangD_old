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
            emptyOffice = (from x in GMData.RelationManager.OfficeMap
                           where x.office.name.Contains("CS")
                           where x.person == null
                           select x.office).FirstOrDefault();

            

            if (emptyOffice == null)
            {
                return false;
            }

            Debug.Log(emptyOffice.name);

            Faction factionSG1 = (from x in GMData.RelationManager.RelationMap
                              where x.office.name == "SG1"
                              select x.faction).FirstOrDefault();
            if (factionSG1 == null)
            {
                return false;
            }

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

    class EVENT_PROV_DISASTER_START : EVENT_HD
    {
        bool Precondition()
        {
            Province[] provinces = (from x in GMData.RelationManager.ProvinceMap
                                    select x.province).ToArray();
            foreach (Province p in provinces)
            {
                disaster = CalcDisater(p);
                if(disaster != null)
                {
                    province = p;
                    return true;
                }
            }
            return false;
        }

        string Desc()
        {
            return UI.Format("EVENT_PROV_DISASTER_START_DESC", province.name, disaster.name);
        }

        class OPTION1 : Option
        {
            void Selected(ref string nxtEvent, ref string param)
            {
                GMData.RelationManager.SetProvinceBuff(OUTTER.province, OUTTER.disaster);
            }

            EVENT_PROV_DISASTER_START OUTTER;
        }

        private Disaster CalcDisater(Province province)
        {
            var debuff = (from x in GMData.RelationManager.ProvinceStatusMap
                where x.province == province
                select x.debuff).FirstOrDefault();

            if (debuff != null)
            {
                return null;
            }

            if (Probability.IsProbOccur(1.0))
            {
                return GMData.NewDisaster();
            }

            return null;
        }

        private Disaster disaster;
        private Province province;
    }

    class EVENT_PROV_DISASTER_RECOVER : EVENT_HD
    {
        bool Precondition()
        {
            var query = from x in GMData.RelationManager.ProvinceMap
                                    select x.province;
            foreach (var v in query)
            {
                disaster = CalcDisaterRecover(v);
                if (disaster != null)
                {
                    province = v;
                    return true;
                }
            }
            return false;
        }

        string Desc()
        {
            var q = (from a in GMData.RelationManager.ProvinceMap
                    join b in GMData.RelationManager.OfficeMap on a.office equals b.office
                    select new { b.office, b.person}).FirstOrDefault();

            return UI.Format("EVENT_PROV_DISASTER_RECOVER_DESC", province.name, disaster.name, q.office.name, q.person.name);
        }

        class OPTION1 : Option
        {
            void Selected(ref string nxtEvent, ref string param)
            {
                OUTTER.disaster.recover = true;
            }

            EVENT_PROV_DISASTER_RECOVER OUTTER;
        }

        private Disaster CalcDisaterRecover(Province province)
        {
            var debuffList = (from x in GMData.RelationManager.ProvinceStatusMap
                              where x.province == province
                              select x.debuff);

            foreach(var disa in debuffList)
            {
                if(disa.recover)
                {
                    continue;
                }

                if (Probability.IsProbOccur(0.08))
                {
                    return disa;
                }
            }

            return null;
        }

        private Disaster disaster;
        private Province province;
    }

    class EVENT_PROV_DISASTER_END : EVENT_HD
    {
        bool Precondition()
        {
            var query = from x in GMData.RelationManager.ProvinceMap
                        select x.province;
            foreach (var v in query)
            {
                disaster = CalcDisaterEnd(v);
                if (disaster != null)
                {
                    province = v;
                    return true;
                }
            }
            return false;
        }

        string Desc()
        {
            var q = (from a in GMData.RelationManager.ProvinceMap
                     join b in GMData.RelationManager.OfficeMap on a.office equals b.office
                     select new { b.office, b.person }).FirstOrDefault();

            return UI.Format("EVENT_PROV_DISASTER_END_DESC", province.name, disaster.name, q.office.name, q.person.name);
        }

        class OPTION1 : Option
        {
            void Selected(ref string nxtEvent, ref string param)
            {
                GMData.RelationManager.RemoveProvinceBuff(OUTTER.province, OUTTER.disaster);
            }

            EVENT_PROV_DISASTER_END OUTTER;
        }

        private Disaster CalcDisaterEnd(Province province)
        {
            var debuffList = from x in GMData.RelationManager.ProvinceStatusMap
                              where x.province == province
                              select x.debuff;


            foreach (var disa in debuffList)
            {
                if(!disa.recover)
                {
                    continue;
                }

                float prob = 0.0005f;
                if(disa.saved == "MAX")
                {
                    prob += 0.001f;
                }
                else if (disa.saved == "MID")
                {
                    prob += 0.0005f;
                }
                else if (disa.saved == "MIN")
                {
                    prob += 0.0003f;
                }

                if (Probability.IsProbOccur(prob))
                {
                    return disa;
                }
            }

            return null;
        }

        private Disaster disaster;
        private Province province;
    }
}