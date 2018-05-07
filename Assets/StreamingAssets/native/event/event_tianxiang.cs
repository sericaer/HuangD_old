using HuangDAPI;
using UnityEngine;

namespace native
{
    public partial class EVENT_TEST: EVENT_HD
    {
        string title = "EVENT_TEST_TITLE";
        string desc = "EVENT_TEST_DESC";

        bool Precondition()
        {
            return true;
        }

        class OPTION1 : Option
        {
            string desc = "OPTION1";
        }

        class OPTION2 : Option
        {
            string desc = "OPTION2";
        }

        class OPTION3 : Option
        {
            string desc = "OPTION3";

            string Selected(out string ret)
            {
                Debug.Log(((EVENT_TEST)OUTTER).title + desc);
                ret = null;
                return null;
            }
        }
    }
}