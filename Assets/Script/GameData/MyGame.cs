using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using UnityEngine;

using Tools;
using System.Linq;

[Serializable]
public class StatusList : SerialList<HuangDAPI.Status> { };

[Serializable]
public partial class MyGame
{
	[NonSerialized]
    public static MyGame Inst = new MyGame();

	public void Initialize(string strEmpName, string strYearName, string strDynastyName)
    {
        gameEnd = false;

        historyRecord = "";

        empName  = strEmpName;
        empAge   = Probability.GetRandomNum (16, 40);
		empHeath = Probability.GetRandomNum (5, 8);

        Stability = Probability.GetRandomNum(60, 90);
        Economy   = Probability.GetRandomNum(60, 90);
        Military  = Probability.GetRandomNum(60, 90);

        yearName = strYearName;
		dynastyName = strDynastyName;
        date = new GameTime();

        provManager = new ProvinceManager();

        relationManager = new RelationManager();
        relationManager.Init();

        officeManager = new OfficeManager ();
		//femaleOfficeManager = new OfficeManager (typeof(ENUM_OFFICE_FEMALE));

		factionManager = new FactionManager ();

        personManager = new PersonManager (officeManager.CountCenter + officeManager.CountLocal, true);
		personManager.Sort ((p1,p2)=> -(p1.score.CompareTo(p2.score)));

        femalePersonManager = new PersonManager (officeManager.CountFemale, false);
        statusManager = new StatusManager();

        DictFlag = new StringSerialDictionary();

		//Person.ListListener.Add (relOffice2Person.Listen);
		//Person.ListListener.Add (relFaction2Person.Listen);
		Person.ListListener.Add (personManager.Listen);
		Person.ListListener.Add (femalePersonManager.Listen);
        Person.ListListener.Add (statusManager.Listen);
        Person.ListListener.Add (relationManager.Listen);

        //InitRelationOffice2Person ();
		//InitRelationFaction2Person ();

		//InitRelationFemaleOffice2Person ();

        InitZhouj2Office();
    }

    //public List<HuangDAPI.GMData.RelationMapElem> RelationMap
    //{
    //    get
    //    {
    //        return relationManager.GetRelationMap();
    //    }
    //}

 //   public Person[] GetPerson(BySelector selecor)
	//{
 //       if(selecor == null)
 //       {
 //           return personManager.GetRange(0, personManager.Count);
 //       }

	//	if (selecor.empty)
	//	{
 //           throw new ArgumentException("seletor is empty!");
	//	}

 //       Debug.Log(String.Format("GetPerson {0}", selecor.ToString()));

 //       List<Person> lstResult = null;
 //       if (!selecor.persons.empty)
 //       {
 //           lstResult = personManager.GetPersonBySelector(selecor.persons);
 //       }
 //       if (!selecor.offices.empty)
 //       {
 //           lstResult = relOffice2Person.GetPersonBySelector(selecor.offices, lstResult);
 //       }
 //       if (!selecor.factions.empty)
 //       {
 //           lstResult = relFaction2Person.GetPersonBySelector(selecor.factions, lstResult);
 //       }

 //       if (lstResult == null)
 //       {
 //           lstResult = new List<Person>();
 //       }

 //       var listDebug = from o in lstResult select o.name;
 //       Debug.Log(String.Format("GetPerson result:{0}", string.Join(",", listDebug.ToArray())));

 //       return lstResult.ToArray ();
	//}

	//public Faction[] GetFaction(BySelector selecor)
	//{
   //     if (selecor == null)
   //     {
   //         return factionManager.GetRange(0, factionManager.Count);
   //     }

   //     if (selecor.empty)
   //     {
   //         throw new ArgumentException("seletor is empty!");
   //     }

   //     Debug.Log(String.Format("GetFaction {0}", selecor.ToString()));

   //     List<Faction> lstResult = null;
   //     if (!selecor.factions.empty)
   //     {
   //         lstResult = factionManager.factionBySelector(selecor.factions);
   //     }
   //     if (!selecor.persons.empty)
   //     {
   //         lstResult = relFaction2Person.GetFactionBySelector(selecor.persons, lstResult);
   //     }
   //     if (!selecor.offices.empty)
   //     {
   //         List<Person>  listPerson = relOffice2Person.GetPersonBySelector(selecor.offices, null);

