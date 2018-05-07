using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace HuangDAPI
{
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

            Type[] nestedTypes = this.GetType().GetNestedTypes(BindingFlags.NonPublic | BindingFlags.Public);

            for(int i= nestedTypes.Count()-1; i>=0; i--)
            {
                if (nestedTypes[i].BaseType != typeof(EVENT_HD.Option))
                {
                    continue;
                }

                EVENT_HD.Option option = (EVENT_HD.Option)Activator.CreateInstance(nestedTypes[i]);
                option.OUTTER = this;
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
                _funcPrecondition = GetDelegateInSubEvent<Func<bool>>("Precondition",
                                                                  () => {
                                                                      return true;
                                                                  });
                _funcDesc = GetDelegateInSubEvent<Func<string>>("Desc",
                                                () => {
                                                    FieldInfo field = _subFields.Where(x => x.Name == "desc").First();
                                                    return (string)field.GetValue(this);
                                                });
                _funcSelected = GetDelegateInSubEvent<DelegateSelected>("Selected",
                                                (out string param) => {
                                                    param = null;
                                                    return null;
                                                });
            }

            public delegate string DelegateSelected(out string param);
            public Func<bool> _funcPrecondition;
            public Func<string> _funcDesc;
            public DelegateSelected _funcSelected;
            public EVENT_HD OUTTER;
        }



        public Func<bool> _funcPrecondition;
        public Func<string> _funcTitle;
        public Func<string> _funcDesc;
        public Func<string> _funcHistorRecord;
        public Action<string> _funcInitialize;


        private List<Option> listOptions;


    }
}
