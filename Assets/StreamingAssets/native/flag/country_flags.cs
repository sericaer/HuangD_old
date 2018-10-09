using HuangDAPI;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace native
{
    public class SSYD1 : COUNTRY_FLAG
    {
        public double affectCountryTax(double baseValue)
        {
            return -baseValue * 0.1;
        }
    }

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

        public double affectCountryTax(double baseValue)
        {
            switch (Level)
            {
                case 1:
                    return -baseValue * 0.1;
                case 2:
                    return -baseValue * 0.25;
                case 3:
                    return -baseValue * 0.5;
                default:
                    throw new Exception();
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

        public double affectCountryTax(double baseValue)
        {
            switch (Level)
            {
                case 1:
                    return baseValue * 0.1;
                case 2:
                    return baseValue * 0.3;
                case 3:
                    return baseValue * 0.5;
                default:
                    throw new Exception();
            }
        }

        public double affectCountryReb(double baseValue)
        {
            switch (Level)
            {
                case 1:
                    return 0.5;
                case 2:
                    return 1.0;
                case 3:
                    return 3.0;
                default:
                    throw new Exception();
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

    public class YHSX : COUNTRY_FLAG
    {
        public override void ProcEvent(ref string name, ref dynamic param)
        {
            string eventname = "";
            Probability.ProbAction(1,     ()=>{
                                                eventname = "EVENT_STAB_DEC";
                                              },
                                   0.015, ()=>{
                                                eventname = "";
                                               }
                                   );

            name = eventname;
        }

        public int affectEmperorHeath(int baseValue)
        {
            return -5;
        }
    }
}