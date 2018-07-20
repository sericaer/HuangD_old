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

public class OfficeAttrAttribute : Attribute
{
    public int Power = 0;
    public OfficeGroup group;
}
