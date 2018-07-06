﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using HuangDAPI;

public partial class MyGame
{
    [Serializable]
    public class DecisionPlan
    {
        public DecisionPlan(string key)
        {
            name = key;
        }

        public string name;
    }

    [Serializable]
    public class DecisionProc
    {
        public DecisionProc(string key)
        {
            name = key;
        }

        public string name;
    }

    private List<DecisionPlan> DecisionPlans = new List<DecisionPlan>();

    private List<DecisionProc> DecisionProcs = new List<DecisionProc>();

    public static class DecisionManager
    {
        public static void InitDecision()
        {
            Update();
        }

        public static List<DecisionPlan> Plans
        {
            get
            {
                return MyGame.Inst.DecisionPlans;
            }
        }

        public static List<DecisionProc> Procs
        {
            get
            {
                return MyGame.Inst.DecisionProcs;
            }
        }

        public static void Update()
        {
            foreach (var elem in StreamManager.decisionDict)
            {
                if (elem.Value._funcPrecondition())
                {
                    MyGame.Inst.DecisionPlans.Add(new DecisionPlan(elem.Key));
                }
            }
        }
    }


}
