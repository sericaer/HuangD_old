using System;
namespace HuangDAPI
{
    public class Stability
    {
        public static int current 
        { 
            get
            {
                return MyGame.Stability.current;
            }
            set
            {
                MyGame.Stability.current = value;
            }
        }
    }

    public class Economy
    {
        public static double current
        {
            get
            {
                return MyGame.Economy.current;
            }
            set
            {
                MyGame.Economy.current = value;
            }
        }

        public static double NetIncome
        {
            get
            {
                return MyGame.Economy.NetInCome();
            }
        }
    }

    public class Military
    {
        public static int current
        {
            get
            {
                return MyGame.Military.current;
            }
            set
            {
                MyGame.Military.current = value;
            }
        }
    }

    public class Diplomacy
    {
        public static int current
        {
            get
            {
                return MyGame.Diplomacy.current;
            }
            set
            {
                MyGame.Diplomacy.current = value;
            }
        }

        public const int WAR = MyGame.Diplomacy.WAR;
        public const int PEACE = MyGame.Diplomacy.PEACE;
    }
}
