using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Tools;
using HuangDAPI;

[Serializable]
class StringSerialDictionary : SerialDictionary<string,string>{};

[Serializable]
class NameList : SerialList<string>{};

[Serializable]
class ListSerialDictionary : SerialDictionary<string, NameList>{};

partial class MyGame
{
    public class RelationManager
    {
        public RelationManager()
        {
            
        }

        public void Init()
        {
            foreach (var eProv in Enum.GetValues(typeof(Province.ENUM_PROV)))
            {
                FieldInfo field = eProv.GetType().GetField(eProv.ToString());
                Province.ProvinceAttribute attributeProv = Attribute.GetCustomAttribute(field, typeof(Province.ProvinceAttribute)) as Province.ProvinceAttribute;

                Province newProvince = new Province(eProv.ToString(), attributeProv.economy);
                listProvince2Status.Add(new HuangDAPI.GMData.ProvinceStatusElem { province = newProvince, debuff = null, });//debuffList = new List<HuangDAPI.Disaster>() });
            }

            foreach (var eOffice in Enum.GetValues(typeof(ENUM_OFFICE_CENTER)))
            {

                FieldInfo field = eOffice.GetType().GetField(eOffice.ToString());
                OfficeAttrAttribute attribute = Attribute.GetCustomAttribute(field, typeof(OfficeAttrAttribute)) as OfficeAttrAttribute;

                Office newOffice = new Office(eOffice.ToString(), attribute.Power);

                listOffice2Person.Add(new HuangDAPI.GMData.OfficeMapElem{office = newOffice, person = null});
            }

            foreach (var eOffice in Enum.GetValues(typeof(ENUM_OFFICE_LOCAL)))
            {
                FieldInfo field = eOffice.GetType().GetField(eOffice.ToString());
                OfficeAttrAttribute attribute = Attribute.GetCustomAttribute(field, typeof(OfficeAttrAttribute)) as OfficeAttrAttribute;

                foreach(var elem in listProvince2Status)
                {

                    Office newOffice = new Office(string.Format("{0}|{1}", elem.province.name.ToString(), eOffice.ToString()), attribute.Power);
                    listOffice2Person.Add(new HuangDAPI.GMData.OfficeMapElem { office = newOffice, person = null });

                    listProvince2Office.Add(new HuangDAPI.GMData.ProvinceMapElem { province = elem.province, office = newOffice, });//debuffList = new List<HuangDAPI.Disaster>() });
                }
            }



            List<HuangDAPI.Person> listPerson = new List<HuangDAPI.Person>();
            while (listPerson.Count < listOffice2Person.Count)
            {
                listPerson.Add(Person.NewPerson(true));
            }

            listPerson.nth_element(0, 3, (x, y) => -(x.score.CompareTo(y.score)));
            listPerson.nth_element(3, 3+9, (x, y) => -(x.score.CompareTo(y.score)));
            listPerson.nth_element(3+9, listPerson.Count, (x, y) => -(x.score.CompareTo(y.score)));

            listOffice2Person.Sort((x, y) => -(x.office.power.CompareTo(y.office.power)));
            for (int i = 0; i < listPerson.Count; i++)
            {
                listOffice2Person[i].person = listPerson[i];
            }

            foreach (var eOffice in Enum.GetValues(typeof(ENUM_OFFICE_FEMALE)))
            {

                FieldInfo field = eOffice.GetType().GetField(eOffice.ToString());
                OfficeAttrAttribute attribute = Attribute.GetCustomAttribute(field, typeof(OfficeAttrAttribute)) as OfficeAttrAttribute;

                Office newOffice = new Office(eOffice.ToString(), attribute.Power);

                listHougong2Person.Add(new HuangDAPI.GMData.OfficeMapElem { office = newOffice, person = Person.NewPerson(false) });
            }


            List<Faction> listFaction = new List<Faction>{
                new Faction(FactionManager.ENUM_FACTION.SHI.ToString()),
                new Faction(FactionManager.ENUM_FACTION.XUN.ToString()),
                new Faction(FactionManager.ENUM_FACTION.WAI.ToString()),
            };


            foreach (Person p in listPerson)
            {
                Faction f = null;

                int iRandom = Tools.Probability.GetRandomNum(0, 100);
                if (iRandom < 60)
                {
                    f = listFaction.Find(x => x.name == "SHI");
                }
                else if (iRandom < 90)
                {
                    f = listFaction.Find(x => x.name == "XUN");
                }
                else
                {
                    f = listFaction.Find(x => x.name == "WAI");
                }

                listFaction2Person.Add(new HuangDAPI.GMData.FactionMapElem { faction = f, person = p });
            }
        }

