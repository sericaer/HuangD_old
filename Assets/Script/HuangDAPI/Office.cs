using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace HuangDAPI
{
    public interface Office
    {
        string name { get; }
        int power { get; }
        Person person { get; set; }
    }

    public interface Hougong
    {
        string name { get; }
        Person person { get; }
    }



    //public class Offices
    //{

    //    public static Office[] JQ
    //    {
    //        get
    //        {
    //            List<Office> list = (from x in GMData.RelationManager.OfficeMap
    //                                 where x.office.name.Contains("JQ")
    //                                 orderby x.office.name
    //                                 select x.office).ToList();

    //            return list.ToArray();
    //        }
    //    }

    //    public static Office[] SG
    //    {
    //        get
    //        {
    //            List<Office> list = (from x in GMData.RelationManager.OfficeMap
    //                                 where x.office.name.Contains("SG")
    //                                 orderby x.office.name
    //                                 select x.office).ToList();

    //            return list.ToArray();
    //        }
    //    }

    //    public static Office[] CS
    //    {
    //        get
    //        {
    //            List<Office> list = (from x in GMData.RelationManager.OfficeMap
    //                                 where x.office.name.Contains("CS")
    //                                 orderby x.office.name
    //                                 select x.office).ToList();
    //            return list.ToArray();
    //        }
    //    }

    //    public static Office[] All
    //    {
    //        get
    //        {
    //            List<Office> list = (from x in GMData.RelationManager.OfficeMap
    //                                 select x.office).ToList();
    //            return list.ToArray();
    //        }
    //    }
    //}
}
