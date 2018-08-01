﻿using HuangDAPI;
using System.Linq;

namespace native
{
    class COUNTRY_FLAG_SSYD : COUNTRY_FLAG<COUNTRY_FLAG_SSYD>
    {
        bool IsVisable()
        {
            return _level > LEVEL0;
        }

        string EFFECT()
        {
            switch (_level)
            {
                case LEVEL1:
                    return "PROVINCE_TAX|*0.9";
                case LEVEL2:
                    return "PROVINCE_TAX|*0.75";
                case LEVEL3:
                    return "PROVINCE_TAX|*0.5";
                default:
                    return "";
            }
        }

        enum LV
        {
            LEVEL0,
            LEVEL1,
            LEVEL2,
            LEVEL3
        }

        static LV Level
        {
            get
            {
                return Inst._level;
            }
            set
            {
                Inst.Enable();
                Inst._level = value;
            }
        }

        LV? _level = null;
    }
}