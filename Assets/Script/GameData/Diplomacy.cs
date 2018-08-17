using System;
public partial class MyGame
{
    public class Diplomacy : SerializeManager
    {
        public const int WAR = 0;
        public const int PEACE = 1;
        public static int current = PEACE;
    }
}
