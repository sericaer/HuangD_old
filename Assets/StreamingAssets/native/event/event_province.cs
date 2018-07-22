////using HuangDAPI;
////using System.Collections;
////using System.Collections.Generic;
////using System;
////using System.Linq;
////using UnityEngine;

////namespace native
////{
////    class EVENT_CS_EMPTY : EVENT_HD
////    {
////        bool Precondition()
////        {
////            emptyOffice = (from x in GMData.Offices.CS
////                           where x.person == null
////                           select x).FirstOrDefault();

////            if (emptyOffice == null)
////            {
////                return false;
////            }

////            if(GMData.Offices.SG[1].person == null)
////            {
////                return false;
////            }

////            Faction factionSG1 = GMData.Offices.SG[1].person.faction;

////            listPerson.Add(GMData.NewMalePerson(factionSG1));
////            listPerson.Add(GMData.NewMalePerson(factionSG1));

////            Faction faction3rd;
////            if(Probability.IsProbOccur(0.8))
////            {
////                faction3rd = factionSG1;
////            }
////            else
////            {
////                faction3rd = GMData.Factions.Random();
////            }

////            listPerson.Add(GMData.NewMalePerson(faction3rd));

////            return true;
////        }

////        string Desc()
////        {
////            return UI.Format("EVENT_CS_EMPTY_DESC", emptyOffice.name);
////        }

////        class OPTION1 : Option
////        {
////            string Desc()
////            {
////                Person p = OUTTER.listPerson[0];
////                return UI.Format("EVENT_CS_EMPTY_OPTION1_DESC", p.ToString(), p.faction.name);
////            }
////            void Selected(ref string nxtEvent, ref object param)
////            {
////                GMData.RelationManager.SetOffice(OUTTER.listPerson[0], OUTTER.emptyOffice);
////            }

////            EVENT_CS_EMPTY OUTTER;
////        }
////        class OPTION2 : Option
////        {

////            string Desc()
////            {
////                Person p = OUTTER.listPerson[1];
////                return UI.Format("EVENT_CS_EMPTY_OPTION1_DESC", p.ToString(), p.faction.name);
////            }

////            void Selected(ref string nxtEvent, ref object param)
////            {
////                GMData.RelationManager.SetOffice(OUTTER.listPerson[1], OUTTER.emptyOffice);
////            }

////            EVENT_CS_EMPTY OUTTER;
////        }

////        class OPTION3 : Option
////        {
////            string Desc()
////            {
////                Person p = OUTTER.listPerson[2];
////                return UI.Format("EVENT_CS_EMPTY_OPTION1_DESC", p.ToString(), p.faction.name);
////            }

////            void Selected(ref string nxtEvent, ref object param)
////            {
////                GMData.RelationManager.SetOffice(OUTTER.listPerson[2], OUTTER.emptyOffice);
////            }

////            EVENT_CS_EMPTY OUTTER;
////        }

////        private Office emptyOffice;
////        private List<Person> listPerson = new List<Person>();
////    }

////    class EVENT_PROV_DISASTER_START : EVENT_HD
////    {
////        bool Precondition()
////        {
////            foreach (Province p in GMData.Provinces.All)
////            {
////                disaster = CalcDisater(p);
////                if(disaster != null)
////                {
////                    province = p;
////                    return true;
////                }
////            }
////            return false;
////        }

////        string Desc()
////        {
////            return UI.Format("EVENT_PROV_DISASTER_START_DESC", province.name, disaster.name);
////        }

////        class OPTION1 : Option
////        {
////            void Selected(ref string nxtEvent, ref object param)
////            {
////                GMData.RelationManager.SetProvinceBuff(OUTTER.province, OUTTER.disaster);
////            }

////            EVENT_PROV_DISASTER_START OUTTER;
////        }

////        private Disaster CalcDisater(Province province)
////        {
////            if (province.debuff != null)
////            {
////                return null;
////            }

////            if (Probability.IsProbOccur(0.0))
////            {
////                return GMData.NewDisaster();
////            }

////            return null;
////        }

////        private Disaster disaster;
////        private Province province;
////    }

////    class EVENT_PROV_DISASTER_RECOVER : EVENT_HD
////    {
////        bool Precondition()
////        {
////            foreach (var v in GMData.Provinces.All)
////            {
////                disaster = CalcDisaterRecover(v);
////                if (disaster != null)
////                {
////                    province = v;
////                    return true;
////                }
////            }
////            return false;
////        }

////        string Desc()
////        {
////            var office = province.mainOffice;
////            return UI.Format("EVENT_PROV_DISASTER_RECOVER_DESC", province.name, disaster.name, office.name, office.person.name);
////        }

