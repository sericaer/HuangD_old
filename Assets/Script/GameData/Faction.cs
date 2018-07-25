using System;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;
using HuangDAPI;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

#if NET_4_6
#else
using Mono.CSharp;
#endif

public partial class MyGame
{
    [JsonObject(MemberSerialization.Fields)]
    public class Faction : SerializeManager, HuangDAPI.Faction
    {

        public static void Initialize()
        {
            Type type = StreamManager.Types.Where(x => x.Name == "Factions").Single();
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);
        }

        public static Faction[] All
        {
            get
            {
                return _All.ToArray();
            }
        }

        public Faction(string name)
        {
            _name = name;
            _All.Add(this);
        }


        public  string name
        {
            get
            {
                return _name;
            }
        }

        public int powerPercent
        {
            get
            {
                int total = 0;
                foreach(var faction in Faction.All)
                {
                    total += faction.power;
                }

                return (int)Math.Round(this.power * 100 /(double)total, 0);
            }
        }

        public int power
        {
            get
            {
                int value = 0;
                foreach (var elem in powerdetail)
                {
                    value += elem.Item2;
                }

                return value;
            }
        }

        internal List<Tuple<string, int>> powerdetail
        {
            get
            {
                List<Tuple<string, int>> list = new List<Tuple<string, int>>();

                var query = MyGame.RelationManager.mapPerson2Faction.FindAll(x => x.faction == this.name);

                foreach (dynamic elem in query)
                {

                    Person person = Person.All.Where((arg) => arg.name == (string)elem.person).Single();

                    int power = 0;

                    if (person.score > 60)
                    {
                        power = 1;
                        if (person.score > 70)
                        {
                            power = 3;
                        }
                        if (person.score > 80)
                        {
                            power = 6;
                        }
                        else if (person.score > 90)
                        {
                            power = 9;
                        }
                        else if (person.score > 95)
                        {
                            power = 12;
                        }

                        list.Add(new Tuple<string, int>(UI.Format("{0}{1}", person.name, "SCORE"), power));
                    }

                    list.Add(new Tuple<string, int>(UI.Format("{0}{1}", person.name, person.office.name), person.office.power));
                }

                return list;
            }
        }
        public override string ToString()
        {
            return name;
        }

        [SerializeField]
        string _name;

        [SerializeField]
        static List<Faction> _All = new List<Faction>();
    }
}