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
}
