using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mono.CSharp;

using UnityEngine;
//using HuangDAPI;

public partial class MyGame
{
    [Serializable]
    public class DecisionPlan : HuangDAPI.DecisionPlan
    {
        public DecisionPlan(string key)
        {
            name = key;
        }

        public bool IsEnable()
        {
            HuangDAPI.DECISION decisionDef = StreamManager.decisionDict[name];
            bool isEnable =  decisionDef._funcEnable();

            if(oldState != isEnable)
            {
                if(isEnable == true)
                {
                    string eventName = decisionDef._funcEnableEvent();
                    Debug.Log("Add " + eventName);

                    MyGame.Inst.eventManager.InsertDecisionEvent(eventName, name, "");
                }
                if(isEnable == false)
                {
                    string eventName = decisionDef._funcDisableEvent();
                    Debug.Log("Add" + eventName);

                    MyGame.Inst.eventManager.InsertDecisionEvent(eventName, name, "");
                }
            }

            return isEnable;
        }

        public void process()
        {
            HuangDAPI.DECISION decisionDef = StreamManager.decisionDict[name];
            MyGame.Inst.eventManager.InsertDecisionEvent(decisionDef._funcStartEvent(), name, "");

            MyGame.Inst.DecisionProcs.Add(name, new DecisionProc(name));
            MyGame.Inst.DecisionPlans.Remove(name);
        }

        public string name;
        public HuangDAPI.Office ResponsibleOffice
        {
            get
            {
                HuangDAPI.DECISION decisionDef = StreamManager.decisionDict[name];
                return HuangDAPI.GMData.Offices.All.First(x => x.name == decisionDef._Responsible);
            }
        }

        private bool? oldState = null;
    }

    [Serializable]
    public class DecisionProc : HuangDAPI.DecisionProc
    {
        public DecisionProc(string key)
        {
            name = key;
            _startTime = new GameTime(MyGame.Inst.date);

            decisionDef = StreamManager.decisionDict[key];

            resPerson = HuangDAPI.GMData.Offices.All.First(x=>x.name== decisionDef._Responsible).person;

            decisionDef.Flags = this.Flags;

            

            //for (int i = 0; i < decisionDef._TimeLine.Length; i++)
            //{
            //    string elem = decisionDef._TimeLine[i];
            //    try
            //    {
            //        string[] kv = elem.Split("|".ToCharArray());

            //        if (kv.Length != 2)
            //        {
            //            throw new ArgumentException("error arg " + elem);
            //        }

            //        _timeline.Add(new Tuple<string, int>(kv[0], Convert.ToInt32(kv[1])));
            //    }
            //    catch (Exception e)
            //    {
            //        throw new ArgumentException("error arg " + elem);
            //    }
            //}

            MyGame.Inst.date.incDayEvent += DayIncrease;
        }

        public HuangDAPI.Person ResponsiblePerson
        {
            get
            {
                return resPerson;
            }
        }
        public List<string> Flags
        {
            get
            {
                return _Flags;
            }
         }

        public int maxDay
        {
            get
            {
                //int sum = 0;
                //foreach (var elem in timeline)
                //{
                //    sum += elem.Item2;
                //}

                //return sum;

                
                return decisionDef._CostDay;
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

            MyGame.Inst.eventManager.InsertDecisionEvent(decisionDef._funcProcEvent(), name, "");

            if (currDay >= maxDay && maxDay != 0)
            {
                MyGame.Inst.date.incDayEvent -= DayIncrease;
                MyGame.Inst.DecisionProcs.Remove(this.name);
                MyGame.Inst.eventManager.InsertDecisionEvent(decisionDef._funcFinishEvent(), name, "");
                return;
            }
            if(decisionDef._funcProcFinish != null && decisionDef._funcProcFinish())
            {
                MyGame.Inst.DecisionProcs.Remove(this.name);
                MyGame.Inst.eventManager.InsertDecisionEvent(decisionDef._funcFinishEvent(), name, "");
                return;
            }
        }

        public int currDay;
        public string name;
        private List<string> _Flags = new List<string>();
        private List<Tuple<string, int>> _timeline = new List<Tuple<string, int>>();
        private HuangDAPI.Person resPerson = null;
        private GameTime _startTime;
        private HuangDAPI.DECISION decisionDef;
    }

    private Dictionary<string, HuangDAPI.DecisionPlan> DecisionPlans = new Dictionary<string, HuangDAPI.DecisionPlan>();
    private Dictionary<string, HuangDAPI.DecisionProc> DecisionProcs = new Dictionary<string, HuangDAPI.DecisionProc>();

    public static class DecisionManager
    {
        public static void InitDecision()
        {
            Update();
        }

        public static Dictionary<string, HuangDAPI.DecisionPlan> Plans
        {
            get
            {
                return MyGame.Inst.DecisionPlans;
            }
        }

        public static Dictionary<string, HuangDAPI.DecisionProc> Procs
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
                if (elem.Value._funcVisable())
                {
                    if(!MyGame.Inst.DecisionPlans.ContainsKey(elem.Key)
                        && !MyGame.Inst.DecisionProcs.ContainsKey(elem.Key))
                    {
                        MyGame.Inst.DecisionPlans.Add(elem.Key, new DecisionPlan(elem.Key));
                    }
                }
                else
                {
                    MyGame.Inst.DecisionPlans.Remove(elem.Key);
                }
            }
        }
    }

}
