using UnityEngine;
using Tools;


public partial class MyGame
{
    public class Stability : SerializeManager
    {
        public static int current
        {
            get
            {
                return _current;
            }
            set
            {

                _current = value;
                if(_current > Max)
                {
                    _current = Max;
                }
                if (_current < Min)
                {
                    _current = Min;
                }
            }
        }

        [SerializeField]
        public static int _current = Probability.GetRandomNum(0, 3);

        public const int Max = 5;
        public const int Min = 0;
    }
}
