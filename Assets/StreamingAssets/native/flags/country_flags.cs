using HuangDAPI;
using System.Linq;

namespace native
{
    class COUNTRY_FLAG_SSYD : COUNTRY_FLAG<COUNTRY_FLAG_SSYD>
    {
        public static string Title()
        {
            return string.Format("{0}_{1}_{2}", Inst.GetType().Name, Inst._level, "TITLE");
        }

        public static string Desc()
        {
            return string.Format("{0}_{1}_{2}", Inst.GetType().Name, Inst._level, "DESC");
        }

        public static bool IsVisable()
        {
            return Inst._level > LV.LEVEL0;
        }

        public static string EFFECT()
        {
            switch (Inst._level)
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
            public const int LEVEL0 = 0;
            public const int LEVEL1 = 1;
            public const int LEVEL2 = 2;
            public const int LEVEL3 = 3;
        }

        public static int Level
        {
            get
            {
                return Inst._level;
            }
            set
            {
                Enable();
                Inst._level = value;
            }
        }

        int _level = LV.LEVEL0;
    }
}