using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using HuangDAPI;
using Newtonsoft.Json;

#if NET_4_6
#else
using Mono.CSharp;
#endif

using UnityEngine;
//using HuangDAPI;

public partial class MyGame
{
    public class DecisionPlan : SerializeManager,HuangDAPI.DecisionPlan
    {
        public DecisionPlan(string key)
        {
            name = key;
            All.Add(this);
        }

        public bool IsEnable()
        {
            HuangDAPI.DECISION decisionDef = StreamManager.decisionDict[name];

            bool isEnable =  decisionDef.IsEnable();

            if(oldState != isEnable)
            {
                if(isEnable == true)
                {
                    dynamic param = new ExpandoObject();
                    //string eventName = decisionDef._funcEnableEvent(ref param);


                    //GameFrame.eventManager.InsertDecisionEvent(eventName, name, param);
                }
                if(isEnable == false)
                {
                    string eventName = decisionDef._funcDisableEvent();


                    //GameFrame.eventManager.InsertDecisionEvent(eventName, name, null);
                }

                oldState = isEnable;
            }

            return isEnable;
        }

        public void process()
        {
            HuangDAPI.DECISION decisionDef = StreamManager.decisionDict[name];
            //GameFrame.eventManager.InsertDecisionEvent(decisionDef._funcStartEvent(), name, null);

            var process = new DecisionProc(name);
            All.Remove(this);
        }

        public string name;
        //public HuangDAPI.Office ResponsibleOffice
        //{
        //    get
        //    {
        //        HuangDAPI.DECISION decisionDef = StreamManager.decisionDict[name];
        //        return HuangDAPI.Offices.All.First(x => x.name == decisionDef._Responsible);
        //    }
        //}

        private bool? oldState = null;

        public HuangDAPI.Office ResponsibleOffice
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        [SerializeField]
        public static List<DecisionPlan> All = new List<DecisionPlan>();
    }

    [JsonObject(MemberSerialization.Fields)]
    public class DecisionProc : SerializeManager,HuangDAPI.DecisionProc
    {
        public DecisionProc(string key)
        {
            name = key;
            //_startTime = new GameTime(MyGame.Inst.date);

            //decisionDef = StreamManager.decisionDict[key];

            //resPerson = HuangDAPI.Offices.All.First(x=>x.name== decisionDef._Responsible).person;

            //decisionDef.Flags = this.Flags;

            

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

            MyGame.GameTime.current.incDayEvent += DayIncrease;
            All.Add(this);
        }

        public HuangDAPI.Person ResponsiblePerson
        {
            get
            {
                return Person.All.Where((arg) => arg.name == resPerson).SingleOrDefault();
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

                HuangDAPI.DECISION decisionDef = StreamManager.decisionDict[name];
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

        //public GameTime startTime
        //{
        //    get
        //    {
        //        return _startTime;
        //    }
        //}

        public void DayIncrease()
        {
            currDay++;

            HuangDAPI.DECISION decisionDef = StreamManager.decisionDict[name];
            //GameFrame.eventManager.InsertDecisionEvent(decisionDef._funcProcEvent(), name, "");


            if(decisionDef._funcProcFinish != null && decisionDef._funcProcFinish())
            {
                All.Remove(this);
                //GameFrame.eventManager.InsertDecisionEvent(decisionDef._funcFinishEvent(), name, "");
                return;
            }
            else if (currDay >= maxDay)
            {
                MyGame.GameTime.current.incDayEvent -= DayIncrease;
                All.Remove(this);
                //GameFrame.eventManager.InsertDecisionEvent(decisionDef._funcFinishEvent(), name, "");
                return;
            }
        }

        public int currDay;
        public string name;
        private List<string> _Flags = new List<string>();
        private List<Tuple<string, int>> _timeline = new List<Tuple<string, int>>();
        private string resPerson = null;
        //private GameTime _startTime;
        private string decisionname = null;

        [SerializeField]
        public static List<DecisionProc> All =new List<DecisionProc>();
    }

    //private Dictionary<string, HuangDAPI.DecisionPlan> DecisionPlans = new Dictionary<string, HuangDAPI.DecisionPlan>();
    //private Dictionary<string, HuangDAPI.DecisionProc> DecisionProcs = new Dictionary<string, HuangDAPI.DecisionProc>();

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
                return DecisionPlan.All;
            }
        }

        public static List<DecisionProc> Procs
        {
            get
            {
                return DecisionProc.All;
            }
        }

        public static void Update()
        {
            foreach (var elem in StreamManager.decisionDict)
            {
                if (elem.Value.IsEnable() )
                {
                    if(!Plans.Exists((obj) => obj.name == elem.Key)
                       && !Procs.Exists((obj) => obj.name == elem.Key))
                    {
                        var plan = new DecisionPlan(elem.Key);
                        plan.IsEnable();
                    }
                }
                else
                {
                    Plans.RemoveAll((obj) => obj.name == elem.Key);
                }
            }
        }
    }

}
