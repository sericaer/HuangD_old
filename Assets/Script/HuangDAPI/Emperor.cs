using System;
namespace HuangDAPI
{
    public class Emperor
    {
        public static string name
        {
            get
            {
                return MyGame.Emperor.name;
            }
        }

        public static int age
        {
            get
            {
                return MyGame.Emperor.age;
            }
        }

        public static EffectType Heath
        {
            get
            {
                return MyGame.Emperor.Inst.mHeath;
            }
            set
            {
                MyGame.Emperor.Inst.mHeath = value;
            }
        }
    }
}