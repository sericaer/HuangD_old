using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public partial class MyGame
{
    public class Disaster : HuangDAPI.Disaster
    {
        public class DisasterAttribute : Attribute
        {
            public bool isBuff;
        }

        public enum ENUM_DISASTER
        {
            [DisasterAttribute(isBuff = false)]
            DISA_HONG,
            [DisasterAttribute(isBuff = false)]
            DISA_HAN,
            [DisasterAttribute(isBuff = false)]
            DISA_HUANG,
            [DisasterAttribute(isBuff = false)]
            DISA_WEN,
            [DisasterAttribute(isBuff = false)]
            DISA_ZHEN,
            [DisasterAttribute(isBuff = false)]
            DISA_KOU,
        }

        public static Disaster NewDisaster()
        {
            ENUM_DISASTER enumDisaster = (ENUM_DISASTER)Tools.Probability.GetRandomNum(0, Enum.GetValues(typeof(ENUM_DISASTER)).Length - 1);
            return new Disaster(enumDisaster.ToString());
        }

        public string name
        {
            get
            {
                return _name;
            }
        }

        public bool recover
        {
            get
            {
                return _recover;
            }

            set
            {
                _recover = value;
            }
        }

        private Disaster(string name)
        {
            _name = name;
        }

        private string _name;
        private bool _recover;

    }
}

