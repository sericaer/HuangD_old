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
            SetEffectEvent();
        }

        public void Disable()
        {
            _exist = false;
            _startTime = null;
            ResetEffectEvent();
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

        public delegate dynamic DelegateEffect(dynamic param);

        public virtual void Effect(ref Dictionary<FlagEffect, DelegateEffect> effectDict)
        {
            return ;
        }

        public static void AfterDeserial()
        {
            foreach(var elem in All)
            {
                if(elem.IsEnabled())
                {
                    elem.SetEffectEvent();
                }
                else
                {
                    elem.ResetEffectEvent();
                }
            }
        }

        public void SetEffectEvent()
        {
            var effectDict = new Dictionary<FlagEffect, DelegateEffect>();
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
                    default:
                        break;
                }
            }

        }

        public void ResetEffectEvent()
        {
            var effectDict = new Dictionary<FlagEffect, DelegateEffect>();
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
    }
}