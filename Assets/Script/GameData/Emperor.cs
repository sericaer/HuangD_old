using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public partial class MyGame
{
    [JsonObject(MemberSerialization.Fields)]
    public class Emperor : SerializeManager
    {
        public static void Initialize(string strEmpName, int age, int heath)
        {
            Inst = new Emperor(strEmpName, age, heath);
        }

        public static string name
        {
            get
            {
                return Inst.strEmpName;
            }
        }

        public static int age
        {
            get
            {
                return Inst._age;
            }
        }

        public static int heath
        {
            get
            {
                int rslt = Inst.heathbase;
                foreach (var elem in HeathEffects)
                {
                    rslt += elem.Value(0);
                }

                return rslt;
            }
        }

        public static List<Tuple<string, int>> heathDetail()
        {
            List<Tuple<string, int>> result = new List<Tuple<string, int>>();

            result.Add(new Tuple<string, int>("HEATH_BASE", Inst.heathbase));

            foreach (var elem in HeathEffects)
            {
                result.Add(new Tuple<string, int>(elem.Key, elem.Value(0)));
            }

            return result;
        }

        public static Dictionary<string, Func<dynamic, dynamic>> HeathEffects = new Dictionary<string, Func<dynamic, dynamic>>();

        [JsonProperty]
        private string strEmpName;
        [JsonProperty]
        private int _age;
        [JsonProperty]
        private int heathbase;


        private Emperor(string strEmpName, int age, int heath)
        {
            this.strEmpName = strEmpName;
            this._age = age;
            this.heathbase = heath;
        }

        [SerializeField]
        static Emperor Inst;
    }
}
