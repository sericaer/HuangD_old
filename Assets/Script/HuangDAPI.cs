using System;
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

	//public class Selector
  //  {
		//private Selector()
   //     {
   //         throw new NotImplementedException();
   //     }

   //     public static BySelector ByPerson(params string[] key)
   //     {
			//return new MyGame.BySelector().ByPerson(key);
   //     }

   //     public static BySelector ByOffice(params string[] key)
   //     {
			//return new MyGame.BySelector().ByOffice(key);
   //     }

   //     public static BySelector ByFaction(params string[] key)
   //     {
			//return new MyGame.BySelector().ByFaction(key);
   //     }

   //     public static BySelector ByPersonNOT(params string[] key)
   //     {
			//return new MyGame.BySelector().ByPersonNOT(key);
   //     }

   //     public static BySelector ByOfficeNOT(params string[] key)
   //     {
			//return new MyGame.BySelector().ByOfficeNOT(key);
   //     }

   //     public static BySelector ByFactionNOT(params string[] key)
   //     {
			//return new MyGame.BySelector().ByFactionNOT(key);
    //    }
    //}



    public interface Person
    {
		string name { get; }
		int press { get; set; }
        int score { get; set; }
        void Die();
        MyGame.PersonProcess Process(string name, params object[] param);
    }

    public interface PersonProcess
    {
        Person opp { get; }
        List<object> tag { get; }
    }

    public interface Office
    {
        string name { get; }
        int power { get; }
    }

	public interface Faction
	{
		string name { get; }

	}

    public interface Province
    {
        string name { get; }
        string economy { get; }
    }

    public interface Disaster
    {
        string name { get; }
        string saved { get; set; }
        bool recover { get; set; }

    }

    public interface Status
    {
        string ID { get; }
        string name { get; }
        string desc { get; }
        StatusParam param { get; set; }

    }

    public class StatusParam
    {
        public StatusParam(string ID)
        {
            mID = ID;
        }

        public string ID 
        { 
            get
            {
                return mID;
            }
        }

        public override string ToString()
        {
            return mID;
        }

        string mID;
    }

  //  public interface BySelector
  //  {
		//BySelector ByPerson(params string[] key);
		//BySelector ByOffice(params string[] key);
		//BySelector ByFaction(params string[] key);
		//BySelector ByPersonNOT(params string[] key);
    //    BySelector ByOfficeNOT(params string[] key);
    //    BySelector ByFactionNOT(params string[] key);
    //}

    public class ReflectBase
	{
		public ReflectBase()
		{
			Type type = this.GetType();

			_subFields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).ToList();

			_subMethods = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).ToList();
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

	public abstract class EVENT_HD : ReflectBase
	{
		public EVENT_HD()
		{
			title = StreamManager.uiDesc.Get(this.GetType().Name + "_TITLE");
			desc = StreamManager.uiDesc.Get(this.GetType().Name + "_DESC");

			_funcPrecondition = GetDelegateInSubEvent<Func<bool>>("Precondition",
																  () =>
																  {
																	  return false;
																  });
			_funcTitle = GetDelegateInSubEvent<Func<string>>("Title",
															() =>
															{
																FieldInfo field = _subFields.Where(x => x.Name == "title").First();
																return (string)field.GetValue(this);
															});
			_funcDesc = GetDelegateInSubEvent<Func<string>>("Desc",
															() =>
															{
																FieldInfo field = _subFields.Where(x => x.Name == "desc").First();
																return (string)field.GetValue(this);
															});
			_funcHistorRecord = GetDelegateInSubEvent<Func<string>>("HistorRecord",
																	() =>
																	{
																		return null;
																	});
			_funcInitialize = GetDelegateInSubEvent<Action<string>>("Initialize",
																  (param) =>
																  {
																	  return;
																  });

			listOptions = new List<Option>();

			Type[] nestedTypes = this.GetType().GetNestedTypes(BindingFlags.NonPublic | BindingFlags.Public);

			for (int i = nestedTypes.Count() - 1; i >= 0; i--)
			{
				if (nestedTypes[i].BaseType != typeof(EVENT_HD.Option))
				{
					continue;
				}
                
				EVENT_HD.Option option = (EVENT_HD.Option)Activator.CreateInstance(nestedTypes[i]);
				option.Initialize(this);
				listOptions.Add(option);
			}
		}

        internal void LoadMemento()
        {
            Dictionary<string, object> dict =_mementoDict[this.GetType().Name];

            for (int i = 0; i < _subFields.Count; i++)
            {
                if(dict.ContainsKey(_subFields[i].Name))
                {
                    _subFields[i].SetValue(this, dict[_subFields[i].Name]);
                }
            }
           
        }

        internal void SetMemento()
        {
            List<FieldInfo> _superFields = this.GetType().BaseType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).ToList();
            List<FieldInfo> subFields = new List<FieldInfo>();

            foreach(FieldInfo fsub in _subFields)
            {
                if(_superFields.Find(x=>x.Name == fsub.Name) == null)
                {
                    subFields.Add(fsub);
                }
            }
            
            Dictionary<string, object> attribDic = new Dictionary<string, object>();
            foreach(FieldInfo field in subFields)
            {
                attribDic.Add(field.Name, field.GetValue(this));
            }
            _mementoDict.Add(this.GetType().Name, attribDic);
        }

		internal Option[] options
		{
			get
			{
				return listOptions.ToArray();
			}
		}

		public abstract class Option : ReflectBase
		{   
			public Option()
			{
			}

			internal void Initialize(EVENT_HD outter)
			{
				FieldInfo[] fields = _subFields.Where(x => x.Name == "OUTTER").ToArray();
				if(fields.Length != 0)
				{
					fields.First().SetValue(this, outter);
				}

				desc = StreamManager.uiDesc.Get(outter.GetType().Name + "_" + this.GetType().Name + "_DESC");

				_funcPrecondition = GetDelegateInSubEvent<Func<bool>>("Precondition",
                                                  () =>
                                                  {
                                                      return true;
                                                  });
                _funcDesc = GetDelegateInSubEvent<Func<string>>("Desc",
                                                () =>
                                                {
                                                    FieldInfo field = _subFields.Where(x => x.Name == "desc").First();
                                                    return (string)field.GetValue(this);
                                                });
                _funcSelected = GetDelegateInSubEvent<DelegateSelected>("Selected",
                                                (ref string nxtEvent, ref string param) =>
                                                {
                                                });

			}

			public delegate void DelegateSelected(ref string nxtEvent, ref string param);

			public Func<bool> _funcPrecondition;
			public Func<string> _funcDesc;
			public DelegateSelected _funcSelected;

			//public EVENT_HD OUTTER;
            
			protected string desc;
           
		}



		public Func<bool> _funcPrecondition;
		public Func<string> _funcTitle;
		public Func<string> _funcDesc;
		public Func<string> _funcHistorRecord;
		public Action<string> _funcInitialize;

		protected string title;
		protected string desc;

		private List<Option> listOptions;
        private static Dictionary<string, Dictionary<string, object>> _mementoDict = new Dictionary<string, Dictionary<string, object>>();
	}

	public class GMData
	{
        public class RelationMapElem
        {
            public Office office;
            public Person person;
            public Faction faction;
        }

        public class FactionMapElem
        {
            public Faction faction;
            public Person person;
        }

        public class OfficeMapElem
        {
            public Office office;
            public Person person;
        }

        public class ProvinceMapElem
        {
            public Province province;
            public Office office;
            //public List<Disaster> debuffList;
        }

        public class ProvinceStatusElem
        {
            public Province province;
            public Disaster debuff;
        }


        public class RelationManager
        {
            public static List<RelationMapElem> RelationMap
            {
                get
                {
                    return MyGame.Inst.relationManager.GetRelationMap();
                }
            }

            public static List<OfficeMapElem> OfficeMap
            {
                get
                {
                    return MyGame.Inst.relationManager.GetOfficeMap();
                }
            }

            public static List<FactionMapElem> FactionMap
            {
                get
                {
                    return MyGame.Inst.relationManager.GetFactionMap();
                }
            }

            public static List<ProvinceMapElem> ProvinceMap
            {
                get
                {
                    return MyGame.Inst.relationManager.GetProvinceMap();
                }
            }

            public static List<ProvinceStatusElem> ProvinceStatusMap
            {
                get
                {
                    return MyGame.Inst.relationManager.GetProvinceStatusMap();
                }
            }

            public static void SetOffice(Person person, Office office)
            {
                NewPersonInfo personInfo = listNewPersonInfo.Find((obj) => obj._person == person);

                if(personInfo != null)
                {
                    MyGame.Inst.relationManager.SetFaction2Person(personInfo._faction, person);
                }

                MyGame.Inst.relationManager.SetOffice2Person(office, person);
            }

            public static void SetProvinceBuff(Province province, Disaster disaster)
            {
                MyGame.Inst.relationManager.SetProvinceBuff(province, disaster);
            }

            public static void RemoveProvinceBuff(Province province, Disaster disaster)
            {
                MyGame.Inst.relationManager.RemoveProvinceBuff(province, disaster);
            }
        }


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

            public static void Add(string ID)
            {
                MyGame.Inst.statusManager.listStatus.Add(new MyGame.TWStatus(ID));
            }

            public static void Remove(string ID)
            {
                int index = MyGame.Inst.statusManager.listStatus.FindIndex(x => x.ID == ID);
                if (index == -1)
                {
                    return;
                }

                MyGame.Inst.statusManager.listStatus.RemoveAt(index);
            }

            public static bool Contains(string ID)
            {
                int index = MyGame.Inst.statusManager.listStatus.FindIndex(x => x.ID == ID);
                if(index == -1)
                {
                    return false;
                }

                return true;
            }

            public static StatusParam Get(string ID)
            {
                int index = MyGame.Inst.statusManager.listStatus.FindIndex(x => x.ID == ID);
                if (index == -1)
                {
                    return null;
                }

                return MyGame.Inst.statusManager.listStatus[index].param;
            }

            public static void Set(string ID, StatusParam value)
            {
                int index = MyGame.Inst.statusManager.listStatus.FindIndex(x => x.ID == ID);
                if (index == -1)
                {
                    throw new ArgumentException(string.Format("can not find {0} in status list", ID));
                }

                MyGame.Inst.statusManager.listStatus[index].param = value;
            }
        }

        public class Emp
        {
            public static int Heath
            {
                get
                {
                    return MyGame.Inst.empHeath;
                }
                set
                {
                    MyGame.Inst.empHeath = value;
                }
            }

            public static void Die()
            {
                MyGame.Inst.EmpDie();
            }
        }

        public static Person NewMalePerson(HuangDAPI.Faction faction)
        {
            Person person =  MyGame.Inst.NewPerson(true);
            listNewPersonInfo.Add(new NewPersonInfo{ _person = person, _faction = faction });

            return person;
        }

        public static Person NewFemalePerson(HuangDAPI.Faction faction)
        {
            Person person = MyGame.Inst.NewPerson(false);
            //listTemPerson.Add(new TempPersonInfo{ _person = person, _faction = faction });

            return person;
        }

        public static Disaster NewDisaster()
        {
            Disaster disaster = MyGame.Inst.NewDisaster();
            return disaster;
        }

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



        public static int Stability
        {
            get
            {
                return MyGame.Inst.Stability;
            }
            set
            {
                MyGame.Inst.Stability = value;
            }
        }

        public static int Economy
        {
            get
            {
                return MyGame.Inst.Economy;
            }
            set
            {
                MyGame.Inst.Economy = value;
            }
        }

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

        private static List<NewPersonInfo> listNewPersonInfo = new List<NewPersonInfo>();


    }

	public class PersonStatusAttrib : Attribute
    {
        public int press;
    }

    public class UI
    {
        public static string Format(string format, params string[] param)
        {
            string[] trans = (from x in param
                              select StreamManager.uiDesc.Get(x)).ToArray();

            return string.Format(StreamManager.uiDesc.Get(format), trans);
        }
    }
}