   //         List<string> listPersonName = new List<string>();
   //         foreach(Person p in listPerson)
   //         {
   //             listPersonName.Add(p.name);
   //         }

			//BySelector selectbyPerson = (BySelector)(new BySelector().ByPerson(listPersonName.ToArray()));

   //         lstResult = relFaction2Person.GetFactionBySelector(selectbyPerson.persons, lstResult);
   //     }

   //     if (lstResult == null)
   //     {
   //         lstResult = new List<Faction>();
   //     }

   //     var listDebug = from o in lstResult select o.name;
   //     Debug.Log(String.Format("GetFaction result:{0}", string.Join(",", listDebug.ToArray())));

   //     return lstResult.ToArray();
   // }

   // public Office[] GetOffice(BySelector selecor)
   // {
   //     if (selecor.empty)
   //     {
   //         throw new ArgumentException ("seletor is empty!");
   //     }

   //     Debug.Log(String.Format("GetOffice {0}", selecor.ToString()));

   //     List<Office> lstResult = null;
   //     if (!selecor.offices.empty)
   //     {
   //         lstResult = officeManager.GetOfficeBySelector(selecor.offices);
   //     }
   //     if (!selecor.persons.empty)
   //     {
   //         lstResult = relOffice2Person.GetOfficeBySelector(selecor.persons, lstResult);
   //     }
   //     if (!selecor.factions.empty)
   //     {
   //         List<Person>  listPerson = relFaction2Person.GetPersonBySelector(selecor.factions, null);

   //         List<string> listPersonName = new List<string>();
   //         foreach(Person p in listPerson)
   //         {
   //             listPersonName.Add(p.name);
   //         }

			//BySelector selectbyPerson = (BySelector)(new BySelector().ByPerson(listPersonName.ToArray()));

    //        lstResult = relOffice2Person.GetOfficeBySelector(selectbyPerson.persons, lstResult);
    //    }

    //    if (lstResult == null)
    //    {
    //        lstResult = new List<Office>();
    //    }

    //    var listDebug = from o in lstResult select o.name;
    //    Debug.Log(String.Format("GetOffice result:{0}", string.Join(",", listDebug.ToArray())));

    //    return lstResult.ToArray ();
    //}

    //public Province[] GetProvince(BySelector selecor)
    //{
    //    List<Province> lstResult = null;

    //    if (selecor == null)
    //    {
    //        return provManager.provinces;
    //    }

    //    if (selecor.empty)
    //    {
    //        throw new ArgumentException ("seletor is empty!");
    //    }

    //    Debug.Log(String.Format("GetProvince {0}", selecor.ToString()));


    //    //if (!selecor.provbuffs.empty)
    //    //{
    //    //    lstResult = provManager.GetProvinceBySelector(selecor.provbuffs);
    //    //}

    //    return lstResult.ToArray();
    //}

//    public Province[] GetProvince(BySelector selecor)
//    {
//    }

    public void   SetFlag(string key, string value)
    {
        DictFlag[key] = value;
    }

    public string GetFlag(string key)
    {
        if (!DictFlag.ContainsKey(key))
        {
            return null;
        }

        return DictFlag[key];
    }

    public void ClearFlag(string key)
    {
        DictFlag.Remove(key);
    }

    //public void Appoint(string person, string office)
    //{
    //    Person p = personManager.GetByName(person);
    //    if (p == null)
    //    {
    //        throw new ArgumentException("can not find person by name:" + person);
    //    }

    //    Office o = officeManager.GetByName(office);
    //    if (o == null)
    //    {
    //        throw new ArgumentException("can not find office by name:" + office);
    //    }

    //    relOffice2Person.Set(p, o);

    //}

    public void PreCreatePerson2(string faction, int score)
    {
    }


    public class PER_CREATE_PERSON
    {
        public string factionName;
        public string personName;
        public int  score;
    }

    public PER_CREATE_PERSON PreCreatePerson(string faction, int score)
    {
        Debug.Log(faction + score.ToString());

        PER_CREATE_PERSON pc = new PER_CREATE_PERSON();
        pc.factionName = faction;
        pc.score = score;
        pc.personName = StreamManager.personName.GetRandomMale();

        return  pc;
    }

