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
        public static int current
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

        public static int NetIncome
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
}