////        class OPTION1 : Option
////        {
////            void Selected(ref string nxtEvent, ref object param)
////            {
////                OUTTER.disaster.recover = true;
////            }

////            EVENT_PROV_DISASTER_RECOVER OUTTER;
////        }

////        private Disaster CalcDisaterRecover(Province province)
////        {
////            if(province.debuff == null || province.debuff.recover)
////            {
////                return null;
////            }

////            if (Probability.IsProbOccur(0.08))
////            {
////               return province.debuff;
////            }

////            return null;
////        }

////        private Disaster disaster;
////        private Province province;
////    }

////    class EVENT_PROV_DISASTER_END : EVENT_HD
////    {
////        bool Precondition()
////        {
////            foreach (var v in GMData.Provinces.All)
////            {
////                disaster = CalcDisaterEnd(v);
////                if (disaster != null)
////                {
////                    province = v;
////                    return true;
////                }
////            }
////            return false;
////        }

////        string Desc()
////        {
////            var office = province.mainOffice;
////            return UI.Format("EVENT_PROV_DISASTER_END_DESC", province.name, disaster.name, office.name, office.person.name);
////        }

////        class OPTION1 : Option
////        {
////            void Selected(ref string nxtEvent, ref object param)
////            {
////                GMData.RelationManager.RemoveProvinceBuff(OUTTER.province, OUTTER.disaster);
////            }

////            EVENT_PROV_DISASTER_END OUTTER;
////        }

////        private Disaster CalcDisaterEnd(Province province)
////        {
////            if (province.debuff == null || !province.debuff.recover)
////            {
////                return null;
////            }

////            float prob = 0.0005f;
////            if (province.debuff.saved == "MAX")
////            {
////                prob += 0.001f;
////            }
////            else if (province.debuff.saved == "MID")
////            {
////                prob += 0.0005f;
////            }
////            else if (province.debuff.saved == "MIN")
////            {
////                prob += 0.0003f;
////            }

////            if (Probability.IsProbOccur(prob))
////            {
////                return province.debuff;
////            }

////            return null;
////        }

////        private Disaster disaster;
////        private Province province;
////    }

////    class EVENT_PROV_YEAR_INCOME : EVENT_HD
////    {
////        bool Precondition()
////        {
////            if(GMData.Date.month == 10 && GMData.Date.day == 2)
////            {
////                incomeMap = new Dictionary<Province, int>();
////                foreach (var prov in GMData.Provinces.All)
////                {
////                    incomeMap.Add(prov, CalcIncome(prov));
////                }

////                return true;
////            }

////            return false;
////        }

////        string Desc()
////        {
////            string strDesc = UI.Format("EVENT_PROV_YEAR_INCOME_DESC_TOTAL", incomeMap.Values.Sum().ToString()) + "\n";

////            return strDesc;
////        }

////        class OPTION1 : Option
////        {
////            void Selected(ref string nxtEvent, ref object param)
////            {
////                GMData.Economy += OUTTER.incomeMap.Values.Sum();

////                nxtEvent = "EVENT_PROV_YEAR_INCOME_DETAL";

////                List<List<object>> lists = new List<List<object>>();
////                lists.Add(new List<object> { "PROVINCE_NAME", "INCOME", "CS", "FACTION", "SCORE"});

////                foreach (var elem in OUTTER.incomeMap)
////                {
////                    var office = elem.Key.mainOffice ;
////                    office.person.score += 3;

////                    lists.Add(new List<object> { elem.Key, elem.Value, office.person, office.person.faction, 3 });
////                }
////                param = lists;
////            }

////            EVENT_PROV_YEAR_INCOME OUTTER;
////        }

////        private int CalcIncome(Province province)
////        {

////                int incomeBase = 10;
////                if(province.economy == "HIGH")
////                {
////                    incomeBase += 10;
////                }
////                else if (province.economy == "MID")
////                {

////                }
////                else if (province.economy == "LOW")
////                {
////                    incomeBase = incomeBase - 5;
////                }

////                if(province.debuff == null)
////                {
////                    return incomeBase;
////                }

////                if(province.debuff.recover)
////                {
////                    return (int)(incomeBase * 0.3);
////                }
////                else
////                {
////                    return 0;
////                }

////        }

////        private Dictionary<Province, int> incomeMap = null;
////    }

////    class EVENT_PROV_YEAR_INCOME_DETAL : EVENT_HD
////    {
////        void Initialize(object param)
////        {
////            lists = param as List<List<object>>;
////        }

////        List<List<object>> Desc()
////        {
////            return lists;
////        }

////        class OPTION1 : Option
////        {
////            void Selected(ref string nxtEvent, ref object param)
////            {

////            }

////        }
////        List<List<object>> lists = null;
////    }
////}