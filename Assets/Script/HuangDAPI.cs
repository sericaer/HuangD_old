﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace HuangDAPI
{
    public class Probability : Tools.Probability
    {

    }


    public static class ExtentionMethods
    {
        //public static void Add(this List<PersonFlag> list, string name, string op)
        //{
        //    list.Add(new MyGame.PersonFlag(name, op));
        //}

        //public static void Remove(this List<PersonFlag> list, string name)
        //{
        //    list.RemoveAll(x => x.name == name);
        //}

        //public static void Add(this List<ImpWork> listImpWork, string name, string detail, object src)
        //{
        //    listImpWork.Add(new MyGame.ImpWork(name, detail, src));
        //}

        //public static void Add(this List<ImpWork> listImpWork, string name, string detail, object src, object dest)
        //{
        //    listImpWork.Add(new MyGame.ImpWork(name, detail, src, dest));
        //}

        //public static ImpWork Find(this List<ImpWork> listImpWork, string name)
        //{
        //    return listImpWork.Find(x => x.name == name);
        //}

        //public static bool Contains(this List<ImpWork> listImpWork, string name)
        //{
        //    return listImpWork.Any(x => x.name == name);
        //}

        //public static void Remove(this List<ImpWork> listImpWork, string name)
        //{
        //    listImpWork.RemoveAll(x => x.name == name);
        //}

        public static void RemoveAll(this Dictionary<string, string> dict, Predicate<string> match)
        {
            List<string> needRemoveKeys = new List<string>();
            foreach(var key in dict.Keys)
            {
                if(match(key))
                {
                    needRemoveKeys.Add(key);
                }
            }

            foreach(var key in needRemoveKeys)
            {
                dict.Remove(key);
            }
        }
    }



    //public interface Province
    //{
    //      string name { get; }
    //      string economy { get; }
    //    //public Disaster debuff
    //    //{
    //    //    get
    //    //    {
    //    //        return (from x in GMData.RelationManager.ProvinceStatusMap
    //    //                where x.province == this
    //    //                select x.debuff).SingleOrDefault();
    //    //    }
    //    //    set
    //    //    {
    //    //        GMData.RelationManager.ProvinceStatusMap.Find(x => x.province.name == name).debuff = value;
    //    //    }

    //    //}
    //}

    public abstract class Disaster
    {
        public abstract string name { get; }
        public abstract string saved { get; set; }
        public abstract bool recover { get; set; }
        //public Province[] provinces
        //{
        //    get
        //    {
        //        return (from x in GMData.RelationManager.ProvinceStatusMap
        //                where x.debuff == this
        //                select x.province).ToArray();
        //    }
        //}

    }

    //public interface Status
    //{
    //    string ID { get; }
    //    string name { get; }
    //    string desc { get; }
    //    StatusParam param { get; set; }

    //}

    //public interface ImpWork
    //{
    //    string name { get; set;}
    //    string detail { get; set;}
    //    object src { get; set;}
    //    object dest {get; set;}
    //}

    public interface GMDate
    {
        int year { get; }
        int month { get; }
        int day { get; }

    }

    //public class StatusParam
    //{
    //    public StatusParam(string ID)
    //    {
    //        mID = ID;
    //    }

    //    public string ID 
    //    { 
    //        get
    //        {
    //            return mID;
    //        }
    //    }

    //    public override string ToString()
    //    {
    //        return mID;
    //    }

    //    string mID;
    //}

    public class ReflectBase
	{
		public ReflectBase()
		{
			Type type = this.GetType();

            _subFields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static).ToList();

            _subMethods = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static).ToList();
		}


		protected T GetDelegateInSubEvent<T>(string delegateName, T defaultValue)
		{
			IEnumerable<MethodInfo> methodIEnum = _subMethods.Where(x => x.Name == delegateName);
			if (methodIEnum.Count() == 0)
			{
				return defaultValue;

			}
			return (T)(object)Delegate.CreateDelegate(typeof(T), this, methodIEnum.First());
		}

		protected List<FieldInfo> _subFields;
		protected List<MethodInfo> _subMethods;
	}

    public class GMData
    {

        //public class Provinces
        //{
        //    public static Province[] All
        //    {
        //        get
        //        {
        //            List<Province> list = (from x in RelationManager.ProvinceMap
        //                                 select x.province).ToList();
        //            return list.ToArray();
        //        }
        //    }
        //}

        public class Disasters
        {
            //public static Disaster[] All
            //{
            //    get
            //    {
            //        List<Disaster> list = (from x in RelationManager.ProvinceStatusMap
            //                               where x.debuff != null
            //                               select x.debuff).ToList();
            //        return list.ToArray();
            //    }
            //}
        }

        //public class RelationMapElem
        //{
        //    public Office office;
        //    public Person person;
        //    public Faction faction;
        //}

        //public class FactionMapElem
        //{
        //    public Faction faction;
        //    public Person person;
        //}

        //public class OfficeMapElem
        //{
        //    public Office office;
        //    public Person person;
        //}

        //public class ProvinceMapElem
        //{
        //    public Province province;
        //    public Office office;
        //    //public List<Disaster> debuffList;
        //}

        //public class ProvinceStatusElem
        //{
        //    public Province province;
        //    public Disaster debuff;
        //}


        //public class RelationManager
        //{
        //    public static List<RelationMapElem> RelationMap
        //    {
        //        get
        //        {
        //            return MyGame.Inst.relationManager.GetRelationMap();
        //        }
        //    }

        //    public static List<OfficeMapElem> OfficeMap
        //    {
        //        get
        //        {
        //            return MyGame.Inst.relationManager.GetOfficeMap();
        //        }
        //    }

        //    public static List<FactionMapElem> FactionMap
        //    {
        //        get
        //        {
        //            return MyGame.Inst.relationManager.GetFactionMap();
        //        }
        //    }

        //    public static List<ProvinceMapElem> ProvinceMap
        //    {
        //        get
        //        {
        //            return MyGame.Inst.relationManager.GetProvinceMap();
        //        }
        //    }

        //    public static List<ProvinceStatusElem> ProvinceStatusMap
        //    {
        //        get
        //        {
        //            return MyGame.Inst.relationManager.GetProvinceStatusMap();
        //        }
        //    }

        //    public static void SetOffice(Person person, Office office)
        //    {
        //        NewPersonInfo personInfo = listNewPersonInfo.Find((obj) => obj._person == person);

        //        if (personInfo != null)
        //        {
        //            MyGame.Inst.relationManager.SetFaction2Person(personInfo._faction, person);
        //        }

        //        MyGame.Inst.relationManager.SetOffice2Person(office, person);
        //    }

        //    public static void SetProvinceBuff(Province province, Disaster disaster)
        //    {
        //        MyGame.Inst.relationManager.SetProvinceBuff(province, disaster);
        //    }

        //    public static void RemoveProvinceBuff(Province province, Disaster disaster)
        //    {
        //        MyGame.Inst.relationManager.RemoveProvinceBuff(province, disaster);
        //    }
        //}


        //     public static Person[] GetPersons(BySelector selector = null)
        //     {
        //         return (Person[])MyGame.Inst.GetPerson((MyGame.BySelector)selector);
        //     }
        //     public static Faction[] GetFactions(BySelector selector = null)
        //     {
        //return (Faction[])MyGame.Inst.GetFaction((MyGame.BySelector)selector);
        //}
        //public static Office[] GetOffices(BySelector selector = null)
        //{
        //    return (Office[])MyGame.Inst.GetOffice((MyGame.BySelector)selector);
        //}

        //      public static Person GetPerson(BySelector selector)
        //      {
        //	Person[] p = GetPersons(selector);
        //          if(p == null || p.Length == 0)
        //          {
        //              return null;
        //          }

        //          return p[Probability.GetRandomNum(0, p.Length - 1)];
        //      }
        //public static Faction GetFaction(BySelector selector)
        //     {
        //Faction[] f = GetFactions(selector);
        //return f[Probability.GetRandomNum(0, f.Length - 1)];
        //}
        //public static Office GetOffice(BySelector selector)
        //{
        //    Office[] o = GetOffices(selector);
        //    return o[Probability.GetRandomNum(0, o.Length - 1)];
        //}


        public class TianWenStatus
        {

            //public static void Add(string ID)
            //{
            //    MyGame.Inst.statusManager.listStatus.Add(new MyGame.TWStatus(ID));
            //}

            //public static void Remove(string ID)
            //{
            //    int index = MyGame.Inst.statusManager.listStatus.FindIndex(x => x.ID == ID);
            //    if (index == -1)
            //    {
            //        return;
            //    }

            //    MyGame.Inst.statusManager.listStatus.RemoveAt(index);
            //}

            //public static bool Contains(string ID)
            //{
            //    int index = MyGame.Inst.statusManager.listStatus.FindIndex(x => x.ID == ID);
            //    if (index == -1)
            //    {
            //        return false;
            //    }

            //    return true;
            //}

            //public static StatusParam Get(string ID)
            //{
            //    int index = MyGame.Inst.statusManager.listStatus.FindIndex(x => x.ID == ID);
            //    if (index == -1)
            //    {
            //        return null;
            //    }

            //    return MyGame.Inst.statusManager.listStatus[index].param;
            //}

            //public static void Set(string ID, StatusParam value)
            //{
            //    int index = MyGame.Inst.statusManager.listStatus.FindIndex(x => x.ID == ID);
            //    if (index == -1)
            //    {
            //        throw new ArgumentException(string.Format("can not find {0} in status list", ID));
            //    }

            //    MyGame.Inst.statusManager.listStatus[index].param = value;
            //}
        }

        //public class Emp
        //{
        //    public static int Heath
        //    {
        //        get
        //        {
        //            return MyGame.Inst.empHeath;
        //        }
        //        set
        //        {
        //            MyGame.Inst.empHeath = value;
        //        }
        //    }

        //    public static void Die()
        //    {
        //        MyGame.Inst.EmpDie();
        //    }
        //}

        //public 
        //public static Person NewMalePerson(HuangDAPI.Faction faction)
        //{
        //    Person person = MyGame.Inst.NewPerson(true);
        //    listNewPersonInfo.Add(new NewPersonInfo { _person = person, _faction = faction });

        //    return person;
        //}

        //public static Person NewFemalePerson(HuangDAPI.Faction faction)
        //{
        //    Person person = MyGame.Inst.NewPerson(false);
        //    //listTemPerson.Add(new TempPersonInfo{ _person = person, _faction = faction });

        //    return person;
        //}

        //public static Disaster NewDisaster()
        //{
        //    Disaster disaster = MyGame.Inst.NewDisaster();
        //    return disaster;
        //}

        //public static Person NewPerson(Faction faction)
        //{
        //    return MyGame.Inst.NewPerson(faction);
        //}

        //      public class GlobalFlag
        //{
        //	public static Func<string, string> Get = MyGame.Inst.GetFlag;
        //	public static void Set(string name, string value = "")
        //	{
        //		MyGame.Inst.SetFlag(name, value);
        //	}

        //	public static Action<string> Clear = MyGame.Inst.ClearFlag;
        //}


        //public static Person Emp
        //{
        //    get
        //    {
        //        return MyGame.Inst.Emp;
        //    }
        //}

        //public static int Stability
        //{
        //    get
        //    {
        //        return MyGame.Inst.Stability;
        //    }
        //    set
        //    {
        //        MyGame.Inst.Stability = value;
        //    }
        //}

        //public static int Economy
        //{
        //    get
        //    {
        //        return MyGame.Economy.current;
        //    }
        //    set
        //    {
        //        MyGame.Economy.current = value;
        //    }
        //}

        //public static int Military
        //{
        //    get
        //    {
        //        return MyGame.Inst.Military;
        //    }
        //    set
        //    {
        //        MyGame.Inst.Military = value;
        //    }
        //}

        //public static GMDate Date
        //{
        //    get
        //    {
        //        return MyGame.Inst.date;
        //    }
        //}

        //public static List<string> CountryFlags
        //{
        //    get
        //    {
        //        return MyGame.Inst.CountryFlags;
        //    }
        //}

        //public static List<ImpWork> ImpWorks
        //{
        //    get
        //    {
        //        return MyGame.Inst.ImpWorks;
        //    }
        //}

        public class NewPersonInfo
        {
            public Person _person;
            public Faction _faction;
        }

        public static List<NewPersonInfo> ListNewPersonInfo
        {
            get
            {
                return listNewPersonInfo;
            }
        }

        //public static Dictionary<string, DecisionProc> DecisionProcs
        //{
        //    get
        //    {
        //        return MyGame.DecisionManager.Procs;
        //    }
        //}


        //public static Dictionary<string, DecisionPlan> DecisionPlans
        //{
        //    get
        //    {
        //        return MyGame.DecisionManager.Plans;
        //    }
        //}

        private static List<NewPersonInfo> listNewPersonInfo = new List<NewPersonInfo>();


    }

	public class PersonStatusAttrib : Attribute
    {
        public int press;
    }

    public class UI
    {
        public static string Format(string format, params object[] param)
        {
            List<string> tran = new List<string>();
            for (int i = 0; i < param.Length; i++)
            {
                string temp = param[i] as string;
                if(temp != null)
                {
                    tran.Add(StreamManager.uiDesc.Get(temp));
                }
                else
                {
                    tran.Add(param[i].ToString());
                }
            }

            return string.Format(StreamManager.uiDesc.Get(format), tran.ToArray());
        }
    }
}
