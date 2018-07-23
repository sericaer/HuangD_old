using HuangDAPI;
using System.Linq;

namespace native
{
    //static class ECONOMY_DEFINE 
    //{
    //    static int BASE_TAX = 10;
    //    static double PROV_LOW = 0.5;
    //    static double PROV_MID = 1.0;
    //    static double PROV_HIGH = 1.5;
    //}

    public enum PROV_ECONOMY_DEFINE
    {
        [ProvEconmoyLevelAttr(baseTax = 5)]
        LOW,

        [ProvEconmoyLevelAttr(baseTax = 10)]
        MID,

        [ProvEconmoyLevelAttr(baseTax = 15)]
        HIGH,
    }

    public enum PEESON_SCORE_TO_POWER_DEFINE
    {
        SCORE60_70,
        SCORE70_80,
        SCORE80_90,
        SCORE90_95,
        SCORE95_MAX,
    }
}