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

            MethodInfo method = null;
            method = type.GetMethod("Precondition");
            _funcPrecondition = (Func<bool>)Delegate.CreateDelegate(_funcPrecondition.GetType(), this, method);
 
            method = type.GetMethod("Title");
            _funcTitle = (Func<string>)Delegate.CreateDelegate(_funcTitle.GetType(), this, method);

            method = type.GetMethod("Desc");
            _funcDesc =  (Func<string>)Delegate.CreateDelegate(_funcDesc.GetType(), this, method);

            method = type.GetMethod("HistorRecord");
            _funcHistorRecord =  (Func<string>)Delegate.CreateDelegate(_funcHistorRecord.GetType(), this, method);

            method = type.GetMethod("Initialize");
            _funcInitialize =  (Action<string>)Delegate.CreateDelegate(_funcInitialize.GetType(), this, method);

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
        private List<Option> listOptions;


    }
}
