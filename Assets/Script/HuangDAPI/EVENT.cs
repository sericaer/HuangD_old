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

            _funcPrecondition = GetDelegateInSubEvent<Precondition>("Precondition",
                                                                  (ref dynamic result) =>
                                                                  {
                                                                      result = null;
                                                                      return false;
                                                                  });
            _funcTitle = GetDelegateInSubEvent<Func<string>>("Title",
                                                            () =>
                                                            {
                                                                FieldInfo field = _subFields.Where(x => x.Name == "title").First();
                                                                return (string)field.GetValue(this);
                                                            });
            _funcDesc = GetDelegateInSubEvent<Func<object>>("Desc",
                                                            () =>
                                                            {
                                                                FieldInfo field = _subFields.Where(x => x.Name == "desc").First();
                                                                return field.GetValue(this);
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
                                                                   (dynamic precondition) =>
                                                                  {
                                                                      return true;
                                                                  });
                _funcDesc = GetDelegateInSubEvent<DelegatecDesc>("Desc",
                                                                 (dynamic param) =>
                                                                    {
                                                                        FieldInfo field = _subFields.Where(x => x.Name == "desc").First();
                                                                        string Desc = (string)field.GetValue(this);
                                                                        return Desc;
                                                                    });
                _funcSelected = GetDelegateInSubEvent<DelegateSelected>("Selected",
                                                                        (dynamic precondition, ref string nxtEvent, ref object param) =>
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

            public delegate void DelegateSelected(dynamic precondition, ref string nxtEvent, ref object param);
            public delegate string DelegatecDesc(dynamic param);
            public delegate bool DelegateIsVisable(dynamic precondition);

            public DelegateIsVisable _funcIsVisable;
            public DelegatecDesc _funcDesc;
            public DelegateSelected _funcSelected;

            public dynamic OUTTER;

            protected string desc;

        }



        public delegate bool Precondition(ref dynamic preResult);

        public Precondition _funcPrecondition;
        public Func<string> _funcTitle;
        public Func<object> _funcDesc;
        public Func<string> _funcHistorRecord;
        public Action<object> _funcInitialize;

        protected string title;
        protected string desc;
        public object param;
        public Decision AssocDecision;
        private PropertyInfo _sponsor;

        private List<Option> listOptions;
        private static Dictionary<string, Dictionary<string, object>> _mementoDict = new Dictionary<string, Dictionary<string, object>>();
    }
}
