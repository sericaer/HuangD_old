using System;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;
using HuangDAPI;
using System.Runtime.CompilerServices;

#if NET_4_6
#else
using Mono.CSharp;
#endif

public partial class MyGame
{
    [Serializable]
    public class Faction : HuangDAPI.Faction
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


        public override string name
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

        internal override int power
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

                var query = MyGame.RelationManager.mapPerson2Faction.FindAll(x=>x.faction == this);

                foreach (var elem in query)
                {
                    int power = 0;

                    if (elem.person.score > 60)
                    {
                        power = 1;
                        if (elem.person.score > 70)
                        {
                            power = 3;
                        }
                        if (elem.person.score > 80)
                        {
                            power = 6;
                        }
                        else if (elem.person.score > 90)
                        {
                            power = 9;
                        }
                        else if (elem.person.score > 95)
                        {
                            power = 12;
                        }

                        list.Add(new Tuple<string, int>(UI.Format("{0}{1}", elem.person.name, "SCORE"), power));
                    }

                    list.Add(new Tuple<string, int>(UI.Format("{0}{1}", elem.person.name, elem.person.office.name), elem.person.office.power));
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

        static List<Faction> _All = new List<Faction>();
    }
}