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

	public class Selector
    {
		private Selector()
        {
            throw new NotImplementedException();
        }

        public static BySelector ByPerson(params string[] key)
        {
			return new MyGame.BySelector().ByPerson(key);
        }

        public static BySelector ByOffice(params string[] key)
        {
			return new MyGame.BySelector().ByOffice(key);
        }

        public static BySelector ByFaction(params string[] key)
        {
			return new MyGame.BySelector().ByFaction(key);
        }

        public static BySelector ByPersonNOT(params string[] key)
        {
			return new MyGame.BySelector().ByPersonNOT(key);
        }

        public static BySelector ByOfficeNOT(params string[] key)
        {
			return new MyGame.BySelector().ByOfficeNOT(key);
        }

        public static BySelector ByFactionNOT(params string[] key)
        {
			return new MyGame.BySelector().ByFactionNOT(key);
        }
    }


    public interface Person
    {
		string name { get; }
		int press { get; set; }
    }

	public interface Faction
	{
		string name { get; }
	}

	public interface BySelector
    {
		BySelector ByPerson(params string[] key);
		BySelector ByOffice(params string[] key);
		BySelector ByFaction(params string[] key);
		BySelector ByPersonNOT(params string[] key);
        BySelector ByOfficeNOT(params string[] key);
        BySelector ByFactionNOT(params string[] key);
    }

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
	}

	public class GMData
	{
		public static Person[] GetPersons(BySelector selector)
        {
            return (Person[])MyGame.Inst.GetPerson((MyGame.BySelector)selector);
        }
		public static Faction[] GetFactions(BySelector selector)
        {
			return (Faction[])MyGame.Inst.GetFaction((MyGame.BySelector)selector);
        }

        public static Person GetPerson(BySelector selector)
        {
			Person[] p = GetPersons(selector);
            return p[Probability.GetRandomNum(0, p.Length - 1)];
        }
		public static Faction GetFaction(BySelector selector)
        {
			Faction[] f = GetFactions(selector);
			return f[Probability.GetRandomNum(0, f.Length - 1)];
        }

        public class GlobalFlag
		{
			public static Func<string, string> Get = MyGame.Inst.GetFlag;
			public static void Set(string name, string value = "")
			{
				MyGame.Inst.SetFlag(name, value);
			}

			public static Action<string> Clear = MyGame.Inst.ClearFlag;
		}

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
    }

	public class PersonStatusAttrib : Attribute
    {
        public int press;
    }
}
