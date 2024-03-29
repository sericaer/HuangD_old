﻿using HuangDAPI;
using System.Linq;
using UnityEngine;

namespace native
{
    class EVENT_STAB_DEC : EVENT_HD
    {
        bool Precondition()
        {            
            return false;
        }

        class OPTION1 : Option
        {
            void Selected(ref string nxtEvent, ref object param)
            {
                Stability.current--;
            }
        }
    }

//    class EVENT_STAB_INC : EVENT_HD
//    {
//        bool Precondition()
//        {
//            float prob = CalcProb();

//            if (Probability.IsProbOccur(prob))
//                return true;

//            return false;
//        }

//        class OPTION1 : Option
//        {
//            void Selected(ref string nxtEvent, ref object param)
//            {
//                GMData.Stability++;
//            }
//        }

//        float CalcProb()
//        {
//            float prob = 0.001f;
//            return prob;
//        }
//    }

//    class EVENT_ECO_INC : EVENT_HD
//    {
//        bool Precondition()
//        {
//            return false;
//        }

//        void Initializ(object param)
//        {
//            inc = (int)param;
//        }

//        string Desc()
//        {
//            return UI.Format("EVENT_ECO_INC", inc);
//        }

//        class OPTION1 : Option
//        {
//            void Selected(ref string nxtEvent, ref object param)
//            {
//                GMData.Economy += OUTTER.inc;
//            }
//        }

//        int inc = 1;
//    }

//    class EVENT_ECO_DEC : EVENT_HD
//    {
//        bool Precondition()
//        {
//            return false;
//        }

//        void Initializ(object param)
//        {
//            dec = (int)param;
//        }

//        string Desc()
//        {
//            return UI.Format("EVENT_ECO_DEC", dec);
//        }

//        class OPTION1 : Option
//        {
//            void Selected(ref string nxtEvent, ref object param)
//            {
//                GMData.Economy -= OUTTER.dec;
//            }
//        }

//        int dec = 1;
//    }

//    class EVENT_MIL_INC : EVENT_HD
//    {
//        bool Precondition()
//        {
//            return false;
//        }

//        void Initializ(object param)
//        {
//            inc = (int)param;
//        }

//        string Desc()
//        {
//            return UI.Format("EVENT_MIL_INC", inc);
//        }

//        class OPTION1 : Option
//        {
//            void Selected(ref string nxtEvent, ref object param)
//            {
//                GMData.Military += OUTTER.inc;
//            }
//        }

//        int inc = 1;
//    }

//    class EVENT_MIL_DEC : EVENT_HD
//    {
//        bool Precondition()
//        {
//            return false;
//        }

//        void Initializ(object param)
//        {
//            dec = (int)param;
//        }

//        string Desc()
//        {
//            return UI.Format("EVENT_MIL_DEC", dec);
//        }

//        class OPTION1 : Option
//        {
//            void Selected(ref string nxtEvent, ref object param)
//            {
//                GMData.Military -= OUTTER.dec;

//            }
//        }

//        int dec = 1;
//    }
}