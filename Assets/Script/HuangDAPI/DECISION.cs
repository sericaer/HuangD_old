﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HuangDAPI
{
    public abstract class DECISION : ReflectBase
    {
        public DECISION()
        {
            title = StreamManager.uiDesc.Get(this.GetType().Name + "_TITLE");

            _funcTitle = GetDelegateInSubEvent<Func<string>>("Title",
                                                 () =>
                                                 {
                                                     FieldInfo field = _subFields.Where(x => x.Name == "title").First();
                                                     return (string)field.GetValue(this);
                                                 });
            _funcVisable = GetDelegateInSubEvent<Func<bool>>("Visable",
                                                  () =>
                                                  {
                                                      return true;
                                                  });
            _funcEnable = GetDelegateInSubEvent<Func<bool>>("Enable",
                                                  () =>
                                                  {
                                                      return true;
                                                  });
            _funcEnableEvent = GetDelegateInSubEvent<Func<string>>("EnableEvent",
                                                  () =>
                                                  {
                                                      return "";
                                                  });
            _funcDisableEvent = GetDelegateInSubEvent<Func<string>>("DisableEvent",
                                                  () =>
                                                  {
                                                      return "";
                                                  });
            _funcFinishEvent = GetDelegateInSubEvent<Func<string>>("FinishEvent",
                                                  () =>
                                                  {
                                                      return "";
                                                  });
            _funcStartEvent = GetDelegateInSubEvent<Func<string>>("StartEvent",
                                                  () =>
                                                  {
                                                      return "";
                                                  });
            _funcProcEvent = GetDelegateInSubEvent<Func<string>>("ProcEvent",
                                                  () =>
                                                  {
                                                      return "";
                                                  });

            var fieldInfo = _subFields.Where(x => x.Name == "CostDay").SingleOrDefault();
            if (fieldInfo != null)
            {
                _CostDay = (int)fieldInfo.GetValue(this);
            }

            fieldInfo = _subFields.Where(x => x.Name == "Responsible").SingleOrDefault();
            if (fieldInfo != null)
            {
                _Responsible = (string)fieldInfo.GetValue(this);
            }
        }


        public Func<bool> _funcVisable;
        public Func<bool> _funcEnable;

        public Func<string> _funcTitle;

        public Func<string> _funcEnableEvent;
        public Func<string> _funcDisableEvent;

        public Func<string> _funcStartEvent;
        public Func<string> _funcFinishEvent;
        public Func<string> _funcProcEvent;

        public string _funcFinishEventParam;

        public int _CostDay = 0;
        public string _Responsible = "";
        protected string title;
        public IList<string> Flags;

    }
}
