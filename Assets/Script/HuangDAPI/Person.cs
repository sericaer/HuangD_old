using System;
using System.Collections.Generic;
using System.Linq;

namespace HuangDAPI
{
    public abstract class Person
    {
        public abstract string name { get; }
        public abstract int press { get; }
        public abstract int score { get; set; }
        public abstract int heath { get; set; }
        public abstract int age { get; set; }
        public abstract Dictionary<string, string> Flags { get; set; }

        public Faction faction
        {
            get
            {
                return (from x in GMData.RelationManager.FactionMap
                        where x.person.name == name
                        select x.faction).SingleOrDefault();
            }
        }
        public Office office
        {
            get
            {
                return (from x in GMData.RelationManager.OfficeMap
                        where x.person.name == name
                        select x.office).SingleOrDefault();
            }
        }

        public abstract void Die();
    }

    public static class Persons
    {
        public static Person[] All
        {
            get
            {
                return (from x in GMData.RelationManager.OfficeMap
                        select x.person).ToArray();
            }
        }
    }

    public interface PersonFlag
    {
        string name { get; }
    }

    public interface PersonProcess
    {
        Person opp { get; }
        List<object> tag { get; }
    }
}
