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
}