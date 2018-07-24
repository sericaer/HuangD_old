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
                return Inst._heath;
            }
        }

        [JsonProperty]
        private string strEmpName;
        [JsonProperty]
        private int _age;
        [JsonProperty]
        private int _heath;

        public Emperor(string strEmpName, int age, int heath)
        {
            this.strEmpName = strEmpName;
            this._age = age;
            this._heath = heath;

            Inst = this;
        }

        [SerializeField]
        static Emperor Inst;
    }
}