        public void SetOffice2Person(HuangDAPI.Office officeParam, HuangDAPI.Person ppersonParam)
        {
            var elem = listOffice2Person.Find(x => x.person == ppersonParam);
            if (elem != null)
            {
                elem.person = null;
            }

            elem = listOffice2Person.Find(x => x.office == officeParam);
            if(elem == null)
            {
                listOffice2Person.Add(new HuangDAPI.GMData.OfficeMapElem { office = officeParam, person = ppersonParam });
                return;
            }

            if(elem.person != null)
            {
                throw new ArgumentException(string.Format("office {0} already has person {1}", elem.office.name, elem.person.name));
            }

            elem.person = ppersonParam;
        }

        public void SetFaction2Person(HuangDAPI.Faction factionParam, HuangDAPI.Person personParam)
        {
            var elem = listFaction2Person.Find(x => x.person == personParam);
            if (elem == null)
            {
                listFaction2Person.Add(new HuangDAPI.GMData.FactionMapElem { faction = factionParam, person = personParam });
                return;
            }

            elem.faction = factionParam;
        }

        public void SetProvinceBuff(HuangDAPI.Province provincep, HuangDAPI.Disaster disaster)
        {
            var elem = listProvince2Status.Find(x => x.province == provincep);
            elem.debuff = disaster;
        }

        public void RemoveProvinceBuff(HuangDAPI.Province province, HuangDAPI.Disaster disaster)
        {
            var elem = listProvince2Status.Find(x => x.province == province && x.debuff == disaster);
            elem.debuff = null;
        }

        public HuangDAPI.Person[] Persons
        {
            get
            {
                List<HuangDAPI.Person> listPerson1 = (from x in listOffice2Person
                                                    select x.person).ToList();

                List<HuangDAPI.Person> listPerson2 = (from x in listFaction2Person
                                                     select x.person).ToList();

                if( listPerson1.Except(listPerson2).Count() != 0)
                {
                    throw new SystemException("person in FactionMap and OfficeMap not same!");
                }

                return listPerson1.ToArray();
            }
        }

        public HuangDAPI.Faction[] Factions
        {
            get
            {
                return (from x in listFaction2Person
                        select x.faction).ToArray();
            }

        }

        public HuangDAPI.Office[] Offices
        {
            get
            {
                return (from x in listOffice2Person
                        select x.office).ToArray();
            }
        }

        public HuangDAPI.Office[] Hougongs
        {
            get
            {
                return (from x in listHougong2Person
                        select x.office).ToArray();
            }
        }

        public List<HuangDAPI.GMData.OfficeMapElem> GetOfficeMap()
        {
            return listOffice2Person;
        }

        public List<HuangDAPI.GMData.FactionMapElem> GetFactionMap()
        {
            return listFaction2Person;
        }

        public List<HuangDAPI.GMData.RelationMapElem>  GetRelationMap()
        {
            var q = from a in listOffice2Person
                    join b in listFaction2Person on a.person equals b.person
                    select new HuangDAPI.GMData.RelationMapElem() { office = a.office, person = a.person, faction = (a.person != null ? b.faction : null) };
            var e = from x in listOffice2Person
                    where x.person == null
                    select new HuangDAPI.GMData.RelationMapElem() { office = x.office, person = null, faction = null };

            List<HuangDAPI.GMData.RelationMapElem> result = q.ToList();
            result.AddRange(e.ToList());

            return result;
        }

