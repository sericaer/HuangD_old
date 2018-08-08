﻿using System;
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
            SetEffectEvent();
        }

        public void Disable()
        {
            _exist = false;
            ResetEffectEvent();
        }

        public virtual string Title()
        {
            return this.GetType().Name + "TITLE";
        }

        public virtual string Desc()
        {
            return this.GetType().Name + "Desc";
        }

        public virtual FlagEffect EffectKey()
        {
            return FlagEffect.EFFECT_NULL;
        }

        public virtual void EffectAction(ref object obj)
        {
            return;
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
            FlagEffect key = EffectKey();
            switch (key)
            {
                case FlagEffect.PROVINCE_TAX:
                    foreach (var prov in MyGame.Province.All)
                    {
                        prov.ProvTaxEffectEvent -= this.EffectAction;
                        prov.ProvTaxEffectEvent += this.EffectAction;
                    }
                    break;
                default:
                    break;
            }
        }

        public void ResetEffectEvent()
        {
            FlagEffect key = EffectKey();
            switch (key)
            {
                case FlagEffect.PROVINCE_TAX:
                    foreach (var prov in MyGame.Province.All)
                    {
                        prov.ProvTaxEffectEvent -= this.EffectAction;
                    }
                    break;
                default:
                    break;
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
        protected dynamic _param = new ExpandoObject();

    }

    public enum FlagEffect
    {
        EFFECT_NULL,
        PROVINCE_TAX,
    }
}