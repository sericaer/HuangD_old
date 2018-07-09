using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mono.CSharp;

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
        public DecisionProc(string key, GameTime startTime)
        {
            name = key;
            _startTime = new GameTime(startTime);

            DECISION decisionDef = StreamManager.decisionDict[key];
            for (int i = 0; i < decisionDef._TimeLine.Length; i++)
            {
                string elem = decisionDef._TimeLine[i];
                try
                {
                    string[] kv = elem.Split("|".ToCharArray());

                    if (kv.Length != 2)
                    {
                        throw new ArgumentException("error arg " + elem);
                    }

                    _timeline.Add(new Tuple<string, int>(kv[0], Convert.ToInt32(kv[1])));
                }
                catch (Exception e)
                {
                    throw new ArgumentException("error arg " + elem);
                }
            }

            MyGame.Inst.date.incDayEvent += DayIncrease;
        }

        public int maxDay
        {
            get
            {
                int sum = 0;
                foreach (var elem in timeline)
                {
                    sum += elem.Item2;
                }

                return sum;
            }
        }
        
        public Tuple<string, int>[] timeline
        {
            get
            {
                return _timeline.ToArray();
            }
        }

        public GameTime startTime
        {
            get
            {
                return _startTime;
            }
        }

        public void DayIncrease()
        {
            currDay++;
            if(currDay >= maxDay)
            {
                MyGame.Inst.date.incDayEvent -= DayIncrease;
                MyGame.Inst.DecisionProcs.Remove(this);

                DECISION decisionDef = StreamManager.decisionDict[name];
                MyGame.Inst.eventManager.InsertProcEnd(decisionDef._finEvent, decisionDef._finEventParam);
            }
        }

        public int currDay;
        public string name;
        private List<Tuple<string, int>> _timeline = new List<Tuple<string, int>>();
        private GameTime _startTime;
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
            MyGame.Inst.DecisionProcs.Add(new DecisionProc(name, MyGame.Inst.date));
        }
    }


}
