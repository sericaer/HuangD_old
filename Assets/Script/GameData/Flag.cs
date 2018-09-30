using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HuangDAPI;
using Newtonsoft.Json;
                                                  
public partial class MyGame
{
    [JsonObject(MemberSerialization.Fields)]
    public class CountryFlags : SerializeManager
    {
        public static List<COUNTRY_FLAG> current
        {
            get
            {
                List<COUNTRY_FLAG> rslt = new List<COUNTRY_FLAG>();
                foreach(string name in flagNames)
                {
                    rslt.Add(COUNTRY_FLAG.Find(name));
                }

                return rslt;
            }
        }

        public static void Add(COUNTRY_FLAG flag)
        {
            flagNames.Add(flag.Title());
        }

        public static void Remove(COUNTRY_FLAG flag)
        {
            flagNames.Remove(flag.Title());
        }

        [SerializeField]
        static List<string> flagNames = new List<string>();
    }
}