    public Person CreatePerson(PER_CREATE_PERSON p, string officename)
    {
        Person person = new Person(p);
        personManager.Add(person);

        //relFaction2Person.Set(factionManager.GetByName(p.factionName), person);
        relationManager.SetFaction2Person(factionManager.GetByName(p.factionName), person);
        relationManager.SetOffice2Person(officeManager.GetByName(officename), person);

        return person;
    }

    public Person NewPerson(bool v)
    {
        return Person.NewPerson(v);
    }

    public Disaster NewDisaster()
    {
        return Disaster.NewDisaster();
    }

    private MyGame()
    {
    }


	private void InitRelationOffice2Person ()
	{
		int iCount = officeManager.CountCenter;
		Person[] persons = personManager.GetRange (0, iCount);
        Office[] offices = officeManager.GetRange (typeof(ENUM_OFFICE_CENTER));

		for (int i = 0; i < iCount; i++) 
		{
            relationManager.SetOffice2Person(offices[i], persons[i]);
			//relOffice2Person.Set(persons[i], offices[i]);
		}

        int iLocalCount = officeManager.CountLocal;
        persons = personManager.GetRange (iCount, iCount+iLocalCount);
        offices = officeManager.GetRange (typeof(ENUM_OFFICE_LOCAL));

        for (int i = 0; i < iLocalCount; i++) 
        {
            relationManager.SetOffice2Person(offices[i], persons[i]);
            //relOffice2Person.Set(persons[i], offices[i]);
        }
	}

	private void InitRelationFaction2Person ()
	{
		Person[] persons = personManager.GetRange (0, personManager.Count);

		for (int i = 0; i < persons.Length; i++) 
		{
            Faction faction = null;

			int iRandom = Probability.GetRandomNum (0, 100);
			if(iRandom < 60)
			{
                faction = factionManager.GetByName(FactionManager.ENUM_FACTION.SHI.ToString());
				
			}
			else if(iRandom < 90)
			{
                faction = factionManager.GetByName(FactionManager.ENUM_FACTION.XUN.ToString());
			}
			else
			{
                faction = factionManager.GetByName(FactionManager.ENUM_FACTION.WAI.ToString());
			}

            //relFaction2Person.Set(faction, persons[i]);
            relationManager.SetFaction2Person(faction, persons[i]);
		}
	}

    public void HistoryRecord(string str)
    {
        if (str == "")
        {
            return;
        }

        historyRecord += "\n" + yearName + " " + date.ToString() + str; 
    }

    public string HistoryGet()
    {
        return historyRecord; 
    }

	private void InitRelationFemaleOffice2Person()
	{
		int iCount = officeManager.CountFemale;
		Person[] persons = femalePersonManager.GetRange (0, iCount);
        Office[] offices = officeManager.GetRange (typeof(ENUM_OFFICE_FEMALE));

		for (int i = 0; i < iCount; i++) 
		{
			//relOffice2Person.Set(persons[i], offices[i]);
            relationManager.SetOffice2Person(offices[i], persons[i]);
		}
	}

    private void InitZhouj2Office()
    {

            foreach (Province z in provManager)
            {

            }
         
            
    }

	public string time
	{
		get
		{
			return dynastyName + " " + yearName + date.ToString();
		}
	}

    public Province.STATUS[] GetProvinceDebuff()
    {
        return Province.GetDebuffStatus();
    }

    public void EmpDie()
    {
        gameEnd = true;
    }

    //public Person NewPerson(Faction faction)
    //{
    //    Person
    //}

    public bool   gameEnd;

	public string empName;
	public int    empAge;
	public int    empHeath;

	public string dynastyName;
    public int    Stability;
    public int    Economy;
    public int    Military;


    public RelationManager relationManager;
    public PersonManager personManager;
	public PersonManager femalePersonManager;

	public OfficeManager officeManager;
	//public OfficeManager femaleOfficeManager;

	public FactionManager factionManager;

    public ProvinceManager provManager;

    public StatusManager statusManager;

	public string yearName;
	public GameTime date;

    [SerializeField]
    private StringSerialDictionary DictFlag;


    [SerializeField]
    private string historyRecord;
}

