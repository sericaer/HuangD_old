using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HuangDAPI
{
    public class COUNTRY_FLAG<T> : ReflectBase where T : COUNTRY_FLAG<T>, new()
    {	
        public static bool IsEnabled()
        {
            return _inst._exist;
        }

        public static bool Test()
        {
            return true;
        }

        public static T Inst
        {
            get
            {
                return _inst;
            }
        }

        public static void Enable()
        {
            _inst._exist = true;
        }
        public static void Disable()
        {
            _inst._exist = false;
        }
        
        public string EFFECT;

        private bool _exist=false;

        private static T _inst = new T();

    }
}