using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq.Expressions;

using UnityEngine;

using System.Linq;
using System.Runtime.CompilerServices;

public partial class MyGame
{
    [Serializable]
    public class Office : HuangDAPI.Office
    {
        public Office(string name, int power, OfficeGroup group)
        {
            _name = name;
            _power = power;
            _group = group;

            _All.Add(this);
        }

        public static Office[] All
        {
            get
            {
                return _All.ToArray();
            }
        }

        public static void Initialize()
        {
            RuntimeHelpers.RunClassConstructor(StreamManager.OfficesType.TypeHandle);
        }

        public static Office[] groupCenter1
        {
            get
            {
                return _All.Where(x => x._group == OfficeGroup.Center1).ToArray();
            }
        }

        public static Office[] groupCenter2
        {
            get
            {
                return _All.Where(x => x._group == OfficeGroup.Center2).ToArray();
            }
        }

        public static Office[] groupLocal
        {
            get
            {
                return _All.Where(x => x._group == OfficeGroup.Local).ToArray();
            }
        }

        public static Office[] groupHougong1
        {
            get
            {
                return _All.Where(x => x._group == OfficeGroup.Hougong1).ToArray();
            }
        }

        public static Office[] groupHougong2
        {
            get
            {
                return _All.Where(x => x._group == OfficeGroup.Hougong2).ToArray();
            }
        }

        public static Office[] groupHougong3
        {
            get
            {
                return _All.Where(x => x._group == OfficeGroup.Hougong3).ToArray();
            }
        }

        public Office(string name, int power)
        {
            _name = name;
            _power = power;
        }

        public override string name
        {
            get
            {
                return _name;
            }
        }

        public override int power
        {
            get
            {
                return _power;
            }
        }

        public Person person
        {
            get
            {
                return (from x in MyGame.RelationManager.mapOffice2Person
                 where x.office == this
                 select x.person).SingleOrDefault();
            }
        }

        [SerializeField]
        string _name;

        [SerializeField]
        int _power;

        [SerializeField]
        OfficeGroup _group;

        public static List<Office> _All = new List<Office>();
    }

    [Serializable]
    public class Hougong : HuangDAPI.Office
    {
        public Hougong(string name, HougongGroup group)
        {
            _name = name;
            _group = group;

            _All.Add(this);
        }

        public static Hougong[] All
        {
            get
            {
                return _All.ToArray();
            }
        }

        public static void Initialize()
        {
            RuntimeHelpers.RunClassConstructor(StreamManager.OfficesType.TypeHandle);
        }

        public static Office[] groupCenter1
        {
            get
            {
                return _All.Where(x => x._group == OfficeGroup.Center1).ToArray();
            }
        }

        [SerializeField]
        string _name;

        [SerializeField]
        OfficeGroup _group;

        public static List<Hougong> _All = new List<Hougong>();
    }

    public class OfficeAttrAttribute : Attribute
    {
        public int Power;
    }

    public enum ENUM_OFFICE_CENTER
    {
        [OfficeAttr(Power = 10)]
        SG1,

        [OfficeAttr(Power = 8)]
        SG2,

        [OfficeAttr(Power = 7)]
        SG3,

        [OfficeAttr(Power = 10)]
        JQ1,

        [OfficeAttr(Power = 5)]
        JQ2,

        [OfficeAttr(Power = 5)]
        JQ3,

        [OfficeAttr(Power = 5)]
        JQ4,

        [OfficeAttr(Power = 5)]
        JQ5,

        [OfficeAttr(Power = 5)]
        JQ6,

        [OfficeAttr(Power = 5)]
        JQ7,

        [OfficeAttr(Power = 5)]
        JQ8,

        [OfficeAttr(Power = 5)]
        JQ9,

//        [OfficeAttr(Power = 3)]
//        CS1,
//
//        [OfficeAttr(Power = 3)]
//        CS2,
//
//        [OfficeAttr(Power = 3)]
//        CS3,
//
//        [OfficeAttr(Power = 3)]
//        CS4,
//
//        [OfficeAttr(Power = 3)]
//        CS5,
//
//        [OfficeAttr(Power = 3)]
//        CS6,
//
//        [OfficeAttr(Power = 3)]
//        CS7,
//
//        [OfficeAttr(Power = 3)]
//        CS8,
//
//        [OfficeAttr(Power = 3)]
//        CS9,
    }

    public enum ENUM_OFFICE_LOCAL
    {
        [OfficeAttr(Power = 3)]
        CS
    }

    public enum ENUM_OFFICE_FEMALE
    {
        [OfficeAttr(Power = 0)]
        HOU,

        [OfficeAttr(Power = 0)]
        GUI1,

        [OfficeAttr(Power = 0)]
        GUI2,

        [OfficeAttr(Power = 0)]
        GUI3,

        [OfficeAttr(Power = 0)]
        FEI1,

        [OfficeAttr(Power = 0)]
        FEI2,

        [OfficeAttr(Power = 0)]
        FEI3,

        [OfficeAttr(Power = 0)]
        FEI4,

