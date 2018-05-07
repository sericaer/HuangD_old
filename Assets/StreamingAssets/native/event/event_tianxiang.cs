using HuangDAPI;
using UnityEngine;

namespace native
{
    public class EVENT_TEST: EVENT_HD
    {
        bool Precondition()
        {
            return true;
        }

        string title = "EVENT_TEST_TITLE";
        string desc = "EVENT_TEST_DESC";

        public class OPTION1 : Option
        {
            string desc = "OPTION1";
            
            public override string Selected(out string ret)
            {
                Debug.Log("EVENT_TEST");
                ret = null;
                return null;
            }
        }
    }
}