        public List<HuangDAPI.GMData.ProvinceMapElem> GetProvinceMap()
        {
            return listProvince2Office;
        }

        public List<HuangDAPI.GMData.OfficeMapElem> GetHougongMap()
        {
            return listHougong2Person;
        }

        public List<GMData.ProvinceStatusElem> GetProvinceStatusMap()
        {
            return listProvince2Status;
        }

        public void Listen(object obj, string cmd)
        {
            switch (cmd)
            {
                case "DIE":
                    {
                        Person p = (Person)obj;
                        listOffice2Person.Find(x => x.person == p).person = null;
                        listFaction2Person.RemoveAll(x => x.person == p);
                    }
                    break;
                default:
                    break;
            }
        }

        List<HuangDAPI.GMData.OfficeMapElem> listOffice2Person = new List<HuangDAPI.GMData.OfficeMapElem>();
        List<HuangDAPI.GMData.OfficeMapElem> listHougong2Person = new List<HuangDAPI.GMData.OfficeMapElem>();
        List<HuangDAPI.GMData.FactionMapElem> listFaction2Person = new List<HuangDAPI.GMData.FactionMapElem>();
        List<HuangDAPI.GMData.ProvinceMapElem> listProvince2Office = new List<HuangDAPI.GMData.ProvinceMapElem>();
        List<HuangDAPI.GMData.ProvinceStatusElem> listProvince2Status = new List<HuangDAPI.GMData.ProvinceStatusElem>();


    }

	//[NonSerialized]
	//public RelationOffice2Person relOffice2Person = new RelationOffice2Person();

	//[NonSerialized]
	//public RelationFaction2Person relFaction2Person = new RelationFaction2Person();

    [NonSerialized]
    public RelationZhouj2Office relZhouj2Office = new RelationZhouj2Office();

	//[SerializeField]
	//private StringSerialDictionary DictOffce2Person = new StringSerialDictionary ();

	//[SerializeField]
	//private  ListSerialDictionary DictFaction2Person = new ListSerialDictionary ();

    [SerializeField]
    private  ListSerialDictionary DictZhouj2Office = new ListSerialDictionary ();

	//public class RelationOffice2Person
	//{
	//	public RelationOffice2Person()
	//	{
	//	}

	//	public void Set(Person p, Office o)
	//	{
 //           string oldoffice = "";
 //           foreach (KeyValuePair<string, string> of in Inst.DictOffce2Person) 
 //           {
 //               if (of.Value == p.name) 
 //               {
 //                   oldoffice = of.Key;
 //               }
 //           }

 //           if (oldoffice != "")
 //           {
 //               Inst.DictOffce2Person[oldoffice] = "";
 //           }

	//		Inst.DictOffce2Person[o.name]=p.name;
	//	}

	//	public Person GetPerson(Office o)
	//	{
	//		return GetPerson (o.name);
	//	}

	//	public Person GetPerson(string office)
	//	{
	//		string personName = Inst.DictOffce2Person [office];
	//		Person p = Inst.personManager.GetByName (personName);
	//		if (p == null) 
	//		{
	//			p = Inst.femalePersonManager.GetByName (personName);
	//		}

	//		return p;
	//	}

	//	public List<Person> GetPerson(Office[] offices)
	//	{
	//		List<Person> listResult = new List<Person> ();
	//		foreach (Office o in offices) 
	//		{
	//			Person p = GetPerson (o);
	//			if (p == null) 
	//			{
	//				continue;
	//			}

	//			listResult.Add (p);
	//		}

 //           return listResult;
	//	}

	//	public List<Person> GetPersonBySelector(SelectElem Selector, List<Person> ListPerson)
	//	{
 //           Office[] Offices = null;
 //           if (ListPerson == null)
 //           {
 //               Offices = Inst.officeManager.GetByName(Inst.DictOffce2Person.Keys.ToArray());
 //           }
 //           else
 //           {
 //               Offices = GetOffice(ListPerson.ToArray()).ToArray();
 //           }

