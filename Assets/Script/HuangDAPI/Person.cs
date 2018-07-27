using System;
using System.Collections.Generic;
using System.Linq;

namespace HuangDAPI
{
    public interface Person
    {
        string name { get; }
        //public abstract int press { get; }
        int score { get; set; }
        Faction faction { get; }
        //public abstract int heath { get; set; }
        //public abstract int age { get; set; }
        //public abstract Dictionary<string, string> Flags { get; set; }

        //public Faction faction
        //{
        //    get
        //    {
        //        return (from x in GMData.RelationManager.FactionMap
        //                where x.person.name == name
        //                select x.faction).SingleOrDefault();
        //    }
        //}
        //public Office office
        //{
        //    get
        //    {
        //        return (from x in GMData.RelationManager.OfficeMap
        //                where x.person.name == name
        //                select x.office).SingleOrDefault();
        //    }
        //}

        void Die();
    }

    //public static class Persons
    //{
    //    public static Person[] All
    //    {
    //        get
    //        {
    //            return (from x in GMData.RelationManager.OfficeMap
    //                    select x.person).ToArray();
    //        }
    //    }
    //}

    //public interface PersonFlag
    //{
    //    string name { get; }
    //}

    //public interface PersonProcess
    //{
    //    Person opp { get; }
    //    List<object> tag { get; }
    //}
}
