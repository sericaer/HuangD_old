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

            listOptions = new List<Option>();
            foreach (Type innerType in type.GetNestedTypes())
            {
                if (innerType.BaseType != typeof(EVENT_HD.Option))
                {
                    continue;
                }

                listOptions.Add((EVENT_HD.Option)Activator.CreateInstance(innerType));
            }
        }

        public virtual bool Precondition()
        {
            return false;
        }

        public virtual string Title()
        {
            FieldInfo titleField = _subFields.Where(x => x.Name == "title").First();
            return (string)titleField.GetValue(this);
        }

        public virtual string Desc()
        {
            FieldInfo titleField = _subFields.Where(x => x.Name == "desc").First();
            return (string)titleField.GetValue(this);
        }

        public virtual string HistorRecord()
        {
            return null;
        }

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

        private List<FieldInfo> _subFields;
        private List<Option> listOptions;

        public virtual void initialize(string param)
        {
            
        }
    }
}
