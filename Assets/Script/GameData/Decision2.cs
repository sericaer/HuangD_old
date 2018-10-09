using System;
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

            foreach(var decision in StreamManager.decisionDict.Values)
            {
                if(decision.IsEnable() && _current.Find(x => x.name != decision._funcTitle()) == null)
                {
                    _current.Add(new DecisionProcess(decision));
                }
            }
        }

        public DecisionProcess(DECISION decision)
        {
            name = decision.GetType().Name;
            lastTimes = -1;

            currProcessName = decision.processTimes[0].Item1;
            currProcessTimes = 0;

            foreach(var elem in decision.processTimes)
            {
                maxTimes += elem.Item2;
            }
        }

        private void Increase()
        {
            if(lastTimes == -1)
            {
                return;
            }

            lastTimes++;

            var decisionDef = StreamManager.decisionDict[name];
            var processDef = decisionDef.processTimes.Find(x => x.Item1 == currProcessName);

            if(currProcessTimes+1 < processDef.Item2)
            {
                currProcessTimes++;
            }
            else
            {
                currProcessTimes = 0;
                currProcessName = processDef.Item1;
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
        static List<DecisionProcess> _current = new List<DecisionProcess>();
    }
}
