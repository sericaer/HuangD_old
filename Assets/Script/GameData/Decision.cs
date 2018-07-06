using System;
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
                    if(!MyGame.Inst.DecisionPlans.Exists(x=>x.name == elem.Key)
                        && !MyGame.Inst.DecisionProcs.Exists(x => x.name == elem.Key))
                    {
                        MyGame.Inst.DecisionPlans.Add(new DecisionPlan(elem.Key));
                    }
                }
                else
                {
                    MyGame.Inst.DecisionPlans.RemoveAll(x => x.name == elem.Key);
                }
            }
        }

        internal static void DecisionDo(string name)
        {
            MyGame.Inst.DecisionPlans.RemoveAll(x => x.name == name);
            MyGame.Inst.DecisionProcs.Add(new DecisionProc(name));
        }
    }


}
