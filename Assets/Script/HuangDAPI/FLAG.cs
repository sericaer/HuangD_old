using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using static MyGame;

namespace HuangDAPI
{
    public class COUNTRY_FLAG : Affect
    {
        public static COUNTRY_FLAG Find(string name)
        {
            return _All.Find((x) => x.Title() == name);
        }

        public COUNTRY_FLAG()
        {

            
            _All.Add(this);
        }

        public bool IsEnabled()
        {
            return _exist;
        }


        public virtual void Enabled()
        {

        }

        public void Enable()
        {
            MyGame.CountryFlags.Add(this);

            _exist = true;
            _startTime = new GameTime(GameTime.current);
            Enabled();
        }

        public void Disable()
        {
            MyGame.CountryFlags.Remove(this);

            _exist = false;
            _startTime = null;
            Disabled();
        }

        public virtual void Disabled()
        {

        }

        public int LastTime
        {
            get
            {
                if(!_exist)
                {
                    return 0;
                }

                return GameTime.current - _startTime;
            }
        }

        public virtual string Title()
        {
            return this.GetType().Name + "_TITLE";
        }

        public virtual string Desc()
        {
            return this.GetType().Name + "_Desc";
        }


        public virtual void ProcEvent(ref string name, ref dynamic param)
        {
            return;
        }

        public static COUNTRY_FLAG[] All
        {
            get
            {
                return _All.ToArray();
            }
        }

        public static GMEvent GetEVENT()
        {
            System.Random rnd = new System.Random();

            var query = (from x in All 
                         where x.IsEnabled()
                         select x).ToList().OrderBy(x => rnd.Next());
            
            foreach (var elem in query)
            {
                string nextEvent = "";
                dynamic param = null;

                elem.ProcEvent(ref nextEvent, ref param);
                if (nextEvent != "")
                {
                    GMEvent gmEvent = new GMEvent(StreamManager.eventDict[nextEvent], param, null);
                    return gmEvent;
                }
            }

            return null;
        }

        private static List<COUNTRY_FLAG> _All = new List<COUNTRY_FLAG>();

        private bool _exist = false;

        private GameTime _startTime;

        public dynamic _param = new ExpandoObject();
    }
}