        [OfficeAttr(Power = 0)]
        FEI5,

        [OfficeAttr(Power = 0)]
        FEI6,
    }

    public class OfficeManager : ISerializationCallbackReceiver
    {
        public OfficeManager()
        {
            foreach (var eOffice in Enum.GetValues(typeof(ENUM_OFFICE_CENTER)))
            {

                FieldInfo field = eOffice.GetType().GetField(eOffice.ToString());
                OfficeAttrAttribute attribute = Attribute.GetCustomAttribute(field, typeof(OfficeAttrAttribute)) as OfficeAttrAttribute;

                Office office = new Office(eOffice.ToString(), attribute.Power);
                lstOfficeCenter.Add(office);
               
                DictName2Office.Add(office.name, office);
            }


            foreach (var eOffice in Enum.GetValues(typeof(ENUM_OFFICE_FEMALE)))
            {

                FieldInfo field = eOffice.GetType().GetField(eOffice.ToString());
                OfficeAttrAttribute attribute = Attribute.GetCustomAttribute(field, typeof(OfficeAttrAttribute)) as OfficeAttrAttribute;

                Office office = new Office(eOffice.ToString(), attribute.Power);
                lstOfficeFemale.Add(office);

                DictName2Office.Add(office.name, office);
            }

            foreach (var eOffice in Enum.GetValues(typeof(ENUM_OFFICE_LOCAL)))
            {
                FieldInfo field = eOffice.GetType().GetField(eOffice.ToString());
                OfficeAttrAttribute attribute = Attribute.GetCustomAttribute(field, typeof(OfficeAttrAttribute)) as OfficeAttrAttribute;

                foreach(var eZhouj in Enum.GetValues(typeof(Province.ENUM_PROV)))
                {

                    Office office = new Office(eZhouj.ToString() + "|" + eOffice.ToString(), attribute.Power);
                    lstOfficeLocal.Add(office);

                    DictName2Office.Add(office.name, office);

                    MyGame.Inst.relZhouj2Office.Set(MyGame.Inst.provManager.GetByName(eZhouj.ToString()), office);
                }

            }

        }

        public void OnBeforeSerialize()
        {

        }

        public void OnAfterDeserialize()
        {
            foreach (var office in lstOfficeCenter)
            {
                DictName2Office.Add(office.name, office);
            }

            foreach (var office in lstOfficeFemale)
            {
                DictName2Office.Add(office.name, office);
            }

            foreach (var office in lstOfficeLocal)
            {
                DictName2Office.Add(office.name, office);
            }
        }

        public Office[] GetRange(Type t)
        {
            List<Office> lstOffice = null;
            switch (t.Name)
            {
                case "ENUM_OFFICE_CENTER":
                    lstOffice = lstOfficeCenter;
                    break;
                case "ENUM_OFFICE_LOCAL":
                    lstOffice = lstOfficeLocal;
                    break;
                case "ENUM_OFFICE_FEMALE":
                    lstOffice = lstOfficeFemale;
                    break;
            }

            return GetRange(t, 0, lstOffice.Count);
        }

        public Office[] GetRange(Type t, int start, int end)
        {
            List<Office> lstOffice = null;
            switch (t.Name)
            {
                case "ENUM_OFFICE_CENTER":
                    lstOffice = lstOfficeCenter;
                    break;
                case "ENUM_OFFICE_LOCAL":
                    lstOffice = lstOfficeLocal;
                    break;
                case "ENUM_OFFICE_FEMALE":
                    lstOffice = lstOfficeFemale;
                    break;
            }

            if (start > lstOffice.Count || start >= end)
            {
                Office[] ps = { };
                return ps;
            }

            if (end > lstOffice.Count)
            {
                end = lstOffice.Count;
            }

            return lstOffice.GetRange(start, end - start).ToArray();
        }

        public int CountCenter
        {
            get
            {
                return lstOfficeCenter.Count;
            }
        }

        public int CountLocal
        {
            get
            {
                return lstOfficeLocal.Count;
            }
        }

        public int CountFemale
        {
            get
            {
                return lstOfficeFemale.Count;
            }
        }

        public Office GetByName(string name)
        {
            return DictName2Office[name];
        }

        public Office[] GetByName(string[] names)
        {
            List<Office> lstResult = new List<Office>();
            foreach (string name in names)
            {
                lstResult.Add(GetByName(name));
            }

            return lstResult.ToArray();
        }

        //public List<Office> GetOfficeBySelector(SelectElem Selector)
        //{
        //    List<Office> lstResult = DictName2Office.Values.Where(Selector.Complie<Office>()).ToList();
        //    return lstResult;
        //}

        [SerializeField]
        private List<Office> lstOfficeCenter = new List<Office>();

        [SerializeField]
        private List<Office> lstOfficeLocal = new List<Office>();

        [SerializeField]
        private List<Office> lstOfficeFemale = new List<Office>();

        private Dictionary<string, Office> DictName2Office = new Dictionary<string, Office>();
    }
}