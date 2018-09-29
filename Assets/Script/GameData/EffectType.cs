using System;
using System.Collections;
using System.Collections.Generic;

public class EffectType
{
    public static EffectType operator + (EffectType effect, Tuple<string, Func<dynamic, dynamic>> elem)
    {
        //effect.dict.Add(elem.Item1, elem.Item2);
        return effect;
    }

    public static EffectType operator - (EffectType effect, Tuple<string, Func<dynamic, dynamic>> elem)
    {
        //effect.dict.Remove(elem.Item1);
        return effect;
    }

    public dynamic current 
    {
        get
        {
            int rslt = 0;
            foreach (var elem in dict)
            {
                rslt += elem.Value(Base);
            }

            return rslt;
        }
    }

    public string detail
    {
        get
        {
            string rslt = "";
            foreach (var elem in dict)
            {
                rslt += elem.Value(0).ToString() + " " + elem.Key + "\n";
            }

            return rslt;
        }
    }

    Dictionary<string, Func<dynamic, dynamic>> dict = new Dictionary<string, Func<dynamic, dynamic>>();
    dynamic Base;
}

