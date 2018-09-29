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

        public EffectValue tHeath;
        private Emperor(string strEmpName, int age, int heath)
        {
            this.strEmpName = strEmpName;
            this._age = age;
            this.heathbase = heath;

            tHeath = new EffectValue { baseValue = heath };

            mHeath += new Tuple<string, Func<dynamic, dynamic>>("HEATH_BASE", (dynamic v)=>{
                return heath;
            });
        }
        
        [SerializeField]
        public static Emperor Inst;
    }

    public class EffectValue
    {
        public int baseValue { get; set; }

        public int current
        {
            get
            {
                int rslt = 0;
                foreach (var elem in Effects)
                {
                    rslt += elem.Item2;
                    
                }
                return rslt;
            }
        }

        public string detail
        {
            get
            {
                string rslt = "";
                foreach (var elem in Effects)
                {
                    rslt += rslt += elem.Item2.ToString() + " " + elem.Item1 + "\n";

                }
                return rslt;
            }
        }

        private List<Tuple<string, int>> Effects
        {
            get
            {
                List<Tuple<string, int>> rslt = new List<Tuple<string, int>>();
                rslt.Add(new Tuple<string, int>("BASE_VALUE", baseValue));

                foreach (var flag in HuangDAPI.COUNTRY_FLAG.All)
                {
                    if (flag.funcHeathEffect == null)
                    {
                        continue;
                    }

                    rslt.Add(new Tuple<string, int>(flag.Title(), flag.funcHeathEffect(baseValue)));
                }

                return rslt;
            }
        }

    }
}
