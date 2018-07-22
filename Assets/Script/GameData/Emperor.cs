using System;
using System.Linq;
using System.Collections.Generic;

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

        private string strEmpName;
        private int _age;
        private int _heath;

        public Emperor(string strEmpName, int age, int heath)
        {
            this.strEmpName = strEmpName;
            this._age = age;
            this._heath = heath;

            Inst = this;
        }

        static Emperor Inst;
    }
}
