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

public class ProvEconmoyLevelAttr : Attribute
{
    public string levelName;
    public int baseTax;
}

public class ProvinceAttribute : Attribute
{
    public object economy;
    public object mainOffice;
}