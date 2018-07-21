using System;

public enum OfficeGroup
{
    Center1,
    Center2,

    Local,
}

public enum HougongGroup
{
    Level1,
    Level2,
    Level3
}

public class OfficeAttr : Attribute
{
    public int Power = 0;
    public OfficeGroup group;
}

public class HougongAttr : Attribute
{
    public HougongGroup group;
}

public enum ENUM_ECONOMY
{
    HIGH,
    MID,
    LOW,

}

public class ProvinceAttribute : Attribute
{
    public ENUM_ECONOMY economy;
    public object mainOffice;
}