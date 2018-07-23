using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public partial class MyGame
{
    public class Emperor
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

        [SerializeField]
        private string strEmpName;
        [SerializeField]
        private int _age;
        [SerializeField]
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
