using UnityEngine;
using Tools;


public partial class MyGame
{
    public class Stability : SerializeManager
    {

        [SerializeField]
        public static int current = Probability.GetRandomNum(60, 90);
    }
}
