﻿using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HuangDAPI;
using Newtonsoft.Json;

public partial class MyGame
{
    [JsonObject(MemberSerialization.Fields)]
    public class DecisionProcess : SerializeManager
    {
        public static List<DecisionProcess> current
        {
            get
            {
                return _current;
            }
        }

        public static void Add(DecisionProcess flag)
        {
            _current.Add(flag);
        }

        public static void Remove(DecisionProcess flag)
        {
            _current.Remove(flag);
        }

        public static void Update()
        {
            foreach(var process in _current)
            {
                process.Increase();
            }

            _current.RemoveAll(x => x.IsFinish);

            foreach (var decision in StreamManager.decisionDict.Values)
            {
                if(decision.IsEnable() && _current.Find(x => x.name != decision._funcTitle()) == null)
                {
                    var newDecisionProcess = new DecisionProcess(decision);

                    string eventName = decision._funcEnableEvent();
                    GameFrame.eventManager.InsertDecisionEvent(eventName, newDecisionProcess.name, null, newDecisionProcess);

                    _current.Add(newDecisionProcess);
                }
            }
        }

        public DecisionProcess(DECISION decision)
        {
            name = decision.GetType().Name;
            lastTimes = -1;
            IsFinish = false;

            currProcessName = decision.processTimes[0].Item1;
            currProcessTimes = 0;
            maxTimes = 0;

            foreach (var elem in decision.processTimes)
            {
                maxTimes += elem.Item2;
            }
        }

        public void Start()
        {
            if(lastTimes != -1)
            {
                throw new Exception();
            }

            lastTimes = 0;
        }

        public bool IsStart()
        {
            return lastTimes != -1;
        }

        private void Increase()
        {
            if(!IsStart())
            {
                return;
            }

            lastTimes++;

            var decisionDef = StreamManager.decisionDict[name];
            var Index = decisionDef.processTimes.FindIndex(x => x.Item1 == currProcessName);

            if(currProcessTimes+1 < decisionDef.processTimes[Index].Item2)
            {
                currProcessTimes++;
            }
            else
            {
                if(Index == decisionDef.processTimes.Count -1)
                {
                    string eventName = decisionDef._funcFinishEvent();
                    GameFrame.eventManager.InsertDecisionEvent(eventName, name, null, this);

                    IsFinish = true;
                    return;
                }

                currProcessTimes = 0;
                currProcessName = decisionDef.processTimes[Index+1].Item1;
            }
        }

        [SerializeField]
        public string name;

        [SerializeField]
        public int lastTimes;

        [SerializeField]
        public string currProcessName;

        [SerializeField]
        public int currProcessTimes;

        [SerializeField]
        public int maxTimes;

        [SerializeField]
        public bool IsFinish;

        [SerializeField]
        static List<DecisionProcess> _current = new List<DecisionProcess>();
    }
}
