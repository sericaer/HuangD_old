using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HuangDAPI
{
    public abstract class COUNTRY_FLAG<T> where T : COUNTRY_FLAG<T>, new()
    {
	    public static string Title
		{
		    get
			{
                return _inst.GetType().Name + "TITLE";
            }
        }
	
        public static string Desc
        {
            get
            {
                return _inst.GetType().Name + "Desc";
            }
        }
							
        public static bool IsEnaled
        {
            get
            {
                return _inst._exist;
            }
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