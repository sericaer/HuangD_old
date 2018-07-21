using HuangDAPI;
using System.Linq;

namespace native
{
    public enum PROVINCE_DEFINE
    {
        [ProvinceAttribute(economy = ENUM_ECONOMY.HIGH, mainOffice = OFFICE_DEFINE.CS1)]
        ZHOUJ1,
        [ProvinceAttribute(economy = ENUM_ECONOMY.LOW, mainOffice = OFFICE_DEFINE.CS2)]
        ZHOUJ2,
        [ProvinceAttribute(economy = ENUM_ECONOMY.HIGH, mainOffice = OFFICE_DEFINE.CS3)]
        ZHOUJ3,
        [ProvinceAttribute(economy = ENUM_ECONOMY.MID, mainOffice = OFFICE_DEFINE.CS4)]
        ZHOUJ4,
        [ProvinceAttribute(economy = ENUM_ECONOMY.HIGH, mainOffice = OFFICE_DEFINE.CS5)]
        ZHOUJ5,
        [ProvinceAttribute(economy = ENUM_ECONOMY.MID, mainOffice = OFFICE_DEFINE.CS6)]
        ZHOUJ6,
        [ProvinceAttribute(economy = ENUM_ECONOMY.MID, mainOffice = OFFICE_DEFINE.CS7)]
        ZHOUJ7,
        [ProvinceAttribute(economy = ENUM_ECONOMY.HIGH, mainOffice = OFFICE_DEFINE.CS8)]
        ZHOUJ8,
        [ProvinceAttribute(economy = ENUM_ECONOMY.HIGH, mainOffice = OFFICE_DEFINE.CS9)]
        ZHOUJ9
    }
}