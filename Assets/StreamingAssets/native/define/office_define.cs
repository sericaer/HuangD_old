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
        [HougongAttr(group = HougongGroup.Level1)]
        HOU,

        [HougongAttr(group = HougongGroup.Level2)]
        GUI1,
        [HougongAttr(group = HougongGroup.Level2)]
        GUI2,
        [HougongAttr(group = HougongGroup.Level2)]
        GUI3,

        [HougongAttr(group = HougongGroup.Level3)]
        FEI1,
        [HougongAttr(group = HougongGroup.Level3)]
        FEI2,
        [HougongAttr(group = HougongGroup.Level3)]
        FEI3,
        [HougongAttr(group = HougongGroup.Level3)]
        FEI4,
        [HougongAttr(group = HougongGroup.Level3)]
        FEI5,
        [HougongAttr(group = HougongGroup.Level3)]
        FEI6
    }
}