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

        public static Dictionary<string, Func<dynamic, dynamic>> HeathEffects = new Dictionary<string, Func<dynamic, dynamic>>();

        [JsonProperty]
        private string strEmpName;
        [JsonProperty]
        private int _age;
        [JsonProperty]
        private int heathbase;

        public EffectType mHeath = new EffectType();

        private Emperor(string strEmpName, int age, int heath)
        {
            this.strEmpName = strEmpName;
            this._age = age;
            this.heathbase = heath;

            mHeath += new Tuple<string, Func<dynamic, dynamic>>("HEATH_BASE", (dynamic v)=>{
                return heath;
            });
        }

        [SerializeField]
        public static Emperor Inst;
    }
}