 //           List<Office> SelectOffices = Offices.Where(Selector.Complie<Office>()).ToList();

 //           return GetPerson(SelectOffices.ToArray());
	//	}

 //       public List<Office> GetOfficeBySelector(SelectElem Selector, List<Office> ListOffice)
 //       {
 //           Person[] Persons = null;
 //           if (ListOffice == null)
 //           {
 //               Persons = Inst.personManager.GetByName(Inst.DictOffce2Person.Values.ToArray());
 //           }
 //           else
 //           {
 //               Persons = GetPerson(ListOffice.ToArray()).ToArray();
 //           }

 //           List<Person> SelectPerson = Persons.Where(Selector.Complie<Person>()).ToList();

 //           List<Office> ret = GetOffice(SelectPerson.ToArray());

 //           if (Selector.needNull)
 //           {
 //               string officeName = GetOffice("");

 //               if(ListOffice != null && ListOffice.Find(x=>x.name == officeName) == null)
 //               {
 //                   return ret;
 //               }


 //               Office  office = Inst.officeManager.GetByName (officeName);
 //               if (office != null)
 //               {
 //                   ret.Add(office);
 //               }
 //           }

 //           return ret;
 //       }

	//	public Office GetOffice(Person p)
 //       {
 //           string officeName = GetOffice(p.name);
	//		if (officeName == "") 
	//		{
	//			return null;
	//		}

	//		Office  office = Inst.officeManager.GetByName (officeName);
	//		return office;
	//	}

 //       public string GetOffice(string p)
 //       {
 //           foreach (KeyValuePair<string, string> kvp in Inst.DictOffce2Person)
 //           {
 //               if (kvp.Value.Equals(p))
 //               { 
 //                   return kvp.Key;
 //               }
 //           }

 //           return "";
 //       }

 //       public List<Office> GetOffice(Person[] Persons)
 //       {
 //           List<Office> listResult = new List<Office>();
 //           foreach(Person p in Persons)
 //           {
 //               Office o = GetOffice(p);
 //               if(o != null)
 //               {
 //                   listResult.Add(o);
 //               }
 //           }

 //           return listResult;

 //       }

 //       public void Listen(object obj, string cmd)
	//	{
	//		switch (cmd) 
	//		{
	//		case "DIE":
	//			{
	//				Office o = GetOffice ((Person) obj);
	//				if (o == null)
	//				{
	//					break;
	//				}
	//				Inst.DictOffce2Person [o.name] = "";
	//			}
	//			break;
	//		default:
	//			break;
	//		}
	//	}
	//}

	//public class RelationFaction2Person
	//{
	//	public void Set(Faction f, Person p)
	//	{
	//		if (!Inst.DictFaction2Person.ContainsKey (f.name))
	//		{
	//			Inst.DictFaction2Person.Add (f.name, new NameList());
	//		}

	//		Inst.DictFaction2Person [f.name].ToList().Add (p.name);
	//	}

	//	public Person[] GetPerson(Faction f)
	//	{
	//		return GetPerson (f.name);
	//	}

	//	public Person[] GetPerson(string f)
	//	{
	//		string[] personNames = Inst.DictFaction2Person [f].ToList().ToArray();

	//		return Inst.personManager.GetByName (personNames).ToArray();
	//	}

	//	public Person[] GetPerson(Faction[] factions)
	//	{
	//		List<Person> listResult = new List<Person> ();
	//		foreach (Faction o in factions) 
	//		{
	//			Person[] p = GetPerson (o);

	//			listResult.AddRange (p);
	//		}

	//		return listResult.ToArray();
	//	}

	//	public Faction GetFaction(Person p)
	//	{
	//		return GetFaction(p.name);
	//	}

