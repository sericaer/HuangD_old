using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public partial class MyGame
{
    public class DynastyInfo : SerializeManager
    {
        public static void Initialize(string yearname, string dynastyname)
        {
            Inst = new DynastyInfo(yearname, dynastyname);

        }

        public static string yearName
        {
            get
            {
                return Inst._yearName;
            }
        }

        public static string dynastyName
        {
            get
            {
                return Inst._dynastyName;
            }
        }

        private DynastyInfo()
        {
        }

        private DynastyInfo(string yearname, string dynastyname)
        {
            _yearName = yearname;
            _dynastyName = dynastyname;
        }

        public string _yearName;
        public string _dynastyName;

        [SerializeField]
        public static DynastyInfo Inst;
    }
}
