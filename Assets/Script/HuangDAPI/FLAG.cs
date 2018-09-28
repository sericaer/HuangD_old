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
    [JsonObject(MemberSerialization.Fields)]
    public class COUNTRY_FLAG : SerializeManager //: ReflectBase
    {
        public COUNTRY_FLAG()
        {
            _All.Add(this);
        }

        public bool IsEnabled()
        {
            return _exist;
        }

        public void Enable()
        {
            _exist = true;
            _startTime = new GameTime(GameTime.current);
            SetEffect();
        }

        public void Disable()
        {
            _exist = false;
            _startTime = null;
            ResetEffect();
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
            return this.GetType().Name + "TITLE";
        }

        public virtual string Desc()
        {
            return this.GetType().Name + "Desc";
        }

        //public delegate dynamic DelegateEffect(dynamic param);
        public virtual void Effect(ref Dictionary<FlagEffect, Func<dynamic, dynamic>> effectDict)
        {
            return ;
        }

        public virtual void ProcEvent(ref string name, ref dynamic param)
        {
            return;
        }

        public static void AfterDeserial()
        {
            foreach(var elem in All)
            {
                if(elem.IsEnabled())
                {
                    elem.SetEffect();
                }
                else
                {
                    elem.ResetEffect();
                }
            }
        }

        public void SetEffect()
        {
            var effectDict = new Dictionary<FlagEffect, Func<dynamic, dynamic>>();
            Effect(ref effectDict);

            foreach(var elem in effectDict)
            {
                switch (elem.Key)
                {
                    case FlagEffect.PROVINCE_TAX:
                        foreach (var prov in MyGame.Province.All)
                        {
                            prov.ProvTaxEffects.Add(this.Title(), elem.Value);
                        }
                        break;
                    case FlagEffect.PROVINCE_REB:
                        foreach (var prov in MyGame.Province.All)
                        {
                            prov.ProvRebEffects.Add(this.Title(), elem.Value);
                        }
                        break;
                    case FlagEffect.EMP_HEATH:
                        {
                            MyGame.Emperor.HeathEffects.Add(this.Title(), elem.Value);
                        }
                        break;
                    default:
                        break;
                }
            }

        }

        public void ResetEffect()
        {
            var effectDict = new Dictionary<FlagEffect, Func<dynamic, dynamic>>();
            Effect(ref effectDict);

            foreach (var elem in effectDict)
            {
                switch (elem.Key)
                {
                    case FlagEffect.PROVINCE_TAX:
                        foreach (var prov in MyGame.Province.All)
                        {
                            prov.ProvTaxEffects.Remove(this.Title());
                        }
                        break;
                    case FlagEffect.PROVINCE_REB:
                        foreach (var prov in MyGame.Province.All)
                        {
                            prov.ProvRebEffects.Remove(this.Title());
                        }
                        break;
                    case FlagEffect.EMP_HEATH:
                        {
                            MyGame.Emperor.HeathEffects.Remove(this.Title());
                        }
                        break;
                    default:
                        break;
                }
            }
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

        public Tuple<string, Func<dynamic, dynamic>> NewEffect(int param)
        {
            return new Tuple<string, Func<dynamic, dynamic>>(this.GetType().Name, (dynamic v)=>{
                return param;
            });
        }

        //public Func<string> _funcTitle;
        //public Func<string> _funcDesc;

        [SerializeField]
        private static List<COUNTRY_FLAG> _All = new List<COUNTRY_FLAG>();

        [SerializeField]
        private bool _exist = false;

        [SerializeField]
        private GameTime _startTime;

        [SerializeField]
        protected dynamic _param = new ExpandoObject();

    }

    public enum FlagEffect
    {
        EFFECT_NULL,
        PROVINCE_TAX,
        PROVINCE_REB,

        EMP_HEATH,
    }
}