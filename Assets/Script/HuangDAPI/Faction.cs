using System;
using System.Collections.Generic;
using System.Linq;

namespace HuangDAPI
{
    public abstract class Faction
    { 
        public abstract string name { get; }
        internal abstract int power { get; }
    }

    public static class Factions
    {
        public static Faction[] All
        {
            get
            {
                List<Faction> list = (from x in GMData.RelationManager.FactionMap
                                      select x.faction).ToList();
                return list.Distinct().ToArray();
            }
        }

        public static Dictionary<string, int> Power
        {
            get
            {
                int total = 0;
                List<Tuple<string, int>> list = new List<Tuple<string, int>>();
                foreach (var f in All)
                {
                    int power = f.power;
                    total += power;

                    list.Add(new Tuple<string, int>(f.name, power));
                }

                Dictionary<string, int> rslt = new Dictionary<string, int>();
                foreach (var e in list)
                {
                    double value = (double)e.Item2 * 100 / total;
                    rslt.Add(e.Item1, (int)Math.Round(value));
                }

                return rslt;
            }

        }

        public static Faction Random()
        {
            return (from x in GMData.RelationManager.FactionMap
                    orderby (Guid.NewGuid())
                    select x.faction).First();

        }
    }
}
        士           勋           戚
税收  倾向降低    倾向提升      倾向提升
军制  倾向压制      私兵          
人事  倾向自身(内政)    倾向自身（军事）     倾向收买（倾向宠幸）
外交  和平          开战        开战