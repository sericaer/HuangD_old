using HuangDAPI;
using System.Linq;
using UnityEngine;

namespace native
{
    //public class SSYD : COUNTRY_FLAG
    //{
    //    public readonly int LEVEL0 = 0;
    //    public readonly int LEVEL1 = 1;
    //    public readonly int LEVEL2 = 2;
    //    public readonly int LEVEL3 = 3;

    //    public SSYD()
    //    {
    //        _level = LEVEL0;
    //    }

    //    public string Title()
    //    {
    //        return string.Format("{0}_{1}_{2}", GetType().Name, _level, "TITLE");
    //    }

    //    public string Desc()
    //    {
    //        return string.Format("{0}_{1}_{2}", GetType().Name, _level, "DESC");
    //    }

    //    public FlagEffect EffectKey()
    //    {
    //        return FlagEffect.PROVINCE_TAX;
    //    }

    //    public void EffectAction(ref int tax)
    //    {
    //        if(_level == LEVEL1)
    //        {
    //            tax = (int)(tax * 0.9);
    //            return;
    //        }
    //        if (_level == LEVEL2)
    //        {
    //            tax = (int)(tax * 0.75);
    //            return;
    //        }
    //        if (_level == LEVEL3)
    //        {
    //            tax = (int)(tax * 0.5);
    //            return;
    //        }

    //        return;
    //    }

    //    //public static string EFFECT()
    //    //{
    //    //    switch (Inst._level)
    //    //    {
    //    //        case LV.LEVEL1:
    //    //            return "PROVINCE_TAX|*0.9";
    //    //        case LV.LEVEL2:
    //    //            return "PROVINCE_TAX|*0.75";
    //    //        case LV.LEVEL3:
    //    //            return "PROVINCE_TAX|*0.5";
    //    //        default:
    //    //            return "";
    //    //    }
    //    //}

    //    //public class LV
    //    //{
    //    //    public const int LEVEL0 = 0;
    //    //    public const int LEVEL1 = 1;
    //    //    public const int LEVEL2 = 2;
    //    //    public const int LEVEL3 = 3;
    //    //}

    //    public int Level
    //    {
    //        get
    //        {
    //            return _level;
    //        }
    //        set
    //        {
    //            Enable();
    //            _level = value;
    //        }
    //    }

    //    private int _level;

    //}

    public class SSYD : COUNTRY_FLAG
    {
        public SSYD()
        {
            _param.level = 0;
        }

        public override string Title()
        {
            return string.Format("{0}_{1}_{2}", "COUNTRY_FLAG", GetType().Name + Level.ToString(), "TITLE");
        }

        public override string Desc()
        {
            return string.Format("{0}_{1}_{2}", "COUNTRY_FLAG", GetType().Name + Level.ToString(), "DESC");
        }

        public override FlagEffect EffectKey()
        {
            return FlagEffect.PROVINCE_TAX;
        }

        public override void EffectAction(ref object tax)
        {
            switch(Level)
            {
                case 1:
                    tax = ((double)tax * 0.9);
                    break;
                case 2:
                    tax = ((double)tax * 0.75);
                    break;
                case 3:
                    tax = ((double)tax * 0.5);
                    break;
            }
        }

        public int Level
        {
            get
            {
                //Debug.Log(_param.level);
                return (int)_param.level;
            }
            set
            {
                _param.level = value;
                if (_param.level == 0)
                {
                    Disable();
                }
                else
                {
                    Enable();
                }

            }
        }
    }

    public class KJZS : COUNTRY_FLAG
    {
        public KJZS()
        {
            _param.level = 0;
        }

        public override string Title()
        {
            return string.Format("{0}_{1}_{2}", "COUNTRY_FLAG", GetType().Name + Level.ToString(), "TITLE");
        }

        public override string Desc()
        {
            return string.Format("{0}_{1}_{2}", "COUNTRY_FLAG", GetType().Name + Level.ToString(), "DESC");
        }

        public override FlagEffect EffectKey()
        {
            return FlagEffect.PROVINCE_TAX;
        }

        public override void EffectAction(ref object tax)
        {
            switch (Level)
            {
                case 1:
                    tax = ((double)tax * 1.1);
                    break;
                case 2:
                    tax = ((double)tax * 1.3);
                    break;
                case 3:
                    tax = ((double)tax * 1.5);
                    break;
                default:
                    break;
            }
        }

        public int Level
        {
            get
            {
                return (int)_param.level;
            }
            set
            {
                _param.level = value;
                if (_param.level == 0)
                {
                    Disable();
                }
                else
                {
                    Enable();
                }

            }
        }

        public int MAX_LEVEL = 3;
        public int MIN_LEVEL = 0;
    }

    //public class SSYD1 : COUNTRY_FLAG
    //{

    //    public override string Title()
    //    {
    //        return string.Format("{0}_{1}_{2}", "COUNTRY_FLAG", GetType().Name, "TITLE");
    //    }

    //    public override string Desc()
    //    {
    //        return string.Format("{0}_{1}_{2}", "COUNTRY_FLAG", GetType().Name, "DESC");
    //    }

    //    public override FlagEffect EffectKey()
    //    {
    //        return FlagEffect.PROVINCE_TAX;
    //    }

    //    public override void EffectAction(ref object tax)
    //    {
  
    //        tax = (int)((int)tax * 0.9);
    //        return;
    //    }

    //}

    //public class SSYD2 : COUNTRY_FLAG
    //{
    //    public override string Title()
    //    {
    //        return string.Format("{0}_{1}_{2}", "COUNTRY_FLAG", GetType().Name, "TITLE");
    //    }

    //    public override string Desc()
    //    {
    //        return string.Format("{0}_{1}_{2}", "COUNTRY_FLAG", GetType().Name, "DESC");
    //    }

    //    public override FlagEffect EffectKey()
    //    {
    //        return FlagEffect.PROVINCE_TAX;
    //    }

    //    public override void EffectAction(ref object tax)
    //    {

    //        tax = (int)((int)tax * 0.75);
    //        return;
    //    }

    //}

    //public class SSYD3 : COUNTRY_FLAG
    //{

    //    public override string Title()
    //    {
    //        return string.Format("{0}_{1}_{2}", "COUNTRY_FLAG", GetType().Name, "TITLE");
    //    }

    //    public override string Desc()
    //    {
    //        return string.Format("{0}_{1}_{2}", "COUNTRY_FLAG", GetType().Name, "DESC");
    //    }

    //    public override FlagEffect EffectKey()
    //    {
    //        return FlagEffect.PROVINCE_TAX;
    //    }

    //    public override void EffectAction(ref object tax)
    //    {

    //        tax = (int)((int)tax * 0.5);
    //        return;
    //    }

    //}

}