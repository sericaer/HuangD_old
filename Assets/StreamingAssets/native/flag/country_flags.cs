﻿using HuangDAPI;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace native
{
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



        public override void Effect(ref Dictionary<FlagEffect, DelegateEffect> effectDict)
        {
            effectDict.Add(FlagEffect.PROVINCE_TAX, (dynamic tax)=>
            {
                switch (Level)
                {
                    case 1:
                        return -tax * 0.1;
                        break;
                    case 2:
                        return -tax * 0.25;
                        break;
                    case 3:
                        return -tax * 0.5;
                        break;
                    default:
                        return null;
                }
            });
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

        public override void Effect(ref Dictionary<FlagEffect, DelegateEffect> effectDict)
        {
            effectDict.Add(FlagEffect.PROVINCE_TAX, (dynamic tax)=>
            {
                switch (Level)
                {
                    case 1:
                        return tax * 0.1;
                        break;
                    case 2:
                        return tax * 0.3;
                        break;
                    case 3:
                        return tax * 0.5;
                        break;
                    default:
                        return null;
                }
            });
            effectDict.Add(FlagEffect.PROVINCE_REB, (dynamic reb)=>
            {
                return Level;
            });
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