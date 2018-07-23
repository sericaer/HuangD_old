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
            Type type = StreamManager.Types.Where(x => x.Name == "Offices").Single();
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);
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
}