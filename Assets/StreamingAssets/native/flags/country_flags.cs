using HuangDAPI;
using System.Linq;

namespace native
{
    class COUNTRY_FLAG_SSYD : COUNTRY_FLAG<COUNTRY_FLAG_SSYD>
    {
        public static bool IsVisable()
        {
            return _level != null  && _level != LV.LEVEL0;
        }

        public static string EFFECT()
        {
            switch (_level)
            {
                case LV.LEVEL1:
                    return "PROVINCE_TAX|*0.9";
                case LV.LEVEL2:
                    return "PROVINCE_TAX|*0.75";
                case LV.LEVEL3:
                    return "PROVINCE_TAX|*0.5";
                default:
                    return "";
            }
        }

        public static class LV
        {
            const string LEVEL0 = "LEVEL0";
            const string LEVEL1 = "LEVEL1";
            const string LEVEL2 = "LEVEL2";
            const string LEVEL3 = "LEVEL3";
        }

        public static string Level
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

        string _level = null;
    }
}