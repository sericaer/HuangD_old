using HuangDAPI;
using System.Linq;

namespace native
{
    public enum OFFICE_DEFINE
    {
        [OfficeAttr(Power = 11, group = OfficeGroup.Center1)]
        SG1,
        [OfficeAttr(Power = 8, group = OfficeGroup.Center1)]
        SG2,
        [OfficeAttr(Power = 7, group = OfficeGroup.Center1)]
        SG3,

        [OfficeAttr(Power = 10, group = OfficeGroup.Center2)]
        JQ1,
        [OfficeAttr(Power = 5, group = OfficeGroup.Center2)]
        JQ2,
        [OfficeAttr(Power = 5, group = OfficeGroup.Center2)]
        JQ3,
        [OfficeAttr(Power = 5, group = OfficeGroup.Center2)]
        JQ4,
        [OfficeAttr(Power = 5, group = OfficeGroup.Center2)]
        JQ5,
        [OfficeAttr(Power = 5, group = OfficeGroup.Center2)]
        JQ6,
        [OfficeAttr(Power = 5, group = OfficeGroup.Center2)]
        JQ7,
        [OfficeAttr(Power = 5, group = OfficeGroup.Center2)]
        JQ8,
        [OfficeAttr(Power = 5, group = OfficeGroup.Center2)]
        JQ9,


        [OfficeAttr(Power = 3, group = OfficeGroup.Local)]
        CS1,
        [OfficeAttr(Power = 3, group = OfficeGroup.Local)]
        CS2,
        [OfficeAttr(Power = 3, group = OfficeGroup.Local)]
        CS3,
        [OfficeAttr(Power = 3, group = OfficeGroup.Local)]
        CS4,
        [OfficeAttr(Power = 3, group = OfficeGroup.Local)]
        CS5,
        [OfficeAttr(Power = 3, group = OfficeGroup.Local)]
        CS6,
        [OfficeAttr(Power = 3, group = OfficeGroup.Local)]
        CS7,
        [OfficeAttr(Power = 3, group = OfficeGroup.Local)]
        CS8,
        [OfficeAttr(Power = 3, group = OfficeGroup.Local)]
        CS9
    }

    public enum HOUGONG_DEFINE
    {
        [OfficeAttr(group = HougongGroup.Level1))]
        HOU,

        [OfficeAttr(group = HougongGroup.Level2))]
        GUI1,
        [OfficeAttr(group = HougongGroup.Level2))]
        GUI2,
        [OfficeAttr(group = HougongGroup.Level2))]
        GUI3,

        [OfficeAttr(group = HougongGroup.Level3))]
        FEI1,
        [OfficeAttr(group = HougongGroup.Level3))]
        FEI2,
        [OfficeAttr(group = HougongGroup.Level3))]
        FEI3,
        [OfficeAttr(group = HougongGroup.Level3))]
        FEI4,
        [OfficeAttr(group = HougongGroup.Level3))]
        FEI5,
        [OfficeAttr(group = HougongGroup.Level3))]
        FEI6
    }
}