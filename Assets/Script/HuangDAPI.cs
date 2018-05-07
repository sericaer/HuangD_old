using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace HuangDAPI
{
    public abstract class EVENT_HD
    {
        public EVENT_HD()
        {
            Type type = this.GetType();
            _subFields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).ToList();

            _subMethods = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).ToList();

            MethodInfo method = null;

            _funcPrecondition = GetDelegateInSubEvent<Func<bool>>("Precondition",
                                                                  () =>  {  
                                                                    return false;  
                                                                  });
            _funcTitle = GetDelegateInSubEvent<Func<string>>("Title",
                                                            () =>  {  
                                                                FieldInfo field = _subFields.Where(x => x.Name == "title").First();
                                                                return (string)field.GetValue(this);   
                                                            });
            _funcDesc = GetDelegateInSubEvent<Func<string>>("Desc",
                                                            () =>  {  
                                                                FieldInfo field = _subFields.Where(x => x.Name == "desc").First();
                                                                return (string)field.GetValue(this);   
                                                            });
            _funcHistorRecord = GetDelegateInSubEvent<Func<string>>("HistorRecord",
                                                                    () =>  {  
                                                                        return null; 
                                                                    });
            _funcInitialize = GetDelegateInSubEvent<Action<string>>("Initialize",
                                                                  (param) =>  {  
                                                                    return; 
                                                                 });

            listOptions = new List<Option>();
            foreach (Type innerType in type.GetNestedTypes(BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (innerType.BaseType != typeof(EVENT_HD.Option))
                {
                    continue;
                }

                listOptions.Add((EVENT_HD.Option)Activator.CreateInstance(innerType));
            }
        }

        private T GetDelegateInSubEvent<T>(string delegateName, T defaultValue)
        {
            IEnumerable<MethodInfo> methodIEnum = _subMethods.Where(x => x.Name == delegateName);
            if (methodIEnum.Count() == 0)
            {
                return defaultValue;

            }
            return (T)Delegate.CreateDelegate(typeof(T), this, methodIEnum.First());
        }


//        public bool Precondition()
//        {
//            return false;
//        }
//
//        public virtual string Title()
//        {
//            FieldInfo titleField = _subFields.Where(x => x.Name == "title").First();
//            return (string)titleField.GetValue(this);
//        }
//
//        public virtual string Desc()
//        {
//            FieldInfo titleField = _subFields.Where(x => x.Name == "desc").First();
//            return (string)titleField.GetValue(this);
//        }
//
//        public virtual string HistorRecord()
//        {
//            return null;
//        }
//        public virtual void initialize(string param)
//        {
//
//        }

        internal Option[] options
        {
            get
            {
                return listOptions.ToArray();
            }
        }

        public abstract class Option
        {
            public virtual bool Precondition()
            {
                return true;
            }

            public virtual string Desc()
            {
                Type type = this.GetType();
                FieldInfo[] subFields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).ToArray();

                FieldInfo titleField = subFields.Where(x => x.Name == "desc").First();
                return (string)titleField.GetValue(this);
            }

            public abstract string Selected(out string ret);
        }



        public Func<bool> _funcPrecondition;
        public Func<string> _funcTitle;
        public Func<string> _funcDesc;
        public Func<string> _funcHistorRecord;
        public Action<string> _funcInitialize;

        private List<FieldInfo> _subFields;
        private List<MethodInfo> _subMethods;
        private List<Option> listOptions;


    }
}
