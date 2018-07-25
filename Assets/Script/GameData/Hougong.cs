using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq.Expressions;

using UnityEngine;

using System.Linq;
using System.Runtime.CompilerServices;
using HuangDAPI;
using Newtonsoft.Json;

public partial class MyGame
{
    [JsonObject(MemberSerialization.Fields)]
    public class Hougong : SerializeManager, HuangDAPI.Hougong
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
            Type type = StreamManager.Types.Where(x => x.Name == "Hougongs").Single();
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);
        }

        public static Hougong[] Level1
        {
            get
            {
                return _All.Where(x => x._group == HougongGroup.Level1).ToArray();
            }
        }

        public static Hougong[] Level2
        {
            get
            {
                return _All.Where(x => x._group == HougongGroup.Level2).ToArray();
            }
        }

        public static Hougong[] Level3
        {
            get
            {
                return _All.Where(x => x._group == HougongGroup.Level3).ToArray();
            }
        }

        public string name
        {
            get
            {
                return _name;
            }
        }

        public  HuangDAPI.Person person
        {
            get
            {
                string personname =  (from x in MyGame.RelationManager.mapHougong2Person
                        where x.hougong == this.name
                        select x.person).SingleOrDefault();
                if(personname == null)
                {
                    return null;
                }

                return Person.Females.Where((arg) => arg.name == personname).Single();
            }
        }

        [SerializeField]
        string _name;

        [SerializeField]
        HougongGroup _group;

        [SerializeField]
        public static List<Hougong> _All = new List<Hougong>();
    }
}
