using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HuangDAPI
{
    public abstract class EVENT_HD : ReflectBase
    {
        public EVENT_HD()
        {
            title = StreamManager.uiDesc.Get(this.GetType().Name + "_TITLE");
            desc = StreamManager.uiDesc.Get(this.GetType().Name + "_DESC");

            _funcPrecondition = GetDelegateInSubEvent<DelegatePrecondition>("Precondition",
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
                                                                FieldInfo field = _subFields.Where(x => x.Name == "title").First();
                                                                return (string)field.GetValue(this);
                                                            });
            _funcHistorRecord = GetDelegateInSubEvent<Func<string>>("HistorRecord",
                                                                    () =>
                                                                    {
                                                                        return null;
                                                                    });
            _funcInitialize = GetDelegateInSubEvent<Action<object>>("Initialize",
                                                                      (aaa) =>
                                                                      {
                                                                          return;
                                                                      });

            List<PropertyInfo> propers = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).ToList();
            _sponsor = propers.Find(x => x.Name == "Sponsor");

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

        public bool funcPrecondition(dynamic Precondition)
        {
            this.PreData = Precondition;

            foreach(var option in listOptions)
            {
                option.PreData = Precondition;
            }

            return _funcPrecondition();
        }

        internal void LoadMemento()
        {
            Dictionary<string, object> dict = _mementoDict[this.GetType().Name];

            for (int i = 0; i < _subFields.Count; i++)
            {
                if (dict.ContainsKey(_subFields[i].Name))
                {
                    _subFields[i].SetValue(this, dict[_subFields[i].Name]);
                }
            }

        }

        internal void SetMemento()
        {
            List<FieldInfo> _superFields = this.GetType().BaseType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).ToList();
            List<FieldInfo> subFields = new List<FieldInfo>();

            foreach (FieldInfo fsub in _subFields)
            {
                if (_superFields.Find(x => x.Name == fsub.Name) == null)
                {
                    subFields.Add(fsub);
                }
            }

            Dictionary<string, object> attribDic = new Dictionary<string, object>();
            foreach (FieldInfo field in subFields)
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

        public bool IsSponsorVaild()
        {
            if (_sponsor == null)
            {
                return true;
            }

            return _sponsor.GetValue(this) != null;
        }

        public int LastTriggleInterval
        {
            get
            {
                if(LastTriggleDay == null)
                {
                    return int.MaxValue;
                }

                return MyGame.GameTime.current - LastTriggleDay;
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
                if (fields.Length != 0)
                {
                    fields.First().SetValue(this, outter);
                }

                desc = StreamManager.uiDesc.Get(outter.GetType().Name + "_" + this.GetType().Name + "_DESC");

                _funcIsVisable = GetDelegateInSubEvent<DelegateIsVisable>("IsVisable",
                                                                   () =>
                                                                  {
                                                                      return true;
                                                                  });
                _funcDesc = GetDelegateInSubEvent<DelegatecDesc>("Desc",
                                                                 () =>
                                                                    {
                                                                        FieldInfo field = _subFields.Where(x => x.Name == "desc").First();
                                                                        string Desc = (string)field.GetValue(this);
                                                                        return Desc;
                                                                    });
                _funcSelected = GetDelegateInSubEvent<DelegateSelected>("Selected",
                                                                        (ref string nxtEvent, ref object param) =>
                                                                        {
                                                                        });

                this.OUTTER = outter;

            }

            public Decision AssocDecision
            {
                get
                {
                    return OUTTER.AssocDecision;
                }
            }

            public dynamic Decision
            {
                get
                {
                    return OUTTER.Decision;
                }
            }

            public delegate void DelegateSelected(ref string nxtEvent, ref object param);
            public delegate string DelegatecDesc();
            public delegate bool DelegateIsVisable();

            public DelegateIsVisable _funcIsVisable;
            public DelegatecDesc _funcDesc;
            public DelegateSelected _funcSelected;

            public dynamic OUTTER;

            public dynamic PreData;

            protected string desc;

        }


        public MyGame.GameTime LastTriggleDay = null;

        public delegate bool DelegatePrecondition();

        public DelegatePrecondition _funcPrecondition;
        public Func<string> _funcTitle;
        public Func<string> _funcDesc;
        public Func<string> _funcHistorRecord;
        public Action<object> _funcInitialize;

        protected string title;
        protected string desc;
        protected dynamic PreData;

        public object param;
        public Decision AssocDecision;
        private PropertyInfo _sponsor;
        public dynamic Decision;

        private List<Option> listOptions;
        private static Dictionary<string, Dictionary<string, object>> _mementoDict = new Dictionary<string, Dictionary<string, object>>();
    }
}