	//	public Faction GetFaction(string pname)
	//	{
	//		foreach (KeyValuePair<string, NameList> kvp in Inst.DictFaction2Person)
	//		{
	//			if (kvp.Value.ToList().Contains(pname))
	//			{ 
	//				return Inst.factionManager.GetByName (kvp.Key);
	//			}
	//		}

	//		return null;
	//	}

	//	public Faction[] GetFaction(Person[] Persons)
	//	{
	//		List<string> listFactionName = new List<string> ();
			
	//		foreach (KeyValuePair<string, NameList> kvp in Inst.DictFaction2Person)
	//		{
	//			foreach (Person p in Persons)
	//			{
	//				if (kvp.Value.ToList().Contains(p.name))
	//				{ 
	//					if (!listFactionName.Contains (kvp.Key)) 
	//					{
	//						listFactionName.Add (kvp.Key);
	//					}
	//				}
	//			}
	//		}

 //           return Inst.factionManager.GetByName(listFactionName.ToArray());
	//	}

 //       public List<Person> GetPersonBySelector(SelectElem Selector, List<Person> ListPerson)
 //       {
 //           if (ListPerson == null)
 //           {
 //               Faction[] Factions = Inst.factionManager.GetByName(Inst.DictFaction2Person.Keys.ToArray());

 //               Faction[] SelectFactions = Factions.Where(Selector.Complie<Faction>()).ToList().ToArray();
 //               return GetPerson(SelectFactions).ToList();
 //           }
 //           else
 //           {
 //               ListPerson.RemoveAll(delegate(Person p){
 //                   return !Selector.Complie<Faction>().Invoke(GetFaction(p));
 //                   });

 //               return ListPerson;
 //           }
 //       }

 //       public List<Faction> GetFactionBySelector(SelectElem Selector, List<Faction> ListFaction)
 //       {
 //           if (ListFaction == null)
 //           {
 //               List<string> listPerson = new List<string>();
 //               foreach(NameList list in Inst.DictFaction2Person.Values)
 //               {
 //                   listPerson.AddRange(list.ToList());
 //               }

 //               Person[] Persons = Inst.personManager.GetByName(listPerson.ToArray()).ToArray();

 //               Person[] SelectPersons = Persons.Where(Selector.Complie<Person>()).ToList().ToArray();

 //               return GetFaction(SelectPersons).ToList();
 //           }
 //           else
 //           {
 //               for (int i = 0; i < ListFaction.Count; i++)
 //               {
 //                   Person[] Persons = GetPerson(ListFaction[i]);

 //                   foreach(Person p in Persons)
 //                   {
 //                      if(!Selector.Complie<Person>().Invoke(p))
 //                       {
 //                           ListFaction.RemoveAt(i);
 //                           break;
 //                       }
 //                   }
 //               }

 //               return ListFaction;
 //           }
 //       }

 //       public void Listen(object obj, string cmd)
	//	{
	//		switch (cmd) 
	//		{
	//		case "DIE":
	//			{
	//				Person p = (Person)obj;
	//				Faction o = GetFaction (p);
	//				if (o == null)
	//				{
	//					break;
	//				}
	//				Inst.DictFaction2Person [o.name].ToList ().Remove (p.name);
	//			}
	//			break;
	//		default:
	//			break;
	//		}
	//	}
	//}

    public class RelationZhouj2Office
    {
        public void Set(Province z, Office o)
        {
            if (!Inst.DictZhouj2Office.ContainsKey(z.name))
            {
                Inst.DictZhouj2Office.Add(z.name, new NameList());
            }

            Inst.DictZhouj2Office[z.name].ToList().Add (o.name);
        }

        public List<Office> GetOffices(string zname)
        {
            List<Office> lstResult = new List<Office>();
            List<string> lstName = Inst.DictZhouj2Office[zname].ToList();
            foreach(string name in lstName)
            {
                lstResult.Add(Inst.officeManager.GetByName(name));
            }

            return lstResult;
        }

        public List<Office> GetOffices(Province z)
        {
            return GetOffices(z.name);
        }

    }
}