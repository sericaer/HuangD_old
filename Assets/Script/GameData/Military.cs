using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq.Expressions;

using UnityEngine;

using System.Linq;
using System.Runtime.CompilerServices;
using HuangDAPI;

public partial class MyGame
{
    public class Military : SerializeManager
    {

        [SerializeField]
        public static int current = Probability.GetRandomNum(60, 90);

        internal static Dictionary<string, int> GetConsumer()
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            result.Add("MILITARY_CONSUMER", current);

            return result;
        }
    }
